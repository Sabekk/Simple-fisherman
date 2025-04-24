using UnityEngine;

namespace Gameplay
{
    public abstract class GameplayManager<T> : MonoSingleton<T>, IGameplayManager, IAttachableEvents where T : MonoBehaviour
    {
        public bool Initialized { get; protected set; }

        public virtual void Initialzie() { Initialized = true; }
        /// <summary>
        /// Late initialization with attaching events
        /// </summary>
        public virtual void LateInitialzie()
        {
            AttachEvents();
        }
        /// <summary>
        /// Clearing with detaching events
        /// </summary>
        public virtual void CleanUp()
        {
            DetachEvents();
            Initialized = false;
        }

        public virtual void AttachEvents() { }
        public virtual void DetachEvents() { }
    }
}