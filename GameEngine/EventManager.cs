using System.Collections.Generic;

namespace MoteurJeuxProjetFinal.GameEngine
{
    /// <summary>
    /// EventManager allows to add Events
    /// </summary>
    class EventManager
    {
        
        private GameEngine _gameEngine;
        
        private List<IEvent> _events = new List<IEvent>();

        public void Init(GameEngine _gameEngine)
        {
            this._gameEngine = _gameEngine;
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

        
    }
}