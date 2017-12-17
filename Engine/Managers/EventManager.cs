using Engine;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// EventManager allows to add Events
    /// </summary>
    public class EventManager
    {
        internal struct EntityListener
        {
            public IListener Listener;
            public Entity Entity;
        }

        private EventDispatcher _eventDispatcher;
        private List<IEvent> _events = new List<IEvent>();

        public void Init(GameEngine gameEngine)
        {
            _eventDispatcher = new EventDispatcher(new List<EntityListener>());
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
            _eventDispatcher.AddListener(entityListener);
        }

        public void UnregisterAllListeners()
        {
            _eventDispatcher.ClearListeners();
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
public class EventDispatcher : IEventDispatcher
{
    private List<EventManager.EntityListener> _entityListeners;

    internal EventDispatcher(List<EventManager.EntityListener> entityListeners)
    {
        _entityListeners = entityListeners;
    }

    internal void AddListener(EventManager.EntityListener entityListener)
    {
        _entityListeners.Add(entityListener);
    }

    internal void ClearListeners()
    {
        _entityListeners = new List<EventManager.EntityListener>();
    }

    public void Dispatch(GameStartEvent gameEvent)
    {
        foreach (EventManager.EntityListener entityListener in _entityListeners.ToArray())
        {
            entityListener.Listener.OnGameStart(gameEvent);
        }
    }

    public void Dispatch(GameFinishEvent gameEvent)
    {
        foreach (EventManager.EntityListener entityListener in _entityListeners.ToArray())
        {
            entityListener.Listener.OnGameFinish(gameEvent);
        }
    }

    public void Dispatch(CollisionEvent gameEvent)
    {
        foreach (EventManager.EntityListener entityListener in _entityListeners.ToArray())
        {
            // Dispatch the event only for the concerned entity
            if (gameEvent.Entity.Equals(entityListener.Entity))
                entityListener.Listener.OnCollision(gameEvent);
        }
    }

    public void Dispatch(SceneChangeEvent gameEvent)
    {
        foreach (EventManager.EntityListener entityListener in _entityListeners.ToArray())
        {
            entityListener.Listener.OnSceneChange(gameEvent);
        }
    }

    public void Dispatch(NewSceneDisplayedEvent gameEvent)
    {
        foreach (EventManager.EntityListener entityListener in _entityListeners.ToArray())
        {
            entityListener.Listener.OnNewSceneDisplayed(gameEvent);
        }
    }

    public void Dispatch(EntityClickEvent gameEvent)
    {
        foreach (EventManager.EntityListener entityListener in _entityListeners.ToArray())
        {
            // Dispatch the event only for the concerned entity
            if (gameEvent.Entity.Equals(entityListener.Entity))
                entityListener.Listener.OnClick(gameEvent);
        }
    }
}