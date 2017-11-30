using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MoteurJeuxProjetFinal
{
    class MoveSystem : ISystem
    {
        GameEngine gameEngine;
        public List<MoveNode> _moveNodes = new List<MoveNode>();
        
        public void Start(GameEngine _gameEngine)          
        {
            gameEngine = _gameEngine;
            foreach (Entity entity in gameEngine.GetSceneManager().GetCurrentScene().GetEntities())
            {
                if (entity.GetComponentOfType(typeof(PositionComponent)) != null &&
                    entity.GetComponentOfType(typeof(VelocityComponent)) != null)
                {
                    MoveNode newMoveNode = new MoveNode();
                    newMoveNode.positionComponent = (PositionComponent)(entity.GetComponentOfType(typeof(PositionComponent)));
                    newMoveNode.velocityComponent = (VelocityComponent)(entity.GetComponentOfType(typeof(VelocityComponent)));
                    _moveNodes.Add(newMoveNode);
                }
            }
        }

        public void Update(float deltaTime)
        {
            foreach(MoveNode moveNode in _moveNodes)
            {
                moveNode.positionComponent.position += moveNode.velocityComponent.velocity * deltaTime;

                // Windows collision tweak
                if (moveNode.positionComponent.position.X < 0)
                     moveNode.positionComponent.position.X = 0;
                if (moveNode.positionComponent.position.Y < 0)
                     moveNode.positionComponent.position.Y = 0;
                if (moveNode.positionComponent.position.X > gameEngine.GetDisplayWindow().GetForm().Width-50)
                     moveNode.positionComponent.position.X = gameEngine.GetDisplayWindow().GetForm().Width-50;
                if (moveNode.positionComponent.position.Y > gameEngine.GetDisplayWindow().GetForm().Height-50)
                     moveNode.positionComponent.position.Y = gameEngine.GetDisplayWindow().GetForm().Height-50;

                moveNode.positionComponent.orientation += moveNode.velocityComponent.angularVelocity * deltaTime;
            }
        }

        public void End()
        {

        }
    }
}
