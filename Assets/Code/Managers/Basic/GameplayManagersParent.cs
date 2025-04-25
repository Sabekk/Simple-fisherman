using Gameplay.Cameras;
using Gameplay.Character;
using Gameplay.Fishing;
using Gameplay.Inputs;
using Gameplay.UI;

public class GameplayManagersParent : ManagersParent
{
    #region VARIABLES

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    protected override void SetManagers()
    {
        managers.Add(CamerasManager.Instance);
        managers.Add(CharacterManager.Instance);
        managers.Add(InputManager.Instance);
        managers.Add(FishingManager.Instance);
        managers.Add(UIManager.Instance);
    }

    #endregion
}
