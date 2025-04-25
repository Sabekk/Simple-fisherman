using Gameplay.Controller.Module;
using UnityEngine;

namespace Gameplay.Character.Movement
{
    public class CharacterMovingModule : ControllerModuleBase
    {
        #region VARIABLES

        [SerializeField] protected float walkSpeed = 4;
        [SerializeField] protected float gravity = 5;
        [SerializeField] protected float rotationSpeed = 10f;

        protected Vector2 direction = Vector3.zero;
        protected Vector2 lookDirection = Vector3.zero;
        protected Vector3 moveDirection = Vector3.zero;

        #endregion

        #region PROPERTIES

        public bool IsMoving => direction != Vector2.zero;
        public bool IsGrounded => CharacterTransform == null ? false : Physics.Raycast(CharacterTransform.position, Vector3.down, 0.1f);
        public bool CanMove => Character.FishingController.FishingModule.IsFishing == false;
        protected Transform CharacterTransform => Character.CharacterInGame ? Character.CharacterInGame.transform : null;
        protected CharacterController CharacterController => Character.CharacterInGame != null ? Character.CharacterInGame.CharacterController : null;

        #endregion

        #region METHODS

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (CharacterTransform == null)
                return;

            if (CanMove)
            {
                MoveCharacter();
                RotateCharacterByLookDirection();
            }
        }

        protected virtual void MoveCharacter()
        {
            if (IsMoving)
            {
                moveDirection = CharacterTransform.right * direction.x + CharacterTransform.forward * direction.y;
                CharacterController?.Move(moveDirection * Time.deltaTime * walkSpeed);
            }

            if (!IsGrounded)
            {
                Vector3 velocity = Vector3.down * gravity * Time.deltaTime;
                CharacterController?.Move(velocity);
            }
        }

        protected virtual void RotateCharacterByLookDirection()
        {
            if (lookDirection != Vector2.zero)
            {
                Quaternion deltaRotation = Quaternion.Euler(0, lookDirection.x * rotationSpeed * Time.fixedDeltaTime, 0);
                CharacterTransform.rotation = Quaternion.RotateTowards(CharacterTransform.rotation, deltaRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }

        protected virtual void HandleMoveInDirection(Vector2 direction)
        {
            this.direction = direction;
        }

        protected virtual void HandleLookInDirection(Vector2 direction)
        {
            this.lookDirection = direction;
        }

        #endregion
    }
}