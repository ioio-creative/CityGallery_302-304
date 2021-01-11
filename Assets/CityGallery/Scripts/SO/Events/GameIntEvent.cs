using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboRyanTron.Unite2017.Events
{
    [CreateAssetMenu]
    public class GameIntEvent : ScriptableObject
    {
        [SerializeField]
        private int sentInt;

        public int LastSent => sentInt;
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        protected readonly List<GameIntEventListener> eventListeners =
            new List<GameIntEventListener>();

        public void Raise()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(sentInt);
        }

        public void Raise(int data)
        {
            sentInt = data;
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(sentInt);
        }

        public void RegisterListener(GameIntEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(GameIntEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}
