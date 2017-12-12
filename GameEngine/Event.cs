namespace MoteurJeuxProjetFinal.GameEngine
{
    
    /// <summary>
    /// IEventDispatcher is interface which can dispatch an incoming event to the listeners (with visitor pattern)
    /// To handle a new event : add a method with the corresponding event in the interface and 
    /// in the implemented class in the EventSystem
    /// </summary>
    interface IEventDispatcher
    {
        void Dispatch(GameStartEvent gameEvent);
        void Dispatch(GameFinishEvent gameEvent);
        void Dispatch(CollisionEvent gameEvent);
        void Dispatch(SceneChangeEvent gameEvent);
        void Dispatch(NewSceneDisplayedEvent gameEvent);
    }
    
    ///////////////////
    // LIST OF EVENT //
    ///////////////////

    interface IEvent
    {
        void OnCall(IEventDispatcher dispatcher); 
    }

    class GameStartEvent : IEvent
    {
        void IEvent.OnCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }

    class GameFinishEvent : IEvent
    {
        void IEvent.OnCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }

    class CollisionEvent : IEvent
    {
        public Entity Entity;
        public Entity OtherEntity;
        public CollisionSide CollisionSide;
        
        public CollisionEvent(Entity entity1, Entity entity2, CollisionSide collisionSide)
        {
            Entity = entity1;
            OtherEntity = entity2;
            CollisionSide = collisionSide; // where Entity collides OtherEntity 
        }

        void IEvent.OnCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }

    class SceneChangeEvent : IEvent
    {
        public Scene OldScene;
        public Scene NewScene;
        
        public SceneChangeEvent(Scene oldScene, Scene newScene)
        {
            OldScene = oldScene;
            NewScene = newScene;
        }

        void IEvent.OnCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }

    class NewSceneDisplayedEvent : IEvent
    {
        public Scene NewSceneDisplayed;

        public NewSceneDisplayedEvent(Scene newSceneDisplayed)
        {
            NewSceneDisplayed = newSceneDisplayed;
        }

        void IEvent.OnCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }   
    
}