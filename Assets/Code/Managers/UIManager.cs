using Gameplay.Fishing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class UIManager : GameplayManager<UIManager>
    {
        #region VARIABLES

        [SerializeField] private TextMeshProUGUI fishingResult;
        [SerializeField] private float timeOfShowingInformations;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public override void LateInitialzie()
        {
            base.LateInitialzie();
            fishingResult.gameObject.SetActive(false);
        }

        public override void AttachEvents()
        {
            base.AttachEvents();

            FishingState_Finishing.OnFinalizeFishing += HandleFinalizeFishing;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();

            FishingState_Finishing.OnFinalizeFishing -= HandleFinalizeFishing;
        }

        private IEnumerator ShowTimingInformation()
        {
            fishingResult.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeOfShowingInformations);
            fishingResult.gameObject.SetActive(false);
        }

        #region HANDLERS

        private void HandleFinalizeFishing(bool result)
        {
            if (result)
                fishingResult.SetText("Success");
            else
                fishingResult.SetText("Fail");

            StopAllCoroutines();
            StartCoroutine(ShowTimingInformation());
        }

        #endregion

        #endregion
    }
}