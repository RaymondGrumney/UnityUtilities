using UnityEngine;

namespace CommonAssets.Utilities
{
    public class DestroyWhenNoChildren : MonoBehaviour
    {
        private void Update()
        {
            if(transform.childCount == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}