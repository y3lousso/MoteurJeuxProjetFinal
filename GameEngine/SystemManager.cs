using System.Collections.Generic;

namespace MoteurJeuxProjetFinal.GameEngine
{
    class SystemManager
    {
        GameEngine gameEngine;

        // A prioritized list of the systems 
        public List<ISystem> _systems = new List<ISystem>();

        public void Init(GameEngine _gameEngine)
        {
            gameEngine = _gameEngine;
        }
        public void AddSystem(ISystem system)
        {
            _systems.Add(system);
            system.Start(gameEngine);
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
