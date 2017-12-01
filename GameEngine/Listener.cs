namespace MoteurJeuxProjetFinal.GameEngine
{
    internal interface IListener
    {
    }

    interface OnGameStartListener : IListener
    {
        void OnGameStart(GameStartEvent gameStartEvent);
    }
    
    interface OnGameFinishListener : IListener
    {
        void OnGameFinish(GameFinishEvent gameFinishEvent);
    }

    interface OnCollisionListener : IListener
    {
        void OnCollision(CollisionEvent collisionEvent);
    }

    interface OnSceneChangeListener : IListener
    {
        void OnSceneChange(SceneChangeEvent sceneChangeEvent);
    }
        
  
}