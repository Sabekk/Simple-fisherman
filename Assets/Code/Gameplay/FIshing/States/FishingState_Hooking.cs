using Gameplay.Character;
using UnityEngine;

namespace Gameplay.Fishing
{
    public class FishingState_Hooking : FishingStateBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public override FishingStateType Type => FishingStateType.HOOKING;

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
        }

        public override void Activate()
        {
            base.Activate();

            //TODO Add minigame

            bool result = Random.Range(0, 100f) >= 50f;
            FishingAction.ChangeState(FishingStateType.FINISHING, Floater, result);
        }

        #endregion
    }
}