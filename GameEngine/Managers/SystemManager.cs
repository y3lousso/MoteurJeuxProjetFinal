using System.Collections.Generic;

namespace MoteurJeuxProjetFinal.GameEngine.Managers
{
    class SystemManager
    {
        GameEngine _gameEngine;

        // A prioritized list of the systems 
        private List<ISystem> _systems = new List<ISystem>();

        public void Init(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        public List<ISystem> GetAllSystems()
        {
            return _systems;
        }
        
        public void AddSystem(ISystem system)
        {
            _systems.Add(system);
            system.Start(_gameEngine);
        } 

        public void Update(float deltaTime)
        {
            foreach(ISystem system in _systems)
            {
                system.Update(deltaTime);
            }
        }

        public void RemoveSystem(ISystem system)
        {
            system.End();
            _systems.Remove(system);
        }

        public void End()
        {
            foreach (ISystem system in _systems)
            {
                system.End();
            }
        }
    }
}
