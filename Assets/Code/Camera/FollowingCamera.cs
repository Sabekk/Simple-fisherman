using Cinemachine;
using System;
using UnityEngine;

namespace Gameplay.Cameras
{
    [Serializable]
    public class FollowingCamera : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private CinemachineFreeLook cvCamera;

        #endregion

        #region PROPERTIES
        public CinemachineFreeLook CvCamera => cvCamera;
        public Transform MainCameraTransform => CamerasManager.Instance != null ? CamerasManager.Instance.MainCamera.transform : null;

        #endregion

        #region UNITY_METHODS

        #endregion

        #region METHODS

        #endregion
    }
}