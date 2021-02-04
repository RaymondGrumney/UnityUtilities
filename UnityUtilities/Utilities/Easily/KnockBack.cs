using UnityUtilities.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityUtilities
{
    public class KnockBack
    {
        private Rigidbody2D _rigidbody;
        private Vector2 _force;
        private Vector2? _relativeTo;
        private float? _stunLength = 0f;

        public static KnockBack Knock(GameObject @this) => new KnockBack(@this);
        public static KnockBack Knock(Collision2D @this) => new KnockBack(@this);
        public static KnockBack Knock(Collider2D @this) => new KnockBack(@this);
        public static KnockBack Knock(Rigidbody2D @this) => new KnockBack(@this);

        public KnockBack() { }
        public KnockBack(GameObject @this)
        {
            this._rigidbody = @this.GetComponent<Rigidbody2D>();
            Easily.StartCoroutine(_knockback());
            
        }
        public KnockBack(Collision2D @this)
        {
            this._rigidbody = @this.rigidbody;
            Easily.StartCoroutine(_knockback());
        }
        public KnockBack(Collider2D @this)
        {
            this._rigidbody = @this.attachedRigidbody;
            Easily.StartCoroutine(_knockback());
        }
        public KnockBack(Rigidbody2D @this)
        {
            this._rigidbody = @this;
            Easily.StartCoroutine(_knockback());
        }

        public KnockBack From(GameObject relativeTo)
        {
            _relativeTo = relativeTo.transform.position;
            return this;
        }
        public KnockBack From(Transform relativeTo)
        {
            _relativeTo = relativeTo.position;
            return this;
        }
        public KnockBack From(Vector2 relativeTo)
        {
            _relativeTo = relativeTo;
            return this;
        }
        public KnockBack From(Collider2D relativeTo)
        {
            _relativeTo = relativeTo.bounds.center;
            return this;
        }

        public KnockBack Back(Vector2 force)
        {
            _force = force;
            return this;
        }

        public KnockBack StunningFor(float seconds)
        {
            _stunLength = seconds;
            return this;
        }

        private IEnumerator _knockback()
        {
            yield return new WaitForEndOfFrame();

            if ( _rigidbody != null && _relativeTo != null )
            {
                Vector2 them = _rigidbody.gameObject.transform.position;
                float diff = Mathf.Sign(((Vector2)_relativeTo).x - them.x);

                Vector2 theirVelocity = _rigidbody.velocity;
                _rigidbody.velocity = new Vector2(-_force.x * diff, theirVelocity.y + _force.y);

                if (_stunLength != null)
                {
                    _rigidbody.gameObject.BroadcastMessage("Stun", _stunLength);
                }
            }
        }
    }
}
