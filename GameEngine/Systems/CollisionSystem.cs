﻿using System;
using System.Collections.Generic;
using MoteurJeuxProjetFinal.GameEngine.Components;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    
    class CollisionSystem : ISystem
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
            for (int i = 0; i < _entities.Count; i++)
            {
                PositionComponent positionComponent1 = (PositionComponent) _entities[i].GetComponentOfType(typeof(PositionComponent));
                BoxCollisionComponent boxCollisionComponent1 = (BoxCollisionComponent) _entities[i].GetComponentOfType(typeof(BoxCollisionComponent));
                for (int j = i+1; j < _entities.Count; j++)
                {
                    
                    PositionComponent positionComponent2 = (PositionComponent) _entities[j].GetComponentOfType(typeof(PositionComponent));
                    BoxCollisionComponent boxCollisionComponent2 = (BoxCollisionComponent) _entities[j].GetComponentOfType(typeof(BoxCollisionComponent));
                 
                    if (positionComponent2.position.X + boxCollisionComponent2.size.X > positionComponent1.position.X
                        && positionComponent1.position.X + boxCollisionComponent1.size.X > positionComponent2.position.X
                        && positionComponent2.position.Y + boxCollisionComponent2.size.Y > positionComponent1.position.Y
                        && positionComponent1.position.Y + boxCollisionComponent1.size.Y > positionComponent2.position.Y)
                    {
                        // Collision detected
                        if (_entities[i].GetComponentOfType(typeof(VelocityComponent)) != null)
                        {
                            HandleCollision(_entities[i], positionComponent2, boxCollisionComponent2);

                        }
                        if (_entities[j].GetComponentOfType(typeof(VelocityComponent)) != null)
                        {
                            HandleCollision(_entities[j], positionComponent1, boxCollisionComponent1);
                        }
                        /*
                        PhysicsComponent physicsComponent1 = node1.PhysicsComponent;
                        PhysicsComponent physicsComponent2 = node2.PhysicsComponent;
                        //physicsComponent1._forces.Add(new Vector2(0, -20000f));
                        //physicsComponent2._forces.Add(new Vector2(0, +20000f));
                        
                        // Create the collision Event and throw it :
                        CollisionEvent gameEvent = new CollisionEvent(
                            _gameEngine.GetSceneManager().GetCurrentScene(),
                            _collisionEntityNodes[i].Entity,
                            node1,
                            _collisionEntityNodes[j].Entity,
                            node2);
                        _gameEngine.GetEventManager().AddEvent(gameEvent);
                        */
                    }    
                }
            }
        }
        /// <summary>
        /// Handle the collision of an entity compared to an other (identified by its components)
        /// </summary>
        /// <param name="entity1">The entity</param>
        /// <param name="positionComponent2">The position of the other entity</param>
        /// <param name="boxCollisionComponent2">The box of the other entity</param>
        private void HandleCollision(Entity entity1, PositionComponent positionComponent2, BoxCollisionComponent boxCollisionComponent2)
        {
            PositionComponent positionComponent1 = (PositionComponent) entity1.GetComponentOfType(typeof(PositionComponent));
            BoxCollisionComponent boxCollisionComponent1 = (BoxCollisionComponent) entity1.GetComponentOfType(typeof(BoxCollisionComponent));
            VelocityComponent velocityComponent1 = (VelocityComponent) entity1.GetComponentOfType(typeof(VelocityComponent));

            // The 4 distances which indicate how the entity1 is inside entity2
            // dx1, dx2, dy1 & dy2 are respectively the ditance between the left, right, up & down side of entity1 
            // and the right, left, down & up side of entity2
            float dx1 = positionComponent2.position.X + boxCollisionComponent2.size.X - positionComponent1.position.X;
            float dx2 = positionComponent1.position.X + boxCollisionComponent1.size.X - positionComponent2.position.X;
            float dy1 = positionComponent2.position.Y + boxCollisionComponent2.size.Y - positionComponent1.position.Y;
            float dy2 = positionComponent1.position.Y + boxCollisionComponent1.size.Y - positionComponent2.position.Y;

            // Determinate where the entity1 collides entity2
            if (Math.Min(dx1, dx2) < Math.Min(dy1, dy2))
            {
                if (dx1 > dx2)
                    // Collision by the left
                    positionComponent1.position.X -= dx2;
                else
                    // Collision by the right
                    positionComponent1.position.X += dx1;
                // Negate the Y velocity
                velocityComponent1.velocity.X = 0;
            }
            else
            {
                if (dy1 > dy2)
                    // Collision by the up
                    positionComponent1.position.Y -= dy2;
                else
                    // Collision by the down
                    positionComponent1.position.Y += dy1;
                // Negate the X velocity
                velocityComponent1.velocity.Y = 0;
            }
        }
        

        public void End()
        {
        }

        public bool IsCompatible(Entity entity)
        {
            return entity.GetComponentOfType(typeof(PositionComponent)) != null &&
                   entity.GetComponentOfType(typeof(PhysicsComponent)) != null &&
                   entity.GetComponentOfType(typeof(BoxCollisionComponent)) != null;
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
