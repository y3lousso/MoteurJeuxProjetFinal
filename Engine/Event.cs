﻿namespace Engine
{
    /// <summary>
    /// IEventDispatcher is interface which can dispatch an incoming event to the listeners (with visitor pattern)
    /// To handle a new event : add a method with the corresponding event in the interface and
    /// in the implemented class in the EventSystem
    /// </summary>
    public interface IEventDispatcher
    {
        void Dispatch(GameStartEvent gameEvent);

        void Dispatch(GameFinishEvent gameEvent);

        void Dispatch(CollisionEvent gameEvent);

        void Dispatch(SceneChangeEvent gameEvent);

        void Dispatch(NewSceneDisplayedEvent gameEvent);

        void Dispatch(EntityClickEvent gameEvent);
    }

    ///////////////////
    // LIST OF EVENT //
    ///////////////////

    public interface IEvent
    {
        void OnCall(IEventDispatcher dispatcher);
    }

    public class GameStartEvent : IEvent
    {
        void IEvent.OnCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }

    public class GameFinishEvent : IEvent
    {
        void IEvent.OnCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }

    public class CollisionEvent : IEvent
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

    public class SceneChangeEvent : IEvent
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

    public class NewSceneDisplayedEvent : IEvent
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

    public class EntityClickEvent : IEvent
    {
        public Entity Entity;

        public EntityClickEvent(Entity entity)
        {
            Entity = entity;
        }

        public void OnCall(IEventDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }
}