using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Fishing
{
    public class FishingController : CharacterControllerBase
    {
        #region VARIABLES

        [SerializeField] protected FishingModule fishingModule;

        #endregion

        #region PROPERTIES

        public FishingModule FishingModule => fishingModule;

        #endregion

        #region METHODS

        public override void CreateModules()
        {
            base.CreateModules();
            fishingModule = new FishingModule();
        }

        public override void SetModules()
        {
            base.SetModules();
            modules.Add(fishingModule);
        }

        #endregion
    }
}
