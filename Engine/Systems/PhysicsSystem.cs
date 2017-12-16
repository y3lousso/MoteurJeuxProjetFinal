using Engine.Components;
using System.Collections.Generic;
using System.Numerics;

namespace Engine.Systems
{
    internal class PhysicsSystem : ISystem
    {
        private GameEngine _gameEngine;
        private List<Entity> _entities;

        public void Start(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            InitEntities(_gameEngine.GetSceneManager().GetCurrentScene().GetEntities());
        }

        public void Update(float deltaTime)
        {
            foreach (Entity entity in _entities)
            {
                PhysicsComponent physicsComponent = (PhysicsComponent)entity.GetComponentOfType(typeof(PhysicsComponent));
                VelocityComponent velocityComponent = (VelocityComponent)entity.GetComponentOfType(typeof(VelocityComponent));
                if (physicsComponent.useGravity)
                {
                    physicsComponent._forces.Add(new Vector2(0, 500 * physicsComponent.masse));
                }

                // CalculateSumForces
                Vector2 sumForces = new Vector2(0, 0);
                foreach (Vector2 force in physicsComponent._forces)
                {
                    sumForces += force;
                }

                // Calculate velocity : v = a*t + v0
                velocityComponent.velocity += (sumForces / physicsComponent.masse) * deltaTime;

                if (physicsComponent.useAirFriction)
                {
                    // each second remove "airFrictionTweaker" % of the max speed
                    velocityComponent.velocity -= physicsComponent.airFrictionTweaker * deltaTime * velocityComponent.velocity;
                }

                // Limit Max velocity
                if (velocityComponent.velocity.X > velocityComponent.maxVelocity)
                    velocityComponent.velocity.X = velocityComponent.maxVelocity;
                else if (velocityComponent.velocity.X < -velocityComponent.maxVelocity)
                    velocityComponent.velocity.X = -velocityComponent.maxVelocity;
                if (velocityComponent.velocity.Y > velocityComponent.maxVelocity)
                    velocityComponent.velocity.Y = velocityComponent.maxVelocity;
                else if (velocityComponent.velocity.Y < -velocityComponent.maxVelocity)
                    velocityComponent.velocity.Y = -velocityComponent.maxVelocity;

                // Clear forces vector for next frame
                physicsComponent._forces.Clear();
            }
        }

        public void End()
        {
        }

        public bool IsCompatible(Entity entity)
        {
            return entity.GetComponentOfType(typeof(PhysicsComponent)) != null &&
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