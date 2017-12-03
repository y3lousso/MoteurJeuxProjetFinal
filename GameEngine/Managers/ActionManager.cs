﻿namespace MoteurJeuxProjetFinal.GameEngine.Managers
{
    /// <summary>
    /// ActionManager countains methods to interact with the game engine
    /// </summary>
    class ActionManager
    {

        private GameEngine _gameEngine;

        public void Init(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }
        
        /// <summary>
        /// Change and display the current scene
        /// </summary>
        void ActionChangeCurrentScene()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Add and display an new entity
        /// </summary>
        void ActionAddEntity(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Remove an entity
        /// </summary>
        void ActionRemoveEntity(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Remove an entity
        /// </summary>
        void ActionRemoveEntity(string entityName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Edit the attribute of an entity
        /// </summary>
        void ActionEditEntity(Entity oldEntity, Entity newEntity)
        {
            throw new System.NotImplementedException();
        }
        
        /// <summary>
        /// Edit the attribute of an entity
        /// </summary>
        void ActionEditEntity(string oldEntityName, Entity newEntity)
        {
            throw new System.NotImplementedException();
        }
    }
}