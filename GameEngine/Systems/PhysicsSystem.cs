using System.Collections.Generic;
using System.Numerics;
using MoteurJeuxProjetFinal.GameEngine.Components;
using MoteurJeuxProjetFinal.GameEngine.Nodes;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    class PhysicsSystem : ISystem
    {
        private GameEngine _gameEngine;
        private List<EntityNode> _physicsEntityNodes = new List<EntityNode>();
        
        public void Start(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            foreach (Entity entity in _gameEngine.GetSceneManager().GetCurrentScene().GetEntities())
            {
                AddEntity(entity);
            }
        }

        public void Update(float deltaTime)
        {
            foreach (EntityNode physicsEntityNode in _physicsEntityNodes)
            {
                PhysicsNode physicsNode = (PhysicsNode) physicsEntityNode.Node;
                if (physicsNode.physicsComponent.useGravity)
                {
                    physicsNode.physicsComponent._forces.Add(new Vector2(0, 200 * physicsNode.physicsComponent.masse));
                }
                
                // CalculateSumForces
                Vector2 sumForces = new Vector2(0, 0);
                foreach (Vector2 force in physicsNode.physicsComponent._forces)
                {
                    sumForces += force;
                }

                // Calculate velocity : v = a*t + v0 
                physicsNode.velocityComponent.velocity += (sumForces / physicsNode.physicsComponent.masse) * deltaTime;

                if (physicsNode.physicsComponent.useAirFriction)
                {
                    // each second remove "airFrictionTweaker" % of the max speed
                    physicsNode.velocityComponent.velocity -= physicsNode.physicsComponent.airFrictionTweaker* deltaTime* physicsNode.velocityComponent.velocity;
                }

                // Limit Max velocity
                if(physicsNode.velocityComponent.velocity.X > physicsNode.velocityComponent.maxVelocity)
                    physicsNode.velocityComponent.velocity.X  = physicsNode.velocityComponent.maxVelocity;
                else if (physicsNode.velocityComponent.velocity.X < -physicsNode.velocityComponent.maxVelocity)
                    physicsNode.velocityComponent.velocity.X = -physicsNode.velocityComponent.maxVelocity;
                if (physicsNode.velocityComponent.velocity.Y > physicsNode.velocityComponent.maxVelocity)               
                    physicsNode.velocityComponent.velocity.Y = physicsNode.velocityComponent.maxVelocity;
                else if (physicsNode.velocityComponent.velocity.Y < -physicsNode.velocityComponent.maxVelocity)
                    physicsNode.velocityComponent.velocity.Y = -physicsNode.velocityComponent.maxVelocity;

                // Clear forces vector for next frame
                physicsNode.physicsComponent._forces.Clear();
            }
        }

        public void End()
        {

        }

        public void AddEntity(Entity entity)
        {
            if (entity.GetComponentOfType(typeof(PhysicsComponent)) != null &&
                entity.GetComponentOfType(typeof(VelocityComponent)) != null)
            {
                PhysicsNode newPhysicsNode = new PhysicsNode();
                newPhysicsNode.physicsComponent = (PhysicsComponent)entity.GetComponentOfType(typeof(PhysicsComponent));
                newPhysicsNode.velocityComponent = (VelocityComponent)entity.GetComponentOfType(typeof(VelocityComponent));
                EntityNode entityNode = new EntityNode
                {
                    Node = newPhysicsNode,
                    Entity = entity
                };
                _physicsEntityNodes.Add(entityNode);
            }
        }

        public void RemoveEntity(Entity entity)
        {
            EntityNode entityNode = _physicsEntityNodes.Find(node => node.Entity == entity);
            _physicsEntityNodes.Remove(entityNode);        
        }
    }
}
