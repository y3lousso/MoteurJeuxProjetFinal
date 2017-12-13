using MoteurJeuxProjetFinal.GameEngine;
using System.Collections.Generic;

namespace MoteurJeuxProjetFinal.GameEngine
{
    /// <summary>
    /// EventManager allows to add Events
    /// </summary>
    internal class EventManager
    {
        internal struct EntityListener
        {
            public IListener Listener;
            public Entity Entity;
        }

        private EventDispatcher _eventDispatcher;
        private List<EntityListener> _entityListeners = new List<EntityListener>();
        private List<IEvent> _events = new List<IEvent>();

        public void Init(GameEngine gameEngine)
        {
            _eventDispatcher = new EventDispatcher(_entityListeners);
        }

        public void AddEvent(IEvent gameEvent)
        {
            _events.Add(gameEvent);
        }

        public List<IEvent> GetAllEvents()
        {
            return _events;
        }

        public void ConsumeAllEvents()
        {
            _events = new List<IEvent>();
        }

        public void RegisterListener(IListener listener, Entity entity)
        {
            EntityListener entityListener = new EntityListener
            {
                Listener = listener,
                Entity = entity
            };
            _entityListeners.Add(entityListener);
        }

        public EventDispatcher GetEventDispatcher()
        {
            return _eventDispatcher;
        }
    }
}

/// <summary>
/// EventDispatcher : add a method too handle a new event / listener
/// </summary>
internal class EventDispatcher : IEventDispatcher
{
    private List<EventManager.EntityListener> _entityListeners;

    internal EventDispatcher(List<EventManager.EntityListener> entityListeners)
    {
        _entityListeners = entityListeners;
    }

    public void Dispatch(GameStartEvent gameEvent)
    {
        foreach (EventManager.EntityListener entityListener in _entityListeners)
        {
            entityListener.Listener.OnGameStart(gameEvent);
        }
    }

    public void Dispatch(GameFinishEvent gameEvent)
    {
        foreach (EventManager.EntityListener entityListener in _entityListeners)
        {
            entityListener.Listener.OnGameFinish(gameEvent);
        }
    }

    public void Dispatch(CollisionEvent gameEvent)
    {
        foreach (EventManager.EntityListener entityListener in _entityListeners)
        {
            // Dispatch the event only for the concerned entity
            if (gameEvent.Entity.Equals(entityListener.Entity))
                entityListener.Listener.OnCollision(gameEvent);
        }
    }

    public void Dispatch(SceneChangeEvent gameEvent)
    {
        foreach (EventManager.EntityListener entityListener in _entityListeners)
        {
            entityListener.Listener.OnSceneChange(gameEvent);
        }
    }

    public void Dispatch(NewSceneDisplayedEvent gameEvent)
    {
        foreach (EventManager.EntityListener entityListener in _entityListeners)
        {
            entityListener.Listener.OnNewSceneDisplayed(gameEvent);
        }
    }
}