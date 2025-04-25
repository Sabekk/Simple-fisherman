using Gameplay.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Fishing
{
    public class FishingAction : IDisposable
    {
        #region ACTION

        /// <summary>
        /// Finish event with information about prevaious and next stete
        /// </summary>
        public event Action<FishingStateType, FishingStateType> OnChangedState;

        #endregion

        #region VARIABLES

        private Dictionary<FishingStateType, FishingStateBase> states;

        #endregion

        #region PROPERTIES

        public FishingStateBase CurrentState { get; set; }
        protected CharacterBase Character { get; set; }
        protected Floater Floater { get; set; }

        #endregion

        #region METHODS

        public void Initialize(CharacterBase character)
        {
            states = new();
            Character = character;

            CreateStates();
            ChangeState(FishingStateType.PREPARING);
        }

        public void Dispose()
        {
            CleanUpCurrentState();
        }

        public void ChangeState(FishingStateType stateType, params object[] parameters)
        {
            FishingStateType oldState = CurrentState != null ? CurrentState.Type : FishingStateType.NONE;
            CleanUpCurrentState();

            if (states.TryGetValue(stateType, out var nextState))
            {
                CurrentState = nextState;
                CurrentState.Initialize(this, Character, parameters);
                Debug.Log($"[FishingAction] current state:{CurrentState.Type}");

                CurrentState.Activate();
            }
            else
            {
                stateType = FishingStateType.NONE;
            }

            OnChangedState?.Invoke(oldState, stateType);
        }

        public void OnUpdate()
        {
            if (CurrentState == null)
                return;

            CurrentState.OnUpdate();
        }

        private void CreateStates()
        {
            states.Add(FishingStateType.PREPARING, new FishingState_Preparing());
            states.Add(FishingStateType.THROWING, new FishingState_Throwing());
            states.Add(FishingStateType.WAITING, new FishingState_Waiting());
            states.Add(FishingStateType.FIRST_BITE, new FishingState_FirstBite());
            states.Add(FishingStateType.HOOKING, new FishingState_Hooking());
            states.Add(FishingStateType.FINISHING, new FishingState_Finishing());
        }

        private void CleanUpCurrentState()
        {
            if (CurrentState == null)
                return;

            CurrentState.Dispose();
            CurrentState = null;
        }

        #endregion
    }
}