using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityUtilities.Utilities
{
    public class Exploder
    {
		private bool freezeRotation = false;
		private Sprite _sprite;
		private int _pieces = 5;
		private float _fadeOutTimeInSeconds = 2f;
		private float _unitMass = 1f;
		private float _force = 1f;
		private Vector3? _worldPosition;
		private Quaternion rotation;

		/// <summary>
		/// Explodes 
		/// </summary>
		/// <param name="sprite"></param>
		public Exploder(Sprite sprite)
		{
			_sprite = sprite;
			Easily.StartCoroutine( Splode() );
		}

		/// <summary>
		/// How many pieces, along one edge, to cut the sprite into on explosion. 
		/// The actual number of pieces will be the square of this number.
		/// (Default is 5x5)
		/// </summary>
		/// <param name="pieces"></param>
		/// <returns></returns>
		public Exploder Into(int pieces)
		{
			_pieces = pieces;
			return this;
		}

		/// <summary>
		/// Syntactic Sugar
		/// </summary>
		public Exploder Pieces() => this;
 
		/// <summary>
		/// Defines how long the pieces will wait to fade out.
		/// (Default is 2)
		/// </summary>
		/// <param name="seconds"></param>
		/// <returns></returns>
		public Exploder FadingOutAfter(float seconds)
		{
			_fadeOutTimeInSeconds = seconds;
			return this;
		}

		/// <summary>
		/// Syntactic Sugar
		/// </summary>
		public Exploder Seconds() => this;

		/// <summary>
		/// Sets the mass of each piece.
		/// ( Default is 1 )
		/// </summary>
		/// <param name="mass"></param>
		/// <returns></returns>
		public Exploder EachPieceWeighing(float mass)
		{
			_unitMass = mass;
			return this;
		}

		/// <summary>
		/// The force of the explosion. 
		/// ( Default is 1. )
		/// </summary>
		/// <param name="forceMultiplier"></param>
		/// <returns></returns>
		public Exploder WithForce(float forceMultiplier)
		{
			_force = forceMultiplier;
			return this;
		}
		
		/// <summary>
		/// Where in the world to explode at.
		/// </summary>
		/// <param name="position"></param>
		/// <returns></returns>
		public Exploder At(Vector3 position)
		{
			_worldPosition = Easily.Clone( position );
			return this;
		}
		
		public Exploder Rotated(Quaternion rotation)
		{
			this.rotation = rotation;
			return this;
		}

		public Exploder AndDisableRotationOfPieces()
		{
			freezeRotation = false;
			return this;
		}


		/// <summary>
		/// Does the work.
		/// </summary>
		/// <returns></returns>
		private IEnumerator Splode()
		{
			yield return new WaitForEndOfFrame();

			if (_sprite is null)
			{
				throw new ArgumentNullException("Sprite cannot be null.");
			}

			if (_worldPosition is null)
			{
				throw new ArgumentNullException("Must set position in the world with At(Vector3 position).");
			}

			InitializeBits( out Rect rect, out int width, out int height, out float unitWidth
				          , out float unitHeight, out int partNumber );
			Transform parent = CreateParent();

			for (int i = 0; i < width / unitWidth; i++)
			{
				for (int j = 0; j < height / unitHeight; j++)
				{
					CreateSprite(out Sprite newSprite, ref rect, width, height, unitWidth, unitHeight, i, j);
					CreateGameObject(partNumber
								, parent
								, out GameObject gObject
								, out SpriteRenderer sRenderer
								, out Rigidbody2D rBody);
					AssignSprite(newSprite, sRenderer);
					PlaceAsIfWereOriginalSPrite(i, j, gObject, sRenderer);
					SetupRigidbody(rBody);
					SpriteEffects.Fade(gObject).Out().Over(_fadeOutTimeInSeconds).Then.Destroy();


					partNumber++;
				}
			}
		}

		private void InitializeBits(out Rect rect, out int width, out int height, out float unitWidth, out float unitHeight, out int partNumber)
		{
			rect = _sprite.rect;

			// Get sprite width and height
			width = Mathf.FloorToInt(rect.width);
			height = Mathf.FloorToInt(rect.height);
			unitWidth = (float)width / _pieces;
			unitHeight = (float)height / _pieces;
			partNumber = 0;
		}

		private void SetupRigidbody(Rigidbody2D rBody)
		{
			float Random_X = UnityEngine.Random.Range(-2f, 2f);
			float Random_Y = UnityEngine.Random.Range(3f, 6f);
			if(freezeRotation)
			{
				rBody.freezeRotation = freezeRotation;
				rBody.velocity = new Vector2(Random_X, Random_Y) * _force;
			}
			else
			{
				float Random_Z = UnityEngine.Random.Range(-60f, 60f);
				rBody.velocity = new Vector3(Random_X, Random_Y) * _force;
				rBody.angularVelocity = Random_Z * _force;
			}
			rBody.mass = _unitMass;
		}

		private void PlaceAsIfWereOriginalSPrite(int i, int j, GameObject gObject, SpriteRenderer sRenderer)
		{
			float offsetX = _sprite.bounds.min.x + (sRenderer.sprite.rect.width / _sprite.pixelsPerUnit) * i;
			float offsetY = _sprite.bounds.min.y + (sRenderer.sprite.rect.width / _sprite.pixelsPerUnit) * j;

			// Place every GameObject as it was in the original sprite
			gObject.transform.position = (Vector3)_worldPosition + new Vector3(offsetX, offsetY, 0);
		}

		private static void AssignSprite(Sprite newSprite, SpriteRenderer sRenderer)
		{
			sRenderer.sprite = newSprite;
			sRenderer.color = Color.white;
			sRenderer.sortingLayerName = "Local Displays";
		}

		private Transform CreateParent()
		{
			GameObject parentObject = new GameObject();
			Transform parent = parentObject.transform;
			parentObject.name = $"Exploded {_sprite.name}";
			parentObject.AddComponent<DestroyWhenNoChildren>();
			parent.rotation = rotation;
			parent.position = (Vector3) _worldPosition;
			return parent;
		}

		private void CreateGameObject( int partNumber
			                         , Transform parent
			                         , out GameObject gObject
			                         , out SpriteRenderer sRenderer
			                         , out Rigidbody2D rBody )
		{
			gObject = new GameObject();
			sRenderer = gObject.AddComponent<SpriteRenderer>();
			rBody = gObject.AddComponent<Rigidbody2D>();
			gObject.transform.parent = parent.transform;
			gObject.name = _sprite.name + " part " + partNumber;
		}

		private void CreateSprite( out Sprite newSprite
			                     , ref Rect rect
			                     , int spriteWidth
			                     , int spriteHeight
			                     , float unitWidth
			                     , float unitHeight
			                     , int offsetX
			                     , int offsetY )
		{

			// Cut out the needed part from the texture
			float x = rect.x + offsetX * spriteWidth / (spriteWidth / unitWidth);
			float y = rect.y + offsetY * spriteHeight / (spriteHeight / unitHeight);
			int rectWidth = Mathf.CeilToInt(unitWidth);
			int rectHeight = Mathf.CeilToInt(unitHeight);

			if (x + rectWidth > _sprite.texture.width)
			{
				rectWidth = Mathf.FloorToInt(_sprite.texture.width - x);
			}
			if (y + rectHeight > _sprite.texture.height)
			{
				rectHeight = Mathf.FloorToInt(_sprite.texture.height - y);
			}

			newSprite = Sprite.Create(texture: _sprite.texture,
											  rect: new Rect(x, y, rectWidth, rectHeight),
											  pivot: new Vector2(0f, 0f));
		}
	}
}
