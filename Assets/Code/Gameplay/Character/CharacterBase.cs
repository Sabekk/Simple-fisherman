using Database;
using Database.Character.Data;
using Gameplay.Character.Fishing;
using Gameplay.Character.Movement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public class CharacterBase
    {
        #region ACTION

        public event Action OnCharacterInGameCreated;

        #endregion

        #region VARIABLES

        [SerializeField] protected List<CharacterControllerBase> controllers;
        [SerializeField] private bool isInitialzied;
        [SerializeField] private int dataId;

        [SerializeField] protected CharacterMovementController movementController;
        [SerializeField] protected FishingController fishingController;


        private CharacterInGame characterInGame;
        private CharacterData data;

        #endregion

        #region PROPERTIES

        public CharacterData Data
        {
            get
            {
                if (data == null)
                    data = MainDatabases.Instance.CharacterDataDatabase.GetData(dataId);
                return data;
            }
        }

        public CharacterInGame CharacterInGame => characterInGame;
        public CharacterMovementController MovementController => movementController;
        public FishingController FishingController => fishingController;

        #endregion

        #region CONSTRUCTORS

        public CharacterBase()
        {
            CreateControllers();
        }

        #endregion

        #region METHODS

        public virtual void OnUpdate()
        {
            if (!isInitialzied)
                return;

            UpdateControllers();
        }

        public void Initialize()
        {
            SetControllers();
            InitializeControllers();

            isInitialzied = true;
        }

        public void AttachEvents()
        {
            AttachControllers();
        }

        public void DetachEvents()
        {
            DetachControllers();
        }

        public void CleanUp()
        {
            CleanUpControllers();
        }

        public void SetData(CharacterData data)
        {
            dataId = data.Id;
            this.data = data;
        }

        /// <summary>
        /// Creating visualization for character. Call only from manager
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool TryCreateVisualization(Transform parent = null)
        {
            if (Data == null)
                return false;

            if (characterInGame != null)
                return true;

            var rawCharacter = UnityEngine.Object.Instantiate(Data.CharacterInGamePrefab);
            if (rawCharacter)
                characterInGame = rawCharacter.GetComponent<CharacterInGame>();

            if (characterInGame != null)
            {
                characterInGame.Initialize(this);
                characterInGame.transform.SetParent(parent);
                OnCharacterInGameCreated?.Invoke();
                return true;
            }
            else
                return false;
        }

        protected virtual void CreateControllers()
        {
            movementController = new CharacterMovementController();
            fishingController = new FishingController();

        }

        protected virtual void SetControllers()
        {
            controllers = new();

            controllers.Add(movementController);
            controllers.Add(fishingController);
        }

        protected void InitializeControllers()
        {
            controllers.ForEach(m => m.Initialize(this));
        }

        protected void UpdateControllers()
        {
            controllers?.ForEach(m => m.OnUpdate());
        }

        protected void CleanUpControllers()
        {
            controllers.ForEach(m => m.CleanUp());
        }

        protected void AttachControllers()
        {
            controllers.ForEach(m => m.AttachEvents());
        }

        protected void DetachControllers()
        {
            controllers.ForEach(m => m.DetachEvents());
        }

        #endregion
    }
}