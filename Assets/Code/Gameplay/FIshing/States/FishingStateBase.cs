using Gameplay.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Fishing
{
    public abstract class FishingStateBase : IAttachableEvents, IDisposable
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public abstract FishingStateType Type { get; }
        protected FishingAction FishingAction { get; set; }
        protected CharacterBase Character { get; set; }
        protected Floater Floater { get; set; }

        #endregion

        #region METHODS

        public virtual void Initialize(FishingAction fishingAction, CharacterBase character, params object[] parameters)
        {
            FishingAction = fishingAction;
            Character = character;

            AttachEvents();
        }

        public virtual void Dispose()
        {
            DetachEvents();
        }

        public virtual void OnUpdate() { }

        public virtual void AttachEvents()
        {
            if (Character != null)
            {
                Character.FishingController.FishingModule.OnPreparingAction += HandlePreparingAction;
                Character.FishingController.FishingModule.OnTriggeredAction += HandleTriggeredAction;
            }
        }

        public virtual void DetachEvents()
        {
            if (Character != null)
            {
                Character.FishingController.FishingModule.OnPreparingAction -= HandlePreparingAction;
                Character.FishingController.FishingModule.OnTriggeredAction -= HandleTriggeredAction;
            }
        }


        #region HANLDERS

        protected virtual void HandlePreparingAction() { }
        protected virtual void HandleTriggeredAction() { }

        #endregion

        #endregion
    }
}