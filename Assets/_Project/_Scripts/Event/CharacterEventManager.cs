using System.Collections.Generic;
using GoldenFur.Common;
using UnityEngine;

namespace GoldenFur.Event
{
    public class CharacterEventManager : Singleton<CharacterEventManager>, ICharacterEventListener
    {
        public List<CharacterEventListener> Listeners;

        private void OnEnable()
        {
            Listeners = new List<CharacterEventListener>();
        }

        public void Add(CharacterEventListener listener)
        {
            Debug.Log("Adding listener.");
            Listeners.Add(listener);
        }

        public void Remove(CharacterEventListener listener)
        {
            Debug.Log("Removing listener.");
            Listeners.Remove(listener);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            foreach (var listener in Listeners)
            {
                Gizmos.DrawLine(this.transform.position, listener.transform.position);
            }
        }


        public void OnPlayerHit()
        {
            foreach (var listener in Listeners)
            {
                listener.OnPlayerHit();
            }
        }
    }
}