using System.Collections.Generic;

namespace Engine.Systems
{
    internal class EventSystem : ISystem
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
            foreach (IEvent incomingEvent in incomingEvents.ToArray())
            {
                incomingEvent.OnCall(_dispatcher);
            }

            // Consume all the events
            _gameEngine.GetEventManager().ConsumeAllEvents();
        }

        public void End()
        {
        }

        public bool IsCompatible(Entity entity)
        {
            return false;
        }

        public void AddEntity(Entity entity)
        {
        }

        public void EditEntity(Entity oldEntity, Entity newEntity)
        {
        }

        public void RemoveEntity(Entity entity)
        {
        }

        public void InitEntities(List<Entity> entities)
        {
        }
    }
}