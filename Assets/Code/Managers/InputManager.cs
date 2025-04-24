namespace Gameplay.Inputs
{
    public class InputManager : GameplayManager<InputManager>
    {
        #region VARIABLES

        static InputBinds _controll;

        #endregion

        #region PROPERTIES

        public CharacterInputs CharacterInputs { get; private set; }
        public static InputBinds Input
        {
            get
            {
                if (_controll == null)
                    _controll = new InputBinds();
                return _controll;
            }
        }


        #endregion

        #region UNITY_METHODS

        private void OnEnable() => Input.Enable();

        private void OnDisable() => Input.Disable();

        #endregion

        #region METHODS

        public override void Initialzie()
        {
            base.Initialzie();
            CharacterInputs = new(Input);
        }

        public override void LateInitialzie()
        {
            base.LateInitialzie();
            RefreshInputs();
        }


        private void RefreshInputs()
        {
            //Add switch or sth else to change inputs;
            CharacterInputs.Enable();
        }

        #region HANDLERS

        private void HandleCurrentGameStated()
        {
            RefreshInputs();
        }

        #endregion

        #endregion
    }
}
