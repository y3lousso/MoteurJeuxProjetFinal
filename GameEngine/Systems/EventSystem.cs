using System.Collections.Generic;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    class EventSystem : ISystem
    {
        
        private GameEngine _gameEngine;
        private EventDispatcher _dispatcher;
        
        public void Start(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            _dispatcher = gameEngine.GetEventManager().GetEventDispatcher();
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

}
