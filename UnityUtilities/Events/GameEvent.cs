using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : MonoBehaviour {

	[Tooltip("Whether this event can be interupted by other events.")]
	/// <summary>
	/// Whether this event can be interupted by other events.
	/// </summary>
	public bool interuptable = false;


	[Tooltip("Whether this event pauses player input (excepting \"Next\" option).")]
	/// <summary>
	/// Whether this event pauses player input (excepting "Next" option).
	/// </summary>
	public bool pausesControl = false;


	[Tooltip("The next game event in the chain.")]
	/// <summary>
	/// The next game event in the chain.
	/// </summary>
	public GameEvent nextEvent;

	/// <summary>
	/// The game controller.
	/// </summary>
	protected GameController _gameController;


	// initialize variables
	void Start() {
		startRoutine();
	}

	/// <summary>
	/// The routine ran on start for this event.
	/// </summary>
	protected virtual void startRoutine()
	{
		GameObject gameController = GameObject.Find( "Game Controller" );

		if (gameController) {
			_gameController = gameController.GetComponent<GameController>();
		}
	}

	/// <summary>
	/// Called by the game controller every update
	/// </summary>
	public abstract void onUpdate(); // this is where the event does the thing!

	/// <summary>
	/// Called by the game controller when the event is first launched.
	/// </summary>
	public abstract void onActivation();

	/// <summary>
	/// If this gameEvent has completed
	/// </summary>
	public abstract bool canBeInterruped();


	/// <summary>
	/// Sends the next event in chain to the game controller
	/// </summary>
	protected virtual void SendNextEventInChain() 
	{	
		if (nextEvent) 
		{
			_gameController.SendMessage("SetReceivingInput", !nextEvent.pausesControl );
		} 
		else if( pausesControl ) 
		{
			// return control of the character to the player if no next event
			_gameController.SendMessage("SetReceivingInput", true);
		}

		// passes the next event up to the game cotroller
		_gameController.SetEvent( nextEvent );
	}

}
