using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RoboRyanTron.Unite2017.Events
{
    public class GameIntEventListener : MonoBehaviour
    {
        [Serializable]
        public class UnityIntEvent : UnityEvent<int> { };

        [Tooltip("Event to register with.")]
        public GameIntEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityIntEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(int intParam)
        {
            Response.Invoke(intParam);
        }
    }
}
