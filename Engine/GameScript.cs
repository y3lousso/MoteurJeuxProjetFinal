using Engine.Managers;

namespace Engine
{
    public abstract class GameScript : IListener
    {
        private Entity _entity;

        public void SetEntity(Entity entity)
        {
            _entity = entity;
        }

        public Entity GetEntity()
        {
            return _entity;
        }

        /// <summary>
        /// Method called by the ScriptManager to Init the script
        /// </summary>
        public abstract void Awake();

        /// <summary>
        /// Method called by the ScriptManager to Load the script
        /// Give an instance of ActionManager to allow the script to act on the game engine
        /// </summary>
        public abstract void Start(ActionManager actionManager);

        /// <summary>
        /// A method update each frame to update the script
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Method called by the ScriptManager to End the script
        /// </summary>
        public abstract void End();

        // The listener methods
        public virtual void OnRegister(IListenerRegister register) { }

        public virtual void OnGameStart(GameStartEvent gameStartEvent)
        {
        }

        public virtual void OnGameFinish(GameFinishEvent gameFinishEvent)
        {
        }

        public virtual void OnCollision(CollisionEvent collisionEvent)
        {
        }

        public virtual void OnSceneChange(SceneChangeEvent sceneChangeEvent)
        {
        }

        public virtual void OnNewSceneDisplayed(NewSceneDisplayedEvent sceneChangeEvent)
        {
        }

        public virtual void OnClick(EntityClickEvent entityClickEvent)
        {
        }
    }
}