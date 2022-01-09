using UnityEngine;
using System;

namespace UnityUtilities.Utilities
{
    [Serializable]
    public class Timer
    {
        /// <summary>
        /// When the timer will end.
        /// </summary>
        public float EndTime { get; set; } = 0f;

        private float _runTime = 0f;

        /// <summary>
        /// If allowing the timer to count the Time.time from 0 to EndTime
        /// </summary>
        public bool CountFromStartOfScene = true;

        /// <summary>
        /// Starts the timer, running for runTime.
        /// </summary>
        /// <param name="runTime">How long to the timer returns true.</param>
        /// <param name="CountFromStartOfScene">Whether to allow.</param>
        /// <returns>The time the timer will run out.</returns>
        public float Start(float runTime, bool CountFromStartOfScene = true)
        {
            this.CountFromStartOfScene = CountFromStartOfScene;
            _runTime = runTime;
            return EndTime = Time.time + runTime;
        }
        /// <summary>
        /// Starts the timer, running for runTime.
        /// </summary>
        /// <param name="runTime">How long to the timer returns true.</param>
        /// <param name="CountFromStartOfScene">Whether to allow.</param>
        /// <returns>The time the timer will run out.</returns>
        public float RunFor(float runTime, bool CountAtStartOfScene = true)
            => this.Start(runTime, CountAtStartOfScene);

        /// <summary>
        /// If the timer has run out.
        /// </summary>
        public bool Over
        {
            get => (!CountFromStartOfScene && Time.time < _runTime) || EndTime < Time.time;
        }

        /// <summary>
        /// If the timer has run out.
        /// </summary>
        public bool Complete
            => this.Over;
            
        /// <summary>
        /// If the timer has run out.
        /// </summary>
        public bool Expired
            => this.Over;
    }
}