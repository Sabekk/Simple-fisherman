using Gameplay.Controller.Module;
using Gameplay.Fishing;
using Gameplay.Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Fishing
{
    public class FishingModule : ControllerModuleBase
    {
        #region VARIABLES

        public event Action OnPreparingAction;
        public event Action OnTriggeredAction;

        #endregion

        #region VARIABLES

        #endregion

        #region PROPERTIES

        public bool IsFishing => FishingManager.Instance.FishingAction != null;

        #endregion

        #region METHODS

        public override void AttachEvents()
        {
            base.AttachEvents();
            if (InputManager.Instance)
            {
                InputManager.Instance.CharacterInputs.OnActionPreparing += HandleActionPreparing;
                InputManager.Instance.CharacterInputs.OnActionTriggered += HandleActionTriggered;
            }
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            if (InputManager.Instance)
            {
                InputManager.Instance.CharacterInputs.OnActionPreparing -= HandleActionPreparing;
                InputManager.Instance.CharacterInputs.OnActionTriggered -= HandleActionTriggered;
            }
        }

        #region HANDLERS

        private void HandleActionPreparing()
        {
            OnPreparingAction?.Invoke();
        }

        private void HandleActionTriggered()
        {
            OnTriggeredAction?.Invoke();
        }

        #endregion

        #endregion
    }
}