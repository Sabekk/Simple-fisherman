using Gameplay.Controller.Module;
using UnityEngine;

namespace Gameplay.Character.Movement
{
    public class CharacterAnimatorStateModule : ControllerModuleBase
    {
        #region VARIABLES

        [SerializeField] private float animationBlendingSpeed = 5;

        private float dirX;
        private float dirY;

        private float currentDirX;
        private float currentDirY;

        #endregion

        #region PROPERTIES

        public Animator CharacterAnimator => Character.CharacterInGame.Aniamtor;
        private int MoveDirectionX => Animator.StringToHash("MoveDirX");
        private int MoveDirectionY => Animator.StringToHash("MoveDirY");
        private bool CanMove => Character.MovementController.MovingModule.CanMove;

        #endregion

        #region METHODS

        public override void OnUpdate()
        {
            base.OnUpdate();

            currentDirX = CanMove == false ? 0 : Mathf.MoveTowards(currentDirX, dirX, animationBlendingSpeed * Time.deltaTime);
            currentDirY = CanMove == false ? 0 : Mathf.MoveTowards(currentDirY, dirY, animationBlendingSpeed * Time.deltaTime);

            SetMovementAnimation(currentDirX, currentDirY);
        }

        protected virtual void MoveInDirection(Vector2 direction)
        {
            dirX = direction.x;
            dirY = direction.y;
        }

        private void SetMovementAnimation(float xDir, float yDir)
        {
            if (CharacterAnimator == null)
                return;

            CharacterAnimator.SetFloat(MoveDirectionX, xDir);
            CharacterAnimator.SetFloat(MoveDirectionY, yDir);
        }


        #region HANDLERS

        protected virtual void HandlePreparingAction()
        {
            //TODO Attach to fishing manager/fishing controller and play animations;
        }

        protected virtual void HandleActionTriggered()
        {
            //TODO Attach to fishing manager/fishing controller and play animations;
        }

        #endregion

        #endregion
    }
}