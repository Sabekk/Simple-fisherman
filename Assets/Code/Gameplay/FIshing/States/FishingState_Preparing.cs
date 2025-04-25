using Gameplay.Character;
using UnityEngine;

namespace Gameplay.Fishing
{
    public class FishingState_Preparing : FishingStateBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public override FishingStateType Type => FishingStateType.PREPARING;
        private float Power { get; set; }
        private float MaxPower => Character.Data.MaxThrowPower;

        #endregion

        #region METHODS

        public override void Initialize(FishingAction fishingAction, CharacterBase character, Floater floater, params object[] parameters)
        {
            base.Initialize(fishingAction, character, floater, parameters);
            Power = 0;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Power < MaxPower)
                Power += Time.deltaTime;

            if (Power > MaxPower)
                Power = MaxPower;
        }

        protected override void HandleTriggeredAction()
        {
            base.HandleTriggeredAction();
            FishingAction.ChangeState(FishingStateType.THROWING, Power);
        }

        #endregion
    }
}