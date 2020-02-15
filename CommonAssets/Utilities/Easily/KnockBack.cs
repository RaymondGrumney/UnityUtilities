using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CommonAssets
{
    public class KnockBack
    {
        private Rigidbody2D rigidbody;
        private Vector2 knockBack;
        private Vector2 relativeTo;

        public static KnockBack Knock(GameObject @this) => new KnockBack(@this);
        public static KnockBack Knock(Collision2D @this) => new KnockBack(@this);
        public static KnockBack Knock(Collider2D @this) => new KnockBack(@this);
        public static KnockBack Knock(Rigidbody2D @this) => new KnockBack(@this);

        public KnockBack() { }
        public KnockBack(GameObject @this)
        {
            this.rigidbody = @this.GetComponent<Rigidbody2D>();
        }
        public KnockBack(Collision2D @this)
        {
            this.rigidbody = @this.rigidbody;
        }
        public KnockBack(Collider2D @this)
        {
            this.rigidbody = @this.attachedRigidbody;
        }
        public KnockBack(Rigidbody2D @this)
        {
            this.rigidbody = @this;
        }

        public void From(GameObject relativeTo)
        {
            this.relativeTo = relativeTo.transform.position;
            _knockback();
        }
        public void From(Transform relativeTo)
        {
            this.relativeTo = relativeTo.position;
            _knockback();
        }
        public void From(Vector2 relativeTo)
        {
            this.knockBack = relativeTo;
            _knockback();
        }
        public void From(Collider2D relativeTo)
        {
            this.relativeTo = relativeTo.bounds.center;

            _knockback();
        }

        public KnockBack Back(Vector2 force)
        {
            knockBack = force;
            return this;
        }

        private void _knockback()
        {
            Vector2 them = rigidbody.gameObject.transform.position;
            float diff = Mathf.Sign(relativeTo.x - them.x);

            Vector2 theirvelocity = rigidbody.velocity;
            rigidbody.velocity = new Vector2(-knockBack.x * diff, theirvelocity.y + knockBack.y);
        }
    }
}
