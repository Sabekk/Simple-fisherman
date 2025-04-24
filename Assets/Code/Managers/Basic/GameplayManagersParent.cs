using Gameplay.Cameras;
using Gameplay.Character;

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
    }

    #endregion
}
