using UnityEngine;

namespace CommonAssets.Utilities
{
    /// <summary>
    /// Applys effects to sprites
    /// </summary>
    public class SpriteEffects
    {
        /// <summary>
        /// Explodes the sprite
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public static Exploder Explode(Sprite sprite)
            => new Exploder(sprite);

        /// <summary>
        /// Fades the sprite in or out (in currently not enabled)
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static Fader Fade(GameObject gameObject)
            => new Fader(gameObject);

        // TODO: cut in half
    }
}
