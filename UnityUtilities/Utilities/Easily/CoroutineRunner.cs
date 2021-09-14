using System;
using System.Collections;
using UnityEngine;

namespace UnityUtilities.Utilities {
    public class CoroutineRunner : MonoBehaviour {
        protected IEnumerator _coroutine;
        protected Action _action;
        private float _n = 0;

        public static CoroutineRunner Init(IEnumerator coroutine = null, Action action = null ) {
            GameObject coroutineObj = GameObject.Find("Coroutines");
            CoroutineRunner coroutineRunner;
            if (coroutineObj) {
                coroutineRunner = coroutineObj.GetComponent<CoroutineRunner>() 
                               ?? coroutineObj.AddComponent<CoroutineRunner>();
                // if (coroutineRunner is null) {
                //     coroutineRunner = coroutineObj.AddComponent<CoroutineRunner>();
                // }
            }
            else {
                coroutineObj = Easily.Instantiate(new GameObject(), new Vector3());
                coroutineObj.name = "Coroutines";
                coroutineRunner = coroutineObj.AddComponent<CoroutineRunner>();
            }
            coroutineRunner._coroutine=coroutine;
            coroutineRunner._action=action;
            return coroutineRunner;
        }

        public void Now() {
            _do();
        }

        public CoroutineRunner After(float n) {
            _n = n;
            return this;
        }

        public IEnumerator Seconds() {
            yield return new WaitForSecondsRealtime(_n);
            _do();
        }

        private IEnumerator doAction() {
			yield return new WaitForEndOfFrame();
            _action();
        }

        private void _do() {
            if(_coroutine != null) {
                StartCoroutine(_coroutine);
            }
            else {
                StartCoroutine(doAction());
            }
        }
    }
}
