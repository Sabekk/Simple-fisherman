using Gameplay.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Fishing
{
    public class FishingState_Throwing : FishingStateBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public override FishingStateType Type => FishingStateType.THROWING;
        private float Power { get; set; }
        private Transform ThrowPoint { get; set; }

        #endregion

        #region METHODS

        public override void Initialize(FishingAction fishingAction, CharacterBase character, params object[] parameters)
        {
            base.Initialize(fishingAction, character, parameters);

            if (parameters.Length > 0)
            {
                if (parameters[0] is float powerToSet)
                    Power = powerToSet;
            }

            if (Power == 0)
                Power = Character.Data.MinThrowPower;

            ThrowPoint = Character.CharacterInGame.FishingRod.StartBobberPos;
        }

        public override void Activate()
        {
            base.Activate();
            ThrowFloater();
        }

        private void ThrowFloater()
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            Vector3 targetPoint;

            targetPoint = ray.origin + ray.direction * Character.Data.MaxThrowPower;

            Vector3 direction = (targetPoint - ThrowPoint.position).normalized;

            direction += Vector3.up * 0.2f;
            direction.Normalize();

            CreateFloater();

            if (Floater.Rigidbody != null)
                Floater.Rigidbody.AddForce(direction * Power, ForceMode.Impulse);
        }

        private void CreateFloater()
        {
            Floater = Character.CharacterInGame.FishingRod.SpawnAndGetFloater();
            if (Floater)
                AttachEventsOfFloater();
            else
                FishingAction.ChangeState(FishingStateType.NONE);
        }

        private void AttachEventsOfFloater()
        {
            if (Floater == null)
                return;

            Floater.OnFloorHit += HandleFloaterFloorHit;
        }

        private void DetachEventsOfFloater()
        {
            if (Floater == null)
                return;

            Floater.OnFloorHit -= HandleFloaterFloorHit;
        }

        #region HANDLERS

        private void HandleFloaterFloorHit()
        {
            DetachEventsOfFloater();
            FishingAction.ChangeState(FishingStateType.WAITING, Floater);
        }

        #endregion

        #endregion
    }
}