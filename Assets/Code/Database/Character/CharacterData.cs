using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Character.Data
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Data/Character/CharacterData", fileName = "CharacterData")]
    public class CharacterData : ScriptableObject, IIdEqualable
    {
        #region VARIABLES

        [SerializeField] private int id = Guid.NewGuid().GetHashCode();
        [SerializeReference] private CharacterInGame characterInGamePrefab;
        [SerializeReference] private float gainPowerSpeed;
        [SerializeReference] private float minThrowPower;
        [SerializeReference] private float maxThrowPower;

        #endregion

        #region PROPERTIES

        public int Id => id;
        public CharacterInGame CharacterInGamePrefab => characterInGamePrefab;
        public float GainPowerSpeed => gainPowerSpeed;
        public float MaxThrowPower => maxThrowPower;
        public float MinThrowPower => minThrowPower;

        #endregion

        #region METHODS

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}
