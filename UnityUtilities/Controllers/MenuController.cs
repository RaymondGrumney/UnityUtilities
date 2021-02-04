using CommonAssets.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// <summary>
/// Controls menus. 
/// </summary>
public class MenuController : MonoBehaviour 
{
	[Tooltip("The available menu items in this menu.")]
	public GameObject[] menuItems;

	[Tooltip("The UI Element indicating which option the user is selecting.")]
	public GameObject pointer;

	[Tooltip("The color of the selected option.")]
	public Color selectedColor;

	public float colorCycleDifference = 0.5f;
	public float colorCycleSpeed = 2f;

	/// <summary>
	/// The default color of the current selected option.
	/// </summary>
	private Color _defaultColor;

	[Tooltip("The sound when the selection changes.")]
	public AudioClip moveSound;

	[Tooltip("The sound when the selection changes.")]
	public AudioClip selectSound;

	/// <summary>
	/// The audio source.
	/// </summary>
	private AudioSource _audioSource;

	/// <summary>
	/// The current menuItem selection.
	/// </summary>
	private int _currentSelection = 0;

	/// <summary>
	/// The text element of the current menu item.
	/// </summary>
	private TextElement _currentItemText;

	/// <summary>
	/// If currently allowing directional input.
	/// </summary>
	protected bool _allowDirectionalInput;

	/// <summary>
	/// The previous menu.
	/// </summary>
	private MenuController _previousMenu;

	/// <summary>
	/// Whether this <see cref="MenuController"/> is active. 
	/// </summary>
	private bool _isActive;

	/// <summary>
	/// If the user is currently pressing a direction.
	/// </summary>
	/// <value><c>true</c> if user pressing direction; otherwise, <c>false</c>.</value>
	private bool userPressingDirection 
		=> Joypad.Read.Buttons.Held("left")
			   || Joypad.Read.Buttons.Held("right")
			   || Joypad.Read.Buttons.Held("up")
			   || Joypad.Read.Buttons.Held("down");
			//return Mathf.Abs(Joypad.Read.Buttons.vertical) > 0.1f || Mathf.Abs(Joypad.Read.Buttons.horizontal ) > 0.1f;

	/// <summary>
	/// The game controller in this scene.
	/// </summary>
	protected GameController gameController;

	void Awake () 
	{
		_audioSource = GetComponent<AudioSource>();
	}


	public void SetActive(bool state) 
	{	
		_isActive = state;

		foreach( Image i in transform.parent.GetComponentsInChildren<Image>() ) 
		{
			i.tintColor = new Color(i.tintColor.r, i.tintColor.g, i.tintColor.b, state?1f:0f);
		}

		foreach( TextElement t in transform.parent.GetComponentsInChildren<TextElement>() ) 
		{
			t.style.color = new Color(t.style.color.value.r, t.style.color.value.g, t.style.color.value.b, state ? 1f : 0f);
		}
	}

	// set to starting state
	void Start() 
	{
		updateSelection();
	}

	// Update is called once per frame
	void Update ()
	{
		Color colorDifference = _defaultColor - selectedColor;
		float colorCycle = Mathf.Abs(MyUtilities.Oscillation(colorCycleDifference, 1 / colorCycleSpeed, 0, Time.time));

		// cycle colors
		_currentItemText.style.color = selectedColor + (colorDifference * colorCycle);

		// move up / down
		MoveCursor();

		// when press action, do the thing of the current menu item
		OnActionDoThing();

		// exit option
		if (Input.GetButtonDown("Quit"))
		{
			Application.Quit();
		}
	}

	private void OnActionDoThing()
	{
		if (Joypad.Read.Buttons.Pressed("menuConfirm"))
		{
			Easily.PlaySound(selectSound);

			menuItems[_currentSelection].GetComponentInChildren<MenuAction>().Activate();
		}
	}

	private void MoveCursor()
	{
		if (_isActive && _allowDirectionalInput && userPressingDirection)
		{
			// add or subtract based on sign of horizontal 
			menuItems[_currentSelection].GetComponent<TextElement>().style.color = _defaultColor;

			int move = 0;

			if (Joypad.Read.Buttons.Pressed("up"))
			{
				move = 1;
			}
			else if (Joypad.Read.Buttons.Pressed("down"))
			{
				move = -1;
			}

			_currentSelection = (int)((_currentSelection + move) % menuItems.Length);
			//_currentSelection = (int) ( ( _currentSelection - (int) Mathf.Sign(Joypad.Read.Buttons.vertical) ) % menuItems.Length );

			// cycle through if negative
			if (_currentSelection == -1)
			{
				_currentSelection += menuItems.Length;
			}

			updateSelection();


			// adjust pointer position
			//			if (pointer) {
			//				pointer.transform.position = positionFromCurrentOption();
			//			}
		}

		// pause directional input if holding direction
		_allowDirectionalInput = !(Joypad.Read.Buttons.Held("up") || Joypad.Read.Buttons.Held("down"));
	}

	/// <summary>
	/// Updates the selection.
	/// </summary>
	void updateSelection() 
	{
		_currentItemText = menuItems[ _currentSelection ].GetComponent<TextElement>();

		_defaultColor = _currentItemText.style.color.value;
		_currentItemText.style.color = selectedColor;

		Easily.PlaySound(moveSound);
	}
}
