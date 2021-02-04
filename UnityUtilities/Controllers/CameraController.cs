using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	[Tooltip("The character the camera is following.")]
	public GameObject target;

	[Tooltip("The speed the camera moves at.")]
	public float speed;

	[Header("Camera Boundaries")]
	[Tooltip("Whether to use camera boundaries. Can only be set if boundingObject is set.")]
	public bool usesCameraBounds = false;

	[Tooltip("The object the camera is bounded by.")]
	public GameObject boundingObject;

	// the x/y boundaries for the camera
	private float _xBoundaryMin;
	private float _xBoundaryMax;
	private float _yBoundaryMin;
	private float _yBoundaryMax;
	private Vector3 _initialPosition;

	// make sure there's a bounding object before enabling the useCameraBounds flag
	void OnValidate()
	{
		if (!boundingObject)
		{
			usesCameraBounds = false;
		}
	}

	void Awake()
	{
		// calculate camera height and width
		float cameraHeight = GetComponent<Camera>().orthographicSize;
		float cameraWidth = cameraHeight * ((float)Screen.width / (float)Screen.height);
		_initialPosition = transform.position;
		
		// if the bounding object is assigned
		if (boundingObject != null)
		{
			// the center of the bounding object
			float boundaryCenterX = boundingObject.GetComponent<SpriteRenderer>().bounds.center.x;
			float boundaryCenterY = boundingObject.GetComponent<SpriteRenderer>().bounds.center.y;

			// the height and width of the bounding object
			float boundaryExtentsX = boundingObject.GetComponent<SpriteRenderer>().bounds.extents.x;
			float boundaryExtentsY = boundingObject.GetComponent<SpriteRenderer>().bounds.extents.y;

			// calculate the min and max boundaries
			_xBoundaryMax = boundaryCenterX + boundaryExtentsX - cameraWidth;
			_yBoundaryMax = boundaryCenterY + boundaryExtentsY - cameraHeight;
			_xBoundaryMin = boundaryCenterX + cameraWidth - boundaryExtentsX;
			_yBoundaryMin = boundaryCenterY + cameraHeight - boundaryExtentsY;
		}

	}

	// LateUpdate is called after Update each frame
	// Updates camera position based on player position, limited by boundingObject it usesCameraBounds
	void Update()
	{
		SnapTo(target);
	}


	/// <summary>
	/// Slides toward the passed object.
	/// </summary>
	/// <param name="moveTowardThis">What to slide toward</param>
	public void SlideToward(GameObject moveTowardThis)
	{
		// the fraction of the distance toward the player a single move of assigned speed would take;
		float slideDist = speed / Vector3.Distance(transform.position, moveTowardThis.transform.position);

		// the vector of movement of slideDist toward the target position; note that if slideDist > 1, this will point to the exact target position
		Vector3 moveVector = Vector3.Lerp(this.transform.position, moveTowardThis.transform.position, slideDist);

		// TODO: handle when the character dies
		transform.position = PositionBounded(moveVector);
	}

	/// <summary>
	/// If using camera bounds, clamp the camera position to within the bounding object
	/// </summary>
	/// <returns>The bounded.</returns>
	/// <param name="position">The position bounded by the Bounding Object.</param>
	Vector3 PositionBounded(Vector3 position)
	{
		// the current x, y & z positions
		float x = position.x;
		float y = position.y;
		float z = _initialPosition.z;       // note that this will not be altered

		if (usesCameraBounds)
		{
			x = Mathf.Clamp(position.x, _xBoundaryMin, _xBoundaryMax);
			y = Mathf.Clamp(position.y, _yBoundaryMin, _yBoundaryMax);
		}

		return new Vector3(x, y, z);
	}

	/// <summary>
	/// Snaps the position to the sent object
	/// </summary>
	/// <param name="gObj">G object.</param>
	public void SnapTo(GameObject gObj)
	{
		// Set the position of the camera's transform to be the same as the player's
		Vector3 pos = gObj.transform.position;

		transform.position = PositionBounded(pos);
	}

	private void RotateOrWhatever(GameObject target)
	{
		transform.rotation = target.transform.rotation;
	}
}