using UnityEngine;

namespace Gameplay.Cameras
{
    public class CamerasManager : GameplayManager<CamerasManager>
    {
        #region VARIABLES

        [SerializeField] private FollowingCamera personCameraInGamePrefab;

        private FollowingCamera personCameraInGame;

        #endregion

        #region PROPERTIES

        public Camera MainCamera => Camera.main;
        public FollowingCamera PersonCameraInGame
        {
            get
            {
                if (personCameraInGame == null)
                    TryCacheCamera();
                return personCameraInGame;
            }
        }

        #endregion

        #region UNITY_METHODS

        #endregion

        #region METHODS

        private void TryCacheCamera()
        {
            var rawCamera = Instantiate(personCameraInGamePrefab);
            if (rawCamera != null)
                personCameraInGame = rawCamera.GetComponent<FollowingCamera>();
        }

        #endregion
    }
}
