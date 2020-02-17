using CommonAssets.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Common.CommonAssets
{
    public class ExplodeSpriteOnDestroy : MonoBehaviour
    {
        [Header("Explosion settings")]
        /// <summary>
        /// How many units to create. This number is squared (it is an N x N grid)
        /// Default is 5 (meaning 25 pieces total
        /// </summary>
        [Tooltip("How many units to create. This number is squared (it is an N x N grid)")]
        public int unitsSquared = 5;
        /// <summary>
        /// The mass assigned to each piece.
        /// Default is 1
        /// </summary>
        [Tooltip("The mass assigned to each piece.")]
        public float pieceMass = 1f;
        /// <summary>
        /// The force of the explosion.
        /// Default is 1.
        /// </summary>
        [Tooltip("The force of the explosion.")]
        public float force = 1f;
        /// <summary>
        /// How long (in seconds) each piece remains on screen before fading out.
        /// Default is 3.
        /// </summary>
        [Tooltip("How long each piece remains on screen before fading out.")]
        public float unitFadeOutTimeInSeconds = 3f;

        private void OnDestroy()
        {
            SpriteEffects.Explode( GetComponent<SpriteRenderer>().sprite)
                         .Into(unitsSquared).Pieces()
                         .At(Easily.Clone(transform.position))
                         .EachPieceWeighing(pieceMass)
                         .FadingOutAfter(unitFadeOutTimeInSeconds)
                         .WithForce(force);
        }
    }
}
