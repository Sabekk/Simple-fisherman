using Gameplay.Cameras;
using UnityEngine;

namespace Gameplay.Character.Movement
{
    public class PlayerMovingModule : CharacterMovingModule
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        protected Player Player => Character as Player;
        public FollowingCamera CharacterCamera => Player.PlayerMovementController.CameraModule.CharacterCamera;

        #endregion

        #region METHODS

        public override void AttachEvents()
        {
            base.AttachEvents();
            //TODO dodaæ reakcjê na inputy
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            //TODO dodaæ reakcjê na inputy
        }

        protected override void RotateCharacterByLookDirection()
        {
            if (CharacterCamera == null)
                return;

            if (CharacterCamera.MainCameraTransform != null && IsMoving)
            {
                Vector3 forward = CharacterCamera.MainCameraTransform.forward;
                forward.y = 0;
                if (forward.sqrMagnitude > 0.01f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(forward);
                    CharacterTransform.rotation = Quaternion.Slerp(CharacterTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                }
            }
        }

        #endregion
    }
}