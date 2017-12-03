namespace MoteurJeuxProjetFinal.GameEngine.Managers
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
        public void ActionChangeCurrentScene(Scene scene)
        {
            _gameEngine.GetSceneManager().ChangeCurrentScene(scene);
            _gameEngine.GetSceneManager().DisplayCurrentScene();
        }
        
        /// <summary>
        /// Change and display the current scene
        /// </summary>
        public void ActionChangeCurrentScene(int sceneIndex)
        {
            Scene scene = _gameEngine.GetSceneManager().GetScene(sceneIndex);
            if (scene != null)
            {
                _gameEngine.GetSceneManager().ChangeCurrentScene(scene);
                _gameEngine.GetSceneManager().DisplayCurrentScene(); 
            }
        }

        /// <summary>
        /// Add and display an new entity
        /// </summary>
        public void ActionAddEntity(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Remove an entity
        /// </summary>
        public void ActionRemoveEntity(Entity entity)
        {
            // Remove the entity in the systems
            foreach (ISystem system in _gameEngine.GetSystemManager().GetAllSystems())
            {
                system.RemoveEntity(entity);
            }
            // Remove the entity in the current scene
            _gameEngine.GetSceneManager().GetCurrentScene().RemoveEntity(entity);
        }

        /// <summary>
        /// Remove an entity
        /// </summary>
        public void ActionRemoveEntity(string entityName)
        {
            Entity entity = _gameEngine.GetSceneManager().GetCurrentScene().GetEntities().Find(e => e.GetName().Equals(entityName));
            if (entity != null)
            {
                ActionRemoveEntity(entity);
            }
        }

        /// <summary>
        /// Edit the attribute of an entity
        /// </summary>
        public void ActionEditEntity(Entity oldEntity, Entity newEntity)
        {
            throw new System.NotImplementedException();
        }
        
        /// <summary>
        /// Edit the attribute of an entity
        /// </summary>
        public void ActionEditEntity(string oldEntityName, Entity newEntity)
        {
            throw new System.NotImplementedException();
        }
    }
}