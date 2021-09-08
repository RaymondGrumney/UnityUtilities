using System;
using UnityEngine;

namespace CommonAssets.Utilities
{
    /// <summary>
    /// Gets things
    /// </summary>
    public class Getter<T>
    {
        private GetterType _getterType;
        private Transform _from;
        private object _query;

        public Getter(GetterType getterType) 
        {
            _getterType = getterType;
        }

        /// <summary>
        /// The transform to get from
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public Getter<T> From(Transform transform)
        {
            _from = transform;
            return this;
        }

        /// <summary>
        /// The GameObject to get from
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public Getter<T> From(GameObject gameObject)
        {
            _from = gameObject.transform;
            return this;
        }

        /// <summary>
        /// The resulting game object
        /// </summary>
        public T component
            => gameObject.GetComponent<T>();

        public GameObject gameObject
        {
            get
            {
                if (_from is null)
                    throw new ArgumentNullException("From");
                if (_query is null)
                    throw new ArgumentNullException("Query");

                switch (_getterType) 
                {
                    case GetterType.ChildByName:
                        for(int i = 0; i < _from.childCount; i++)
                        {
                            Transform child = _from.GetChild(i);
                            if (child.name == (string)_query)
                            {
                                return child.gameObject; ;
                            }
                        }
                        return null;

                    case GetterType.Invalid:
                        throw new ArgumentNullException("getterType");
                    default: 
                        throw new ArgumentNullException("getterType");
                }
            }
        }

        /// <summary>
        /// What to search for
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Getter<T> Query(object query)
        {
            _query = query;
            return this;
        }
    }
}
