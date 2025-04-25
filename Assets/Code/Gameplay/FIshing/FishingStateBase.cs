using Gameplay.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Fishing
{
    public abstract class FishingStateBase : IAttachableEvents, IDisposable
    {
        #region ACTION

        /// <summary>
        /// Finish event with information about next step
        /// </summary>
        public event Action<FishingStateType> OnFinish;

        #endregion

        #region VARIABLES

        #endregion

        #region PROPERTIES

        public abstract FishingStateType Type { get; }
        protected FishingAction FishingAction { get; set; }
        protected CharacterBase Character { get; set; }
        protected Floater Floater { get; set; }

        #endregion

        #region METHODS

        public virtual void Initialize(FishingAction fishingAction, CharacterBase character, Floater floater)
        {
            FishingAction = fishingAction;
            Character = character;
            Floater = floater;

            AttachEvents();
        }

        public virtual void Dispose()
        {
            DetachEvents();
        }

        public virtual void AttachEvents()
        {

        }

        public virtual void DetachEvents()
        {

        }


        #endregion
    }
}