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

        public static CoroutineRunner Init(IEnumerator coroutine = null, Action action = null)
        {
            GameObject coroutineObj = GameObject.Find("Coroutines");
            CoroutineRunner coroutineRunner;
            if (coroutineObj)
            {
                coroutineRunner = coroutineObj.AddComponent<CoroutineRunner>();
            }
            else
            {
                coroutineObj = Easily.Instantiate(new GameObject(), new Vector3());
                coroutineObj.name = "Coroutines";
                coroutineRunner = coroutineObj.AddComponent<CoroutineRunner>();
            }
            coroutineRunner._coroutine = coroutine;
            coroutineRunner._action = action;
            return coroutineRunner;
        }

        public void Now()
        {
            _do();
        }

        public void AtEndOfFrame()
        {
            StartCoroutine(_atEndOfFrame());
        }

        private IEnumerator _atEndOfFrame()
        {
            yield return new WaitForEndOfFrame();
            _do();
        }

        public void WaitForFixedUpdate()
        {
            StartCoroutine(_waitForFixedUpdate());
        }
        private IEnumerator _waitForFixedUpdate()
        {
            yield return new WaitForFixedUpdate();
            _do();
        }

        public CoroutineRunner After(float n)
        {
            _n = n;
            return this;
        }

        public void Seconds()
        {
            StartCoroutine(_waitForSecondsRealtime());
        }

        private IEnumerator _waitForSecondsRealtime()
        {
            yield return new WaitForSecondsRealtime(_n);
            _do();
        }

        private void _do()
        {
            if (_coroutine != null)
            {
                StartCoroutine(_coroutine);
            }
            else
            {
                _action();
            }
            Destroy(this);
        }
    }
}
