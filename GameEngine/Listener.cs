namespace MoteurJeuxProjetFinal.GameEngine
{
    
    /// <summary>
    /// A class to register Listeners (with visitor pattern)
    /// </summary>
    interface IListenerRegister
    {
        void Register(OnGameStartListener listener);
        void Register(OnGameFinishListener listener);
        void Register(OnCollisionListener listener);
        void Register(OnSceneChangeListener listener);
    }
    
    //////////////////////
    // LIST OF LISTENER //
    //////////////////////
    
    internal interface IListener
    {
        void OnRegister(IListenerRegister register);
    }

    abstract class OnGameStartListener : IListener
    {
        public abstract void OnGameStart(GameStartEvent gameStartEvent);
        public void OnRegister(IListenerRegister register)
        {
            register.Register(this);
        }
    }
    
    abstract class  OnGameFinishListener : IListener
    {
        public abstract void OnGameFinish(GameFinishEvent gameFinishEvent);
        public void OnRegister(IListenerRegister register)
        {
            register.Register(this);
        }
    }

    abstract class  OnCollisionListener : IListener
    {
        public abstract void OnCollision(CollisionEvent collisionEvent);
        public void OnRegister(IListenerRegister register)
        {
            register.Register(this);
        }
    }

    abstract class  OnSceneChangeListener : IListener
    {
        public abstract void OnSceneChange(SceneChangeEvent sceneChangeEvent);
        public abstract void OnNewSceneDisplayed(NewSceneDisplayedEvent sceneChangeEvent);
        public void OnRegister(IListenerRegister register)
        {
            register.Register(this);
        }
    }
       
  
}