using MoteurJeuxProjetFinal.GameEngine.Components;

namespace MoteurJeuxProjetFinal.GameEngine.Nodes
{
    class CollisionNode : INode
    {
        public PositionComponent PositionComponent;
        public PhysicsComponent PhysicsComponent;
        public BoxCollisionComponent BoxCollisionComponent;
    }
}
