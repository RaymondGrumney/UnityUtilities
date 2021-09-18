using System;
using System.Collections;
using UnityEngine;

namespace UnityUtilities.Utilities 
{
    public class CoroutineRunner : MonoBehaviour 
    {
        protected IEnumerator _coroutine;
        protected Action _action;
        private float _n = 0;

        public static CoroutineRunner Init(IEnumerator coroutine = null, Action action = null ) 
        {
            // if (action !=null) Debug.Log($"Init({coroutine}, {action}), {action?.Target}");
            GameObject coroutineObj = GameObject.Find("Coroutines");
            CoroutineRunner coroutineRunner;
            if (coroutineObj) 
            {
                coroutineRunner = coroutineObj.AddComponent<CoroutineRunner>();
                // if (coroutineRunner is null) {
                //     coroutineRunner = coroutineObj.AddComponent<CoroutineRunner>();
                // }
            }
            else 
            {
                coroutineObj = Easily.Instantiate(new GameObject(), new Vector3());
                coroutineObj.name = "Coroutines";
                coroutineRunner = coroutineObj.AddComponent<CoroutineRunner>();
            }
            coroutineRunner._coroutine=coroutine;
            coroutineRunner._action=action;
            return coroutineRunner;
        }

        public void Now() 
        {
            _do();
        }

        public CoroutineRunner After(float n) 
        {
            // if (_action !=null) Debug.Log($"After({n}), {_action?.Target}");
            _n = n;
            return this;
        }

        public void Seconds() 
        {
            if (_action !=null) Debug.Log($"Seconds, {_action?.Target}");
            StartCoroutine("_wait");
        }

        private IEnumerator _wait()
        {
            // if (_action !=null) Debug.Log($"_wait({_n}), {_action?.Target}");
            yield return new WaitForSecondsRealtime(_n);
            // if (_action !=null) Debug.Log($"waited {_n}, {_action?.Target}");
            _do();
        }

        private void _do() 
        {
            // if (_action !=null) Debug.Log($"_do() {_action?.Target}, {_action?.Target}");
            if(_coroutine != null) 
            {
                StartCoroutine(_coroutine);
            }
            else 
            {
                // if (_action !=null) Debug.Log($"({_action?.Target}), {_action?.Target}");
                _action();
            }
            Destroy(this);
        }
    }
}
