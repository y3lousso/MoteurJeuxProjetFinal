namespace MoteurJeuxProjetFinal.GameEngine.Managers
{
    /// <summary>
    /// ActionManager countains methods to interact with the game engine
    /// </summary>
    internal class ActionManager
    {
        private GameEngine _gameEngine;

        public void Init(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        /// <summary>
        /// Return an instance of the current scene
        /// </summary>
        public Scene ActionGetCurrentScene()
        {
            return _gameEngine.GetSceneManager().GetCurrentScene();
        }

        /// <summary>
        /// Return an instance of the current scene
        /// </summary>
        public int ActionGetCurrentSceneIndex()
        {
            return _gameEngine.GetSceneManager().GetCurrentSceneIndex();
        }

        /// <summary>
        /// Change and display the current scene
        /// </summary>
        public void ActionChangeCurrentScene(Scene scene)
        {
            _gameEngine.GetSceneManager().ChangeCurrentScene(scene);
        }

        /// <summary>
        /// Change and display the current scene
        /// </summary>
        public void ActionChangeCurrentScene(int sceneIndex)
        {
            Scene scene = _gameEngine.GetSceneManager().GetScene(sceneIndex);
            if (scene != null)
            {
                ActionChangeCurrentScene(scene);
            }
        }

        /// <summary>
        /// Play a short sound
        /// </summary>
        public void ActionPlaySound(string soundPath)
        {
            _gameEngine.GetSoundManager().PlaySound(soundPath);
        }
        /// <summary>
        /// Add and display an new entity
        /// </summary>
        public void ActionAddEntity(Entity entity)
        {
            // Add the entity in all the systems
            foreach (ISystem system in _gameEngine.GetSystemManager().GetAllSystems())
            {
                system.AddEntity(entity);
            }
            // Add the entity in the current scene
            _gameEngine.GetSceneManager().GetCurrentScene().AddEntity(entity);
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
        /// Edit the attributes of an entity
        /// Add the entity if the old one is not found !
        /// </summary>
        public void ActionEditEntity(Entity oldEntity, Entity newEntity)
        {
            // Remove the entity in the systems
            foreach (ISystem system in _gameEngine.GetSystemManager().GetAllSystems())
            {
                system.EditEntity(oldEntity, newEntity);
            }
            // Remove the entity in the current scene
            _gameEngine.GetSceneManager().GetCurrentScene().ReplaceEntity(oldEntity, newEntity);
        }

        /// <summary>
        /// Edit the attributes of an entity
        /// Add the entity if the old one is not found !
        /// </summary>
        public void ActionEditEntity(string oldEntityName, Entity newEntity)
        {
            Entity entity = _gameEngine.GetSceneManager().GetCurrentScene().GetEntities().Find(e => e.GetName().Equals(oldEntityName));
            if (entity != null)
            {
                ActionEditEntity(entity, newEntity);
            }
        }
    }
}