using System.Collections.Generic;
using MoteurJeuxProjetFinal.GameEngine.Components;
using MoteurJeuxProjetFinal.GameEngine.Nodes;

namespace MoteurJeuxProjetFinal.GameEngine.Systems
{
    class MoveSystem : ISystem
    {
        private GameEngine _gameEngine;
        private List<EntityNode> _moveEntityNodes;
        
        public void Start(GameEngine gameEngine)          
        {
            _gameEngine = gameEngine;
            InitEntities(gameEngine.GetSceneManager().GetCurrentScene().GetEntities());
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

        public bool IsCompatible(Entity entity)
        {
            return entity.GetComponentOfType(typeof(PositionComponent)) != null &&
                   entity.GetComponentOfType(typeof(VelocityComponent)) != null;
        }

        public void AddEntity(Entity entity)
        {
            if (IsCompatible(entity))
            {
                MoveNode newMoveNode = new MoveNode
                {
                    positionComponent = (PositionComponent) entity.GetComponentOfType(typeof(PositionComponent)),
                    velocityComponent = (VelocityComponent) entity.GetComponentOfType(typeof(VelocityComponent))
                };
                EntityNode entityNode = new EntityNode
                {
                    Node = newMoveNode,
                    Entity = entity
                };
                _moveEntityNodes.Add(entityNode);
            }
        }

        public void EditEntity(Entity oldEntity, Entity newEntity)
        {
            if (IsCompatible(newEntity) && IsCompatible(oldEntity))
            {
                EntityNode entityNode = _moveEntityNodes.Find(node => node.Entity == oldEntity);
                if (!entityNode.Equals(null))
                {
                    entityNode.Entity = newEntity;
                    entityNode.Node = new MoveNode
                    {
                        positionComponent = (PositionComponent) newEntity.GetComponentOfType(typeof(PositionComponent)),
                        velocityComponent = (VelocityComponent) newEntity.GetComponentOfType(typeof(VelocityComponent))
                    };
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
            EntityNode entityNode = _moveEntityNodes.Find(node => node.Entity == entity);
            _moveEntityNodes.Remove(entityNode);      
        }
        
        public void InitEntities(List<Entity> entities)
        {
            _moveEntityNodes = new List<EntityNode>();
            foreach (Entity entity in entities)
            {
                AddEntity(entity);
            }
        }
    }
}
