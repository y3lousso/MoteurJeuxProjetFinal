using MoteurJeuxProjetFinal.GameEngine.Nodes;

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

    abstract class Event
    {
        public Scene CurrentScene;
        protected Event(Scene currentScene)
        {
            CurrentScene = currentScene;
        }
        public abstract void OnCall(IEventDispatcher dispatcher); 
    }

    class GameStartEvent : Event
    {
        public GameStartEvent(Scene currentScene) : base(currentScene)
        {
            
        }

        public sealed override void OnCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }

    class GameFinishEvent : Event
    {
        public GameFinishEvent(Scene currentScene) : base(currentScene)
        {
        }

        public sealed override void OnCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }

    class CollisionEvent : Event
    {
        public Entity Entity1;
        public Entity Entity2;
        public CollisionNode Node1;
        public CollisionNode Node2;
        
        public CollisionEvent(Scene currentScene, Entity entity1, CollisionNode node1, Entity entity2, CollisionNode node2) : base(currentScene)
        {
            Entity1 = entity1;
            Entity2 = entity2;
            Node1 = node1;
            Node2 = node2;
        }

        public sealed override void OnCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }

    class SceneChangeEvent : Event
    {
        public Scene OldScene;
        public Scene NewScene;
        
        public SceneChangeEvent(Scene oldScene, Scene newScene) : base(newScene)
        {
            OldScene = oldScene;
            NewScene = newScene;
        }

        public sealed override void OnCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }

    class NewSceneDisplayedEvent : Event
    {
        public Scene NewSceneDisplayed;

        public NewSceneDisplayedEvent(Scene newSceneDisplayed) : base(newSceneDisplayed)
        {
            NewSceneDisplayed = newSceneDisplayed;
        }


        public sealed override void OnCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }   
    
}