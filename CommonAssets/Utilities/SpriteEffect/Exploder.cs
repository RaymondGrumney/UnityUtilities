using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CommonAssets.Utilities
{
    public class Exploder
    {
		private Sprite _sprite;
		private int _pieces = 5;
		private float _fadeOutTimeInSeconds = 2f;
		private float _unitMass = 1f;
		private float _force = 1f;
		private Vector3? _worldPosition;

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
			
			Rect rect = _sprite.rect;

			// Get sprite width and height
			int width = Mathf.FloorToInt(rect.width);
			int height = Mathf.FloorToInt(rect.height);

			float unitWidth = (float)width / _pieces;
			float unitHeight = (float)height / _pieces;

			int partNumber = 0;

			Transform parent = Easily.Instantiate(new GameObject(), (Vector3)_worldPosition).transform;
			parent.gameObject.name = $"Exploded {_sprite.name}";
			parent.gameObject.AddComponent<DestroyWhenNoChildren>();

			for (int i = 0; i < width / unitWidth; i++)
			{
				for (int j = 0; j < height / unitHeight; j++)
				{
					// Cut out the needed part from the texture
					float x = rect.x + i * width / (width / unitWidth);
					float y = rect.y + j * height / (height / unitHeight);
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

					Sprite newSprite = Sprite.Create(texture: _sprite.texture,
													  rect: new Rect(x, y, rectWidth, rectHeight),
													  pivot: new Vector2(0f, 0f));

					
					GameObject gObject = new GameObject();
					SpriteRenderer sRenderer = gObject.AddComponent<SpriteRenderer>();
					Rigidbody2D rBody = gObject.AddComponent<Rigidbody2D>();
					gObject.transform.parent = parent.transform;
					gObject.name = _sprite.name + " part " + partNumber;

					SpriteEffects.Fade(gObject).Out().Then.Destroy();

					sRenderer.sprite = newSprite;
					sRenderer.color = Color.white;
					sRenderer.sortingLayerName = "Local Displays";

					float offsetX = _sprite.bounds.min.x + (sRenderer.sprite.rect.width / _sprite.pixelsPerUnit) * i;
					float offsetY = _sprite.bounds.min.y + (sRenderer.sprite.rect.width / _sprite.pixelsPerUnit) * j;

					// Place every GameObject as it was in the original sprite
					gObject.transform.position = (Vector3)_worldPosition + new Vector3(offsetX, offsetY, 0);


					rBody.velocity = new Vector2(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(3f, 6f)) * _force;
					rBody.freezeRotation = true;
					rBody.mass = _unitMass;

					partNumber++;
				}
			}
		}
	}
}
