using MoteurJeuxProjetFinal.GameEngine.Managers;

namespace MoteurJeuxProjetFinal.GameEngine
{
    internal abstract class GameScript : IListener
    {
        /// <summary>
        /// Method called by the ScriptManager to Load the script
        /// Give an instance of ActionManager to allow the script to act on the game engine
        /// </summary>
        protected internal abstract void Start(ActionManager actionManager);

        /// <summary>
        /// A method update each frame to update the script
        /// </summary>
        protected internal abstract void Update();

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
    }
}