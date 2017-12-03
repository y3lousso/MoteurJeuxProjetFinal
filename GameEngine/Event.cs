﻿using MoteurJeuxProjetFinal.GameEngine.Nodes;

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
        public Entity Entity1;
        public Entity Entity2;
        public CollisionNode Node1;
        public CollisionNode Node2;
        
        public CollisionEvent(Scene currentScene, Entity entity1, CollisionNode node1, Entity entity2, CollisionNode node2)
        {
            Entity1 = entity1;
            Entity2 = entity2;
            Node1 = node1;
            Node2 = node2;
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