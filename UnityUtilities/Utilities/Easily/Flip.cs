using System;
using UnityEngine;

namespace UnityUtilities.Utilities
{
    public class Flip
    {
        private GameObject _gameObject;
        private string _allowedAxes = "xyz";
        private string _flipAxis;

        
        public Flip(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public Flip On(string axis)
        {
            string strippedAxis = axis.ToLower().Replace("axis", "").Trim();
            if (_allowedAxes.Contains(strippedAxis))
            {
                _flipAxis = strippedAxis;
            }
            else
            {
                throw new Exception($"The requested axis ({strippedAxis}) is not permitted.");
            }


            float x = 0f;
            float y = 0f;
            float z = 0f;

            switch (_flipAxis)
            {
                case "x":
                    x = 180f;
                    break;
                case "y":
                    y = 180f;
                    break;
                case "z":
                    z = 180f;
                    break;
            }

            _gameObject.transform.Rotate(new Vector3(x, y, z));

            return this;
        }
    }
}