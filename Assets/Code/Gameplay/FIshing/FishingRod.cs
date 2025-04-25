using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Fishing
{
    public class FishingRod : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private Floater floaterPrefab;
        [SerializeField] private Transform startBobberPos;

        #endregion

        #region PROPERTIES

        public Transform StartBobberPos => startBobberPos;
        public Floater FloaterPrefab => floaterPrefab;

        #endregion

        #region METHODS

        public Floater SpawnAndGetFloater()
        {
            Floater floater = Instantiate(floaterPrefab);
            floater.transform.position = startBobberPos.position;

            return floater;
        }

        #endregion
    }
}