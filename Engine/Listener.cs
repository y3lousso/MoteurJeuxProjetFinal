namespace Engine
{
    /// <summary>
    /// A class to register Listeners (with visitor pattern)
    /// </summary>
    public interface IListenerRegister
    {
        //void Register(OnGameStartListener listener);
        //void Register(OnGameFinishListener listener);
        //void Register(OnCollisionListener listener);
        //void Register(OnSceneChangeListener listener);
    }

    //////////////////////
    // LIST OF LISTENER //
    //////////////////////

    public interface IListener
    {
        void OnRegister(IListenerRegister register);

        void OnGameStart(GameStartEvent gameStartEvent);

        void OnGameFinish(GameFinishEvent gameFinishEvent);

        void OnCollision(CollisionEvent collisionEvent);

        void OnSceneChange(SceneChangeEvent sceneChangeEvent);

        void OnNewSceneDisplayed(NewSceneDisplayedEvent sceneChangeEvent);

        void OnClick(EntityClickEvent entityClickEvent);
    }
}