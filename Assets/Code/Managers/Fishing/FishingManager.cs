using Gameplay.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Fishing
{
    public class FishingManager : GameplayManager<FishingManager>
    {
        #region VARIABLES

        private FishingAction fishingAction;

        #endregion

        #region PROPERTIES

        public FishingAction FishingAction => fishingAction;
        public Player Player => CharacterManager.Instance.Player;

        #endregion

        #region METHODS

        public override void Initialzie()
        {
            base.Initialzie();
        }

        public override void AttachEvents()
        {
            base.AttachEvents();
            if (CharacterManager.Instance)
                CharacterManager.Instance.OnPlayerCreated += HandlePlayerCreated;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            if (CharacterManager.Instance)
                CharacterManager.Instance.OnPlayerCreated += HandlePlayerCreated;

            DetachEventsOfPlayer();
        }

        private void AttachEventsOfPlayer()
        {
            if (Player != null)
            {
                Player.FishingController.FishingModule.OnPreparingAction += HandlePreparingAction;
                Player.FishingController.FishingModule.OnTriggeredAction += HandleTriggeredAction;
            }
        }

        private void DetachEventsOfPlayer()
        {
            if (Player != null)
            {
                Player.FishingController.FishingModule.OnPreparingAction -= HandlePreparingAction;
                Player.FishingController.FishingModule.OnTriggeredAction -= HandleTriggeredAction;
            }
        }

        private void InitializeFishingAction()
        {
            fishingAction = new();
            fishingAction.Initialize(Player);
        }

        #region HANDLERS

        private void HandlePlayerCreated()
        {
            AttachEventsOfPlayer();
        }

        private void HandlePreparingAction()
        {
            if (fishingAction == null)
                InitializeFishingAction();
        }

        private void HandleTriggeredAction()
        {

        }

        #endregion

        #endregion
    }
}