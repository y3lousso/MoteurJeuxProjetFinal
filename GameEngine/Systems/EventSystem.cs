using System.Collections.Generic;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    class EventSystem : ISystem
    {
        private GameEngine _gameEngine;

        private List<IListener> _listeners;
        private EventDispatcher _dispatcher;
        
        public void Start(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            _listeners= new List<IListener>();
            _dispatcher = new EventDispatcher(_listeners);
        }

        public void Update(float deltaTime)
        {
            // Dispatch all the incoming events 
            List<IEvent> incomingEvents = _gameEngine.GetEventManager().GetAllEvents();
            foreach (IEvent incomingEvent in incomingEvents)
            {
                incomingEvent.onCall(_dispatcher);
            }
            
            // Consume all the events
            _gameEngine.GetEventManager().ConsumeAllEvents();
        }

        public void End()
        {
        }
    }


    /// <summary>
    /// EventDispatcher : add a method too handle a new event / listener
    /// </summary>
    class EventDispatcher : IEventDispatcher
    {
        private List<IListener> _listeners;

        internal EventDispatcher(List<IListener> listeners)
        {
            _listeners = listeners;
        }
        
        public void Dispatch(GameStartEvent gameEvent)
        {
            foreach (OnGameStartListener listener in _listeners.FindAll(listener => listener is OnGameStartListener))
            {
                listener.OnGameStart(gameEvent);
            }
        }

        public void Dispatch(GameFinishEvent gameEvent)
        {
            foreach (OnGameFinishListener listener in _listeners.FindAll(listener => listener is OnGameFinishListener))
            {
                listener.OnGameFinish(gameEvent);
            }
        }

        public void Dispatch(CollisionEvent gameEvent)
        { 
            foreach (OnCollisionListener listener in _listeners.FindAll(listener => listener is OnCollisionListener))
            {
                listener.OnCollision(gameEvent);
            }
        }

        public void Dispatch(SceneChangeEvent gameEvent)
        {
            foreach (OnSceneChangeListener listener in _listeners.FindAll(listener => listener is OnSceneChangeListener))
            {
                listener.OnSceneChange(gameEvent);
            }
        }

        
    }
}
