using System.Collections.Generic;
using MoteurJeuxProjetFinal.GameEngine.Components;
using MoteurJeuxProjetFinal.GameEngine.Nodes;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    class InputSystem : ISystem
    {
        
        private GameEngine _gameEngine;
        private List<InputNode> _inputNodes = new List<InputNode>();

        public void Start(GameEngine _gameEngine)
        {
            this._gameEngine = _gameEngine;
            foreach (Entity entity in this._gameEngine.GetSceneManager().GetCurrentScene().GetEntities())
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
                inputNode.inputComponent.inputXY = _gameEngine.GetInputManager().inputs.inputXY;
                // apply them as a force to the physic component
                inputNode.physicsComponent._forces.Add(inputNode.inputComponent.inputXY * inputNode.inputComponent.inputTweaker);
            }
        }

        public void End()
        {

        }
    }
}
