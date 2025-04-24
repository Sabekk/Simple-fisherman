using Gameplay.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterInGame : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private Animator aniamtor;
    [SerializeField] private CharacterController characterController;

    #endregion

    #region PROPERTIES

    public CharacterBase Character { get; set; }
    public Animator Aniamtor => aniamtor;
    public CharacterController CharacterController => characterController;

    #endregion

    #region METHODS

    public void Initialize(CharacterBase character)
    {
        Character = character;
    }

    #endregion
}
