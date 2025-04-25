using Gameplay.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Fishing
{
    public class FishingState_Finishing : FishingStateBase
    {
        #region ACTION

        public static event Action<bool> OnFinalizeFishing;

        #endregion

        #region PROPERTIES

        public override FishingStateType Type => FishingStateType.FINISHING;
        private bool Result { get; set; }

        #endregion

        #region METHODS

        public override void Initialize(FishingAction fishingAction, CharacterBase character, params object[] parameters)
        {
            base.Initialize(fishingAction, character, parameters);

            if (parameters.Length > 0)
            {
                if (parameters[0] is Floater throwedFloater)
                    Floater = throwedFloater;
            }

            if (parameters.Length > 1)
            {
                if (parameters[1] is bool result)
                    Result = result;
            }
        }

        public override void Activate()
        {
            base.Activate();
            FishingAction.ChangeState(FishingStateType.NONE);
        }

        public override void Dispose()
        {
            base.Dispose();
            if (Floater != null)
                Floater.DestroyFloater();
            PublishOfResult();
        }

        private void PublishOfResult()
        {
            OnFinalizeFishing?.Invoke(Result);
        }

        #endregion
    }
}