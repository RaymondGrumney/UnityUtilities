using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CommonAssets.Utilities
{
    public class Fader
    {
        // TODO: Enable Fade In
        // TODO: Sprite Ocillation
        // TODO: Remove script after finished fading in/out

        private GameObject _gameObject;
        private float _fadeOutTimeInSeconds = 1f;
        private bool _thenDestroy = false;
        private bool _fadeOut = false;
        private bool _fadeIn = false;   // untested
        private int _modifier = -1;
        private float _minAlpha = 0f;   // checked but not settable
        private float _maxAlpha = 1f;   // not used yet
        private FaderComponent component;

        public Fader(GameObject gameObject)
        {
            _gameObject = gameObject;
            component = gameObject.AddComponent<FaderComponent>();
            component.StartCoroutine(Initialize());
        }

        public Fader() 
        {
        }

        public Fader Fade(GameObject gameObject)
        {
            _gameObject = gameObject;
            return this;
        }

        public Fader Over(float nSeconds)
        {
            _fadeOutTimeInSeconds = nSeconds;
            return this;
        }

        public Fader Destroy()
        {
            _thenDestroy = true;
            return this;
        }

        public Fader Out()
        {
            _fadeOut = true;

            if(!_fadeIn)
            {
                _modifier = -1;
            }

            return this;
        }

        public Fader In()
        {
            throw new NotImplementedException("It probably works but hasn't been tested.");

            _fadeIn = true;

            if(!_fadeOut)
            {
                _modifier = 1;
            }

            return this;
        }

        public Fader Then => this;

        private IEnumerator Initialize()
        {
            yield return new WaitForEndOfFrame();

            if (!_fadeOut && !_fadeIn)
            {
                throw new ArgumentException("In() and/or Out() must be called to define behavior.");
            }
            
            component.fadeOutTimeInSeconds  = _fadeOutTimeInSeconds;
            component.thenDestroy           = _thenDestroy;
            component.fadeIn                = _fadeIn;   // untested
            component.fadeOut               = _fadeOut;
            component.modifier              = _modifier;
            component.minAlpha              = _minAlpha; // currently checked but not setable
            component.maxAlpha              = _maxAlpha; // currently unchecked
        }
    }
}
