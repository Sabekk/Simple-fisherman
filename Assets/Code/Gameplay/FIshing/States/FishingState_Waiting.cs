using Gameplay.Character;
using UnityEngine;

namespace Gameplay.Fishing
{
    public class FishingState_Waiting : FishingStateBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        private float WaitingTime { get; set; }
        private float CurrentTime { get; set; }
        public override FishingStateType Type => FishingStateType.WAITING;

        #endregion

        #region METHODS

        public override void Initialize(FishingAction fishingAction, CharacterBase character, params object[] parameters)
        {
            base.Initialize(fishingAction, character, parameters);

            CurrentTime = 0;
            //TODO Move to fishing setting
            WaitingTime = Random.Range(0f, 10f);

            if (parameters.Length > 0)
            {
                if (parameters[0] is Floater throwedFloater)
                    Floater = throwedFloater;
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            CurrentTime += Time.deltaTime;

            if (CurrentTime >= WaitingTime)
                FishingAction.ChangeState(FishingStateType.FIRST_BITE, Floater);
        }

        protected override void HandleTriggeredAction()
        {
            base.HandleTriggeredAction();
            FishingAction.ChangeState(FishingStateType.FINISHING, Floater, false);
        }

        #endregion
    }
}