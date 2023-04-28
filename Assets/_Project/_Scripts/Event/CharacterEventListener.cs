using UnityEngine;

namespace GoldenFur.Event
{
    public abstract class CharacterEventListener : MonoBehaviour, ICharacterEventListener
    {
        public virtual void OnPlayerHit()
        {
        }
    }
}