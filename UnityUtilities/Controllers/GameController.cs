using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Primarily concerned with coordinating between different game objects
/// </summary>
public class GameController : MonoBehaviour 
{
	[Tooltip("The background music for this level.")]
	public AudioClip BGM;	// NOTE: this might move to a level controller when such a thing is implemented.


	[Tooltip("The player Character")]
	public GameObject playerCharacter;

	[Tooltip("The Event that triggers at the start of the level ( if any )")]
	public GameEvent startingEvent;

	/// <summary>
	/// The current event.
	/// </summary>
	private GameEvent _currentEvent;

	[Tooltip("The message controller of the message window.")]
	public MessageController messageController;

    public LevelLoadingData LevelLoadingData { get; private set; }

    public void HeresLevelLoadingData(LevelLoadingData levelLoadingData)
	{
		LevelLoadingData = levelLoadingData; 
	}

	void Update ()
	{
		CheckIfQuit();
		RunCurrentEvent();
	}

	

	private static void CheckIfQuit()
	{
		if (Joypad.Read.Buttons.Pressed("quit"))
		{
			SceneManager.LoadScene("Outro");
		}
	}

	/// <summary>
	/// executes the current event's update function
	/// </summary>
	private void RunCurrentEvent() 
	{
		if (_currentEvent) 
		{
			_currentEvent.onUpdate();
		}
	}

	/// <summary>
	/// Sets the event and returns true if successful.
	/// </summary>
	/// <returns><c>true</c>, if event was set, <c>false</c> otherwise.</returns>
	/// <param name="gameEventStage">Game event stage.</param>
	public bool SetEvent( GameEvent gameEventStage ) 
	{
		// sets currentEvent if there's not currently an event playing or the current event is interruptable
		if(gameEventStage == null && _currentEvent.canBeInterruped() ) 
		{
			_currentEvent = null;
			return true;
		} 
		else if (!_currentEvent || _currentEvent.interuptable || gameEventStage.GetInstanceID() == _currentEvent.nextEvent.GetInstanceID() ) 
		{	
			_currentEvent = gameEventStage;
			_currentEvent.onActivation();

			if (_currentEvent.pausesControl) 
			{
				SetReceivingInput(false);
			}

			return true;
		} 
		else 
		{
			return false;
		}
	}

	public void SetReceivingInput(bool state)
	{
		playerCharacter.SendMessage("SetReceivingInput", state);
	}
}

public class LevelLoadingData
{
	public int TargetEntranceID;
	public GameObject PlayerCharacter;
	public float HeightAboveFloor;
	public Vector2 Velocity;
}