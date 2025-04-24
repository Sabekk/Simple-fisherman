
using Gameplay.Inputs;

namespace Gameplay.Character.Movement
{
    public class PlayerAnimatorStateModule : CharacterAnimatorStateModule
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public override void AttachEvents()
        {
            base.AttachEvents();
            if (InputManager.Instance)
            {
                InputManager.Instance.CharacterInputs.OnMoveInDirection += MoveInDirection;
                InputManager.Instance.CharacterInputs.OnActionPreparing += HandlePreparingAction;
                InputManager.Instance.CharacterInputs.OnActionTriggered += HandleActionTriggered;
            }
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            if (InputManager.Instance)
            {
                InputManager.Instance.CharacterInputs.OnMoveInDirection -= MoveInDirection;
                InputManager.Instance.CharacterInputs.OnActionPreparing -= HandlePreparingAction;
                InputManager.Instance.CharacterInputs.OnActionTriggered -= HandleActionTriggered;
            }
        }

        #endregion
    }
}