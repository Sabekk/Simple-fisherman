using Gameplay.Character;
using UnityEngine;

namespace Gameplay.Fishing
{
    public class FishingState_FirstBite : FishingStateBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        private float TimeToReact { get; set; }
        private float CurrentTime { get; set; }
        private float BitingTime { get; set; }
        private float CurrentBitingTime { get; set; }
        public override FishingStateType Type => FishingStateType.FIRST_BITE;

        #endregion

        #region METHODS

        public override void Initialize(FishingAction fishingAction, CharacterBase character, params object[] parameters)
        {
            base.Initialize(fishingAction, character, parameters);

            //TODO Move to fishing setting
            TimeToReact = Random.Range(3f, 5f);
            BitingTime = 1f;

            //For instantly first bite action
            CurrentBitingTime = BitingTime;
            CurrentTime = 0;

            if (parameters.Length > 0)
            {
                if (parameters[0] is Floater throwedFloater)
                    Floater = throwedFloater;
            }
        }

        public override void Activate()
        {
            base.Activate();
            StartBiteFloater();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            CurrentTime += Time.deltaTime;

            if (CurrentTime >= TimeToReact)
                FishingAction.ChangeState(FishingStateType.FINISHING, Floater, false);

            if (CurrentBitingTime >= BitingTime)
            {
                StartBiteFloater();
                CurrentBitingTime = 0;
            }
        }

        protected override void HandleTriggeredAction()
        {
            base.HandleTriggeredAction();
            FishingAction.ChangeState(FishingStateType.HOOKING, Floater);
        }


        private void StartBiteFloater()
        {
            if (Floater == null)
            {
                FishingAction.ChangeState(FishingStateType.NONE);
                return;
            }

            Floater.MakeBite();
        }

        #endregion
    }
}