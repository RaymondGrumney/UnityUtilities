using UnityUtilities.Utilities;
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
        [Tooltip("How many units to create. This number is squared (it is an N x N grid)")]
        public int unitsSquared = 5;
        [Tooltip("The mass assigned to each piece.")]
        public float pieceMass = 1f;
        [Tooltip("The force of the explosion.")]
        public float force = 1f;
        [Tooltip("How long each piece remains on screen before fading out.")]
        public float unitFadeOutTimeInSeconds = 3f;
        private bool isQuitting = false;

        void OnApplicationQuit()
        {
            isQuitting = true;
        }

        private void OnDestroy()
        {
            if(!isQuitting)
            SpriteEffects.Explode( GetComponent<SpriteRenderer>().sprite )
                         .Into( unitsSquared ).Pieces()
                         .At( Easily.Clone( transform.position ))
                         .EachPieceWeighing( pieceMass )
                         .FadingOutAfter( unitFadeOutTimeInSeconds )
                         //.AndDisableRotationOfPieces()
                         .Rotated(transform.rotation)
                         .WithForce( force );
        }
    }
}
