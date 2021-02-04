using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A single dialog event
/// </summary>
public class Dialog : GameEvent 
{
	[TextArea]
	[Tooltip("The line of dialog to display.")]
	public string line;

	[Tooltip("The character portrait to display.")]
	public Sprite characterPortrait;

	/// <summary>
	/// The message controller.
	/// </summary>
	private MessageController _messageController;


	[Tooltip("Whether to use message speed when displaying these dialogs.")]
	public bool useMessageSpeed = true;


	/// <summary>
	/// Whether the message has been sent to the controller
	/// </summary>
	private bool messageAccepted = false;


	// stuff to do every gameController update while this game event is the active one
	public override void onUpdate()
	{
		if ( !messageAccepted ) 
		{
			sendMessage();
		} 
		else if ( _messageController.messageFinished )
		{
			if (!_messageController.messageVisible()) 
			{
				SendNextEventInChain();
			} 
			else 
			{
				if ( Joypad.Read.Buttons.Pressed("Attack") || Joypad.Read.Buttons.Pressed("Jump") ) 
				{
					SendNextEventInChain();
				}
			}
		}
	}

	protected override void SendNextEventInChain()
	{
		base.SendNextEventInChain();

		// if the next event isn't a dialog, hide the message window
		Dialog next = nextEvent as Dialog;

		if( next == null ) 
		{
			_messageController.hideMessage();
		}
	}

	// stuff to do when this event is made active in the game controller
	public override void onActivation()
	{
		_messageController = _gameController.messageController;

		sendMessage();

		Debug.Log("onActivation() messageAccepted: " + messageAccepted);
	}

	public override bool canBeInterruped()
	{
		return _messageController.messageFinished;
	}

	/// <summary>
	/// Sends the message to the game controller
	/// </summary>
	private void sendMessage() 
	{
		// set if the GameController will use messageSpeed for this message
		_messageController.setUseMessageSpeed( useMessageSpeed );
		// attempt to display the message
		messageAccepted = _messageController.giveMessage( line, characterPortrait );
	}
}
