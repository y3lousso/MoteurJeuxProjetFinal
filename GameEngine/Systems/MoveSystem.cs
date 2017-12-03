using System.Collections.Generic;
using MoteurJeuxProjetFinal.GameEngine.Components;
using MoteurJeuxProjetFinal.GameEngine.Nodes;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    class MoveSystem : ISystem
    {
        private GameEngine _gameEngine;
        private List<EntityNode> _moveEntityNodes = new List<EntityNode>();
        
        public void Start(GameEngine gameEngine)          
        {
            _gameEngine = gameEngine;
            foreach (Entity entity in _gameEngine.GetSceneManager().GetCurrentScene().GetEntities())
            {
                if (entity.GetComponentOfType(typeof(PositionComponent)) != null &&
                    entity.GetComponentOfType(typeof(VelocityComponent)) != null)
                {
                    MoveNode newMoveNode = new MoveNode();
                    newMoveNode.positionComponent = (PositionComponent)entity.GetComponentOfType(typeof(PositionComponent));
                    newMoveNode.velocityComponent = (VelocityComponent)entity.GetComponentOfType(typeof(VelocityComponent));
                    EntityNode entityNode = new EntityNode
                    {
                        Node = newMoveNode,
                        Entity = entity
                    };
                    _moveEntityNodes.Add(entityNode);
                }
            }
        }

        public void Update(float deltaTime)
        {
            foreach(EntityNode moveEntityNode in _moveEntityNodes)
            {
                MoveNode moveNode = (MoveNode) moveEntityNode.Node;
                moveNode.positionComponent.position += moveNode.velocityComponent.velocity * deltaTime;

                // Windows collision tweak
                if (moveNode.positionComponent.position.X < 0)
                     moveNode.positionComponent.position.X = 0;
                if (moveNode.positionComponent.position.Y < 0)
                     moveNode.positionComponent.position.Y = 0;
                if (moveNode.positionComponent.position.X > _gameEngine.GetDisplayWindow().GetForm().Width-50)
                     moveNode.positionComponent.position.X = _gameEngine.GetDisplayWindow().GetForm().Width-50;
                if (moveNode.positionComponent.position.Y > _gameEngine.GetDisplayWindow().GetForm().Height-50)
                     moveNode.positionComponent.position.Y = _gameEngine.GetDisplayWindow().GetForm().Height-50;

                moveNode.positionComponent.orientation += moveNode.velocityComponent.angularVelocity * deltaTime;
            }
        }

        public void End()
        {

        }
    }
}
