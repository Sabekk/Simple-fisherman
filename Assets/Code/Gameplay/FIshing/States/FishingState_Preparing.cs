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
        private float GainPowerSpeed => Character.Data.GainPowerSpeed;

        #endregion

        #region METHODS

        public override void Initialize(FishingAction fishingAction, CharacterBase character, params object[] parameters)
        {
            base.Initialize(fishingAction, character, parameters);
            Power = Character.Data.MinThrowPower;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Power < MaxPower)
                Power += Time.deltaTime * GainPowerSpeed;

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