using MoteurJeuxProjetFinal.GameEngine.Components;
using System.Collections.Generic;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    internal class MoveSystem : ISystem
    {
        private GameEngine _gameEngine;
        private List<Entity> _entities;

        public void Start(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            InitEntities(gameEngine.GetSceneManager().GetCurrentScene().GetEntities());
        }

        public void Update(float deltaTime)
        {
            foreach (Entity entity in _entities)
            {
                PositionComponent positionComponent = (PositionComponent)entity.GetComponentOfType(typeof(PositionComponent));
                VelocityComponent velocityComponent = (VelocityComponent)entity.GetComponentOfType(typeof(VelocityComponent));

                positionComponent.position += velocityComponent.velocity * deltaTime;

                // Windows collision tweak
                if (positionComponent.position.X < 0)
                    positionComponent.position.X = 0;
                if (positionComponent.position.Y < 0)
                    positionComponent.position.Y = 0;
                if (positionComponent.position.X > _gameEngine.GetDisplayWindow().GetForm().Width - 50)
                    positionComponent.position.X = _gameEngine.GetDisplayWindow().GetForm().Width - 50;
                if (positionComponent.position.Y > _gameEngine.GetDisplayWindow().GetForm().Height - 50)
                    positionComponent.position.Y = _gameEngine.GetDisplayWindow().GetForm().Height - 50;

                positionComponent.orientation += velocityComponent.angularVelocity * deltaTime;
            }
        }

        public void End()
        {
        }

        public bool IsCompatible(Entity entity)
        {
            return entity.GetComponentOfType(typeof(PositionComponent)) != null &&
                   entity.GetComponentOfType(typeof(VelocityComponent)) != null;
        }

        public void AddEntity(Entity entity)
        {
            if (IsCompatible(entity))
            {
                _entities.Add(entity);
            }
        }

        public void EditEntity(Entity oldEntity, Entity newEntity)
        {
            if (IsCompatible(newEntity) && IsCompatible(oldEntity))
            {
                int index = _entities.IndexOf(oldEntity);
                if (index != -1)
                {
                    _entities[index] = newEntity;
                }
            }
            else if (IsCompatible(newEntity) && !IsCompatible(oldEntity))
            {
                AddEntity(newEntity);
            }
            else if (!IsCompatible(newEntity) && IsCompatible(oldEntity))
            {
                RemoveEntity(oldEntity);
            }
        }

        public void RemoveEntity(Entity entity)
        {
            _entities.Remove(entity);
        }

        public void InitEntities(List<Entity> entities)
        {
            _entities = new List<Entity>();
            foreach (Entity entity in entities)
            {
                AddEntity(entity);
            }
        }
    }
}