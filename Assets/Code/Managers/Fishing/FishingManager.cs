using Gameplay.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Fishing
{
    public class FishingManager : GameplayManager<FishingManager>
    {
        #region ACTIOn

        public event Action OnFishingActionStarted;
        public event Action OnFishingActionFinished;

        #endregion

        #region VARIABLES

        private FishingAction fishingAction;

        #endregion

        #region PROPERTIES

        public FishingAction FishingAction => fishingAction;
        public Player Player => CharacterManager.Instance.Player;

        #endregion

        #region UNITY_METHODS

        private void Update()
        {
            FishingAction?.OnUpdate();
        }

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
            {
                CharacterManager.Instance.OnPlayerCreated += HandlePlayerCreated;

                if (CharacterManager.Instance.Player != null)
                {
                    DetachEventsOfPlayer();
                    AttachEventsOfPlayer();
                }
            }
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            if (CharacterManager.Instance)
                CharacterManager.Instance.OnPlayerCreated -= HandlePlayerCreated;

            DetachEventsOfPlayer();
            DetachEventsOfFishingAction();
        }

        private void AttachEventsOfPlayer()
        {
            if (Player != null)
            {
                Player.FishingController.FishingModule.OnPreparingAction += HandlePreparingAction;
            }
        }

        private void DetachEventsOfPlayer()
        {
            if (Player != null)
            {
                Player.FishingController.FishingModule.OnPreparingAction -= HandlePreparingAction;
            }
        }

        private void AttachEventsOfFishingAction()
        {
            if (FishingAction == null)
                return;

            FishingAction.OnChangedState += HandleFishingActionChangedState;
        }

        private void DetachEventsOfFishingAction()
        {
            if (FishingAction == null)
                return;

            FishingAction.OnChangedState -= HandleFishingActionChangedState;
        }

        private void InitializeFishingAction()
        {
            fishingAction = new();
            FishingAction.Initialize(Player);
            AttachEventsOfFishingAction();

            OnFishingActionStarted?.Invoke();
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

        private void HandleFishingActionChangedState(FishingStateType previousType, FishingStateType newType)
        {
            if (newType == FishingStateType.NONE)
            {
                FishingAction.Dispose();
                DetachEventsOfFishingAction();
                fishingAction = null;
            }
        }

        #endregion

        #endregion
    }
}