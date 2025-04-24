using Database.Character.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Character
{
    [CreateAssetMenu(menuName = "Database/CharacterDataDatabase", fileName = "CharacterDataDatabase")]

    public class CharacterDataDatabase : ScriptableObject
    {
        #region VARIABLES

        [SerializeReference] private CharacterData defaultData;
        [SerializeField] private List<CharacterData> charactersData;

        #endregion

        #region PROPERTIES

        public CharacterData DefaultData => defaultData;
        public List<CharacterData> CharactersDatas => charactersData;

        #endregion

        #region METHODS

        public CharacterData GetData(int dataId)
        {
            CharacterData data = CharactersDatas.Find(x => x.IdEquals(dataId));
            if (data == null)
                data = defaultData;

            return data;
        }

        #endregion
    }
}
