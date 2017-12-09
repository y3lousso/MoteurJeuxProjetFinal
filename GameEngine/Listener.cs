namespace MoteurJeuxProjetFinal.GameEngine
{
    
    /// <summary>
    /// A class to register Listeners (with visitor pattern)
    /// </summary>
    internal interface IListenerRegister
    {
        //void Register(OnGameStartListener listener);
        //void Register(OnGameFinishListener listener);
        //void Register(OnCollisionListener listener);
        //void Register(OnSceneChangeListener listener);
    }
    
    //////////////////////
    // LIST OF LISTENER //
    //////////////////////

    internal interface IListener
    {
        void OnRegister(IListenerRegister register);
        void OnGameStart(GameStartEvent gameStartEvent);
        void OnGameFinish(GameFinishEvent gameFinishEvent);
        void OnCollision(CollisionEvent collisionEvent);
        void OnSceneChange(SceneChangeEvent sceneChangeEvent);
        void OnNewSceneDisplayed(NewSceneDisplayedEvent sceneChangeEvent);
    }
}