using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoteurJeuxProjetFinal
{
    class CollisionSystem : ISystem
    {
        GameEngine gameEngine;
        public List<CollisionNode> _collisionNodes = new List<CollisionNode>();

        public void Start(GameEngine _gameEngine)
        {
            gameEngine = _gameEngine;
            foreach (Entity entity in gameEngine.GetCurrentScene().GetEntities())
            {
                if (entity.GetComponentOfType(typeof(PositionComponent)) != null && 
                    entity.GetComponentOfType(typeof(VelocityComponent)) != null && 
                    entity.GetComponentOfType(typeof(BoxCollisionComponent)) != null)
                {
                    CollisionNode newCollisionNode = new CollisionNode();
                    newCollisionNode.positionComponent = (PositionComponent)(entity.GetComponentOfType(typeof(PositionComponent)));
                    newCollisionNode.velocityComponent = (VelocityComponent)(entity.GetComponentOfType(typeof(VelocityComponent)));
                    newCollisionNode.boxCollisionComponent = (BoxCollisionComponent)(entity.GetComponentOfType(typeof(BoxCollisionComponent)));
                    _collisionNodes.Add(newCollisionNode);
                }
            }
        }

        public void Update(float deltaTime)
        {
            foreach(CollisionNode collisionNode in _collisionNodes)
            {
                /*if (velocity.X > maxVelocity) { velocity.X = maxVelocity; }
                if (velocity.Y > maxVelocity) { velocity.Y = maxVelocity; }
                if (velocity.X < -maxVelocity) { velocity.X = -maxVelocity; }
                if (velocity.Y < -maxVelocity) { velocity.Y = -maxVelocity; }*/
            }
        }

        public void End()
        {

        }
    }
}
