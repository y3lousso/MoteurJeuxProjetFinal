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
        private ListenerInfo _listenerInfo;
        private List<IEvent> _events = new List<IEvent>();

        public void Init(GameEngine gameEngine)
        {
            _listenerInfo = new ListenerInfo();
            _eventDispatcher = new EventDispatcher(_listenerInfo);
            
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
            //TODO
        }

        public EventDispatcher GetEventDispatcher()
        {
            return _eventDispatcher;
        }
    }
}

    /// <summary>
    /// A class to contains all the listeners
    /// </summary>
    internal class ListenerInfo
    {
        internal List<OnGameStartListener> OnGameStartListeners = new List<OnGameStartListener>();
        internal List<OnGameFinishListener> OnGameFinishListeners = new List<OnGameFinishListener>();
        internal List<OnCollisionListener> OnCollisionListeners = new List<OnCollisionListener>();
        internal List<OnSceneChangeListener> OnSceneChangeListeners = new List<OnSceneChangeListener>();
    }

    /// <summary>
    /// EventDispatcher : add a method too handle a new event / listener
    /// </summary>
    internal class EventDispatcher : IEventDispatcher
    {
        private ListenerInfo _listenerInfo;

        internal EventDispatcher(ListenerInfo listenerInfo)
        {
            _listenerInfo = listenerInfo;
        }
        
        public void Dispatch(GameStartEvent gameEvent)
        {
            foreach (OnGameStartListener listener in _listenerInfo.OnGameStartListeners)
            {
                listener.OnGameStart(gameEvent);
            }
        }

        public void Dispatch(GameFinishEvent gameEvent)
        {
            foreach (OnGameFinishListener listener in _listenerInfo.OnGameFinishListeners)
            {
                listener.OnGameFinish(gameEvent);
            }
        }

        public void Dispatch(CollisionEvent gameEvent)
        { 
            foreach (OnCollisionListener listener in _listenerInfo.OnCollisionListeners)
            {
                listener.OnCollision(gameEvent);
            }
        }

        public void Dispatch(SceneChangeEvent gameEvent)
        {
            foreach (OnSceneChangeListener listener in _listenerInfo.OnSceneChangeListeners)
            {
                listener.OnSceneChange(gameEvent);
            }
        }

    }