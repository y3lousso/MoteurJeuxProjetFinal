using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Numerics;

namespace MoteurJeuxProjetFinal
{
    class CollisionSystem : ISystem
    {
        GameEngine gameEngine;
        public List<CollisionNode> _collisionNodes = new List<CollisionNode>();

        public void Start(GameEngine _gameEngine)
        {
            gameEngine = _gameEngine;
            foreach (Entity entity in gameEngine.GetSceneManager().GetCurrentScene().GetEntities())
            {
                if (entity.GetComponentOfType(typeof(PositionComponent)) != null && 
                    entity.GetComponentOfType(typeof(PhysicsComponent)) != null && 
                    entity.GetComponentOfType(typeof(BoxCollisionComponent)) != null)
                {
                    CollisionNode newCollisionNode = new CollisionNode();
                    newCollisionNode.positionComponent = (PositionComponent)(entity.GetComponentOfType(typeof(PositionComponent)));
                    newCollisionNode.physicsComponent = (PhysicsComponent)(entity.GetComponentOfType(typeof(PhysicsComponent)));
                    newCollisionNode.boxCollisionComponent = (BoxCollisionComponent)(entity.GetComponentOfType(typeof(BoxCollisionComponent)));
                    _collisionNodes.Add(newCollisionNode);
                }
            }
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < _collisionNodes.Count; i++)
            {
                PositionComponent positionComponent1 = _collisionNodes[i].positionComponent;
                BoxCollisionComponent boxCollisionComponent1 = _collisionNodes[i].boxCollisionComponent;
                for (int j = i+1; j < _collisionNodes.Count; j++)
                {
                    PositionComponent positionComponent2 = _collisionNodes[j].positionComponent;
                    BoxCollisionComponent boxCollisionComponent2 = _collisionNodes[j].boxCollisionComponent;
                    if (positionComponent1.position.X < positionComponent2.position.X + boxCollisionComponent2.size.X &&
                       positionComponent1.position.X + boxCollisionComponent1.size.X > positionComponent2.position.X &&
                       positionComponent1.position.Y < positionComponent2.position.Y + boxCollisionComponent2.size.Y &&
                       positionComponent1.position.Y + boxCollisionComponent1.size.Y > positionComponent2.position.Y )
                    {
                        PhysicsComponent physicsComponent1 = _collisionNodes[i].physicsComponent;
                        PhysicsComponent physicsComponent2 = _collisionNodes[j].physicsComponent;
                        physicsComponent1._forces.Add(new Vector2(0, -20000f));
                    }                      
                }
            }
        }

        public void End()
        {

        }
    }
}
