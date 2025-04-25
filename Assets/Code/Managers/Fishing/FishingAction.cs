using Gameplay.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Fishing
{
    public class FishingAction
    {
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

        public void ChangeState(FishingStateType stateType)
        {
            CurrentState = states[stateType];
            CurrentState.Initialize(this, Character, null);
        }

        private void CreateStates()
        {

        }

        #endregion
    }
}