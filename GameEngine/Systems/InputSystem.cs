using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MoteurJeuxProjetFinal
{
    class InputSystem : ISystem
    {
        GameEngine gameEngine;
        public List<InputNode> _inputNodes = new List<InputNode>();

        public void Start(GameEngine _gameEngine)
        {
            gameEngine = _gameEngine;
            foreach (Entity entity in gameEngine.GetCurrentScene().GetEntities())
            {
                if (entity.GetComponentOfType(typeof(InputComponent)) != null &&
                    entity.GetComponentOfType(typeof(PhysicsComponent)) != null)
                {
                    InputNode newInputNode = new InputNode();
                    newInputNode.inputComponent = (InputComponent)(entity.GetComponentOfType(typeof(InputComponent)));
                    newInputNode.physicsComponent = (PhysicsComponent)(entity.GetComponentOfType(typeof(PhysicsComponent)));
                    _inputNodes.Add(newInputNode);
                }
            }
        }

        public void Update(float deltaTime)
        {
            foreach(InputNode inputNode in _inputNodes)
            {
                // get inputs from input manager then ...
                inputNode.inputComponent.inputXY = gameEngine.GetInputManager().inputs.inputXY;
                // apply them as a force to the physic component
                inputNode.physicsComponent._forces.Add(inputNode.inputComponent.inputXY);
            }
        }

        public void End()
        {

        }
    }
}
