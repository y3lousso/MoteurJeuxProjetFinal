using System.Collections.Generic;
using MoteurJeuxProjetFinal.GameEngine;

namespace MoteurJeuxProjetFinal.GameEngine
{
    /// <summary>
    /// EventManager allows to add Events
    /// </summary>
    class EventManager
    {

        private EventDispatcher _eventDispatcher;
        private List<IListener> _listeners = new List<IListener>();
        private List<IEvent> _events = new List<IEvent>();

        public void Init(GameEngine gameEngine)
        {
            _eventDispatcher = new EventDispatcher(_listeners);
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

        public void RegisterListener(IListener listener)
        {
            _listeners.Add(listener);
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
    private List<IListener> _listeners;

    internal EventDispatcher(List<IListener> listeners)
    {
        _listeners = listeners;
    }
    
    public void Dispatch(GameStartEvent gameEvent)
    {
        foreach (IListener listener in _listeners)
        {
            listener.OnGameStart(gameEvent);
        }
    }

    public void Dispatch(GameFinishEvent gameEvent)
    {
        foreach (IListener listener in _listeners)
        {
            listener.OnGameFinish(gameEvent);
        }
    }

    public void Dispatch(CollisionEvent gameEvent)
    { 
        foreach (IListener listener in _listeners)
        {
            listener.OnCollision(gameEvent);
        }
    }

    public void Dispatch(SceneChangeEvent gameEvent)
    {
        foreach (IListener listener in _listeners)
        {
            listener.OnSceneChange(gameEvent);
        }
    }

    public void Dispatch(NewSceneDisplayedEvent gameEvent)
    {
        foreach (IListener listener in _listeners)
        {
            listener.OnNewSceneDisplayed(gameEvent);
        }
    }
}