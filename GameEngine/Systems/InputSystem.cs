using System.Collections.Generic;
using MoteurJeuxProjetFinal.GameEngine.Components;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    internal class InputSystem : ISystem
    {
        private GameEngine _gameEngine;

        public void Start(GameEngine gameEngine)
        {
        }

        public void Update(float deltaTime)
        {
        }

        public void End()
        {
        }

        public void AddEntity(Entity entity)
        {
        }

        public void EditEntity(Entity oldEntity, Entity newEntity)
        {
        }

        public void InitEntities(List<Entity> entities)
        {
        }

        public bool IsCompatible(Entity entity)
        {
            return false;
        }

        public void RemoveEntity(Entity entity)
        {
        }
    }
}