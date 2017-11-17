using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoteurJeuxProjetFinal
{
    class RenderSystem : ISystem
    {        
        GameEngine gameEngine;
        public List<RenderNode> _renderNodes = new List<RenderNode>();

        public void Start(GameEngine _gameEngine)
        {
            gameEngine = _gameEngine;
            foreach (Entity entity in gameEngine.GetCurrentScene().GetEntities())
            {
                if (entity.GetComponentOfType(typeof(PositionComponent)) != null &&
                    entity.GetComponentOfType(typeof(RenderComponent)) != null)
                {
                    RenderNode newRenderNode = new RenderNode();
                    newRenderNode.positionComponent = (PositionComponent)(entity.GetComponentOfType(typeof(PositionComponent)));
                    newRenderNode.renderComponent = (RenderComponent)(entity.GetComponentOfType(typeof(RenderComponent)));
                    _renderNodes.Add(newRenderNode);
                }
            }
        }

        public void Update(float deltaTime)
        {
            foreach(RenderNode renderNode in _renderNodes)
            {
                gameEngine.GetScreen().ClearDynamicPanel();
                gameEngine.GetScreen().AddImageToPanel(gameEngine.GetScreen().dynamicPanel, renderNode.positionComponent.position, renderNode.renderComponent.size, renderNode.renderComponent.image);
            }           
        }

        public void End()
        {

        }
    }
}
