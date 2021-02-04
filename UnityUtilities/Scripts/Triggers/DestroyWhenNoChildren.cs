using UnityEngine;

namespace UnityUtilities.Utilities
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