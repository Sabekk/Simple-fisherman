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


        private CharacterInGame characterInGame;
        private CharacterData data;

        #endregion

        #region PROPERTIES

        public CharacterData Data
        {
            get
            {
                //TODO wyszukiwanie data z bazy po dataId
                return data;
            }
        }

        public CharacterInGame CharacterInGame => characterInGame;

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
            //dataId = data.Id;
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

            //TODO spawn postaci z pliku data z bazy
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
        }

        protected virtual void SetControllers()
        {
            controllers = new();

            controllers.Add(movementController);
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