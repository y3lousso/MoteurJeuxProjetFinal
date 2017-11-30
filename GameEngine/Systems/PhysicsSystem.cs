using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MoteurJeuxProjetFinal
{
    class PhysicsSystem : ISystem
    {
        GameEngine gameEngine;
        public List<PhysicsNode> _physicsNodes = new List<PhysicsNode>();
        
        public void Start(GameEngine _gameEngine)
        {
            gameEngine = _gameEngine;
            foreach (Entity entity in gameEngine.GetSceneManager().GetCurrentScene().GetEntities())
            {
                if (entity.GetComponentOfType(typeof(PhysicsComponent)) != null &&
                    entity.GetComponentOfType(typeof(VelocityComponent)) != null)
                {
                    PhysicsNode newPhysicsNode = new PhysicsNode();
                    newPhysicsNode.physicsComponent = (PhysicsComponent)(entity.GetComponentOfType(typeof(PhysicsComponent)));
                    newPhysicsNode.velocityComponent = (VelocityComponent)(entity.GetComponentOfType(typeof(VelocityComponent)));
                    _physicsNodes.Add(newPhysicsNode);
                }
            }
        }

        public void Update(float deltaTime)
        {
            foreach (PhysicsNode physicsNode in _physicsNodes)
            {
                if (physicsNode.physicsComponent.useGravity == true)
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

                if (physicsNode.physicsComponent.useAirFriction == true)
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
    }
}
