using System.Numerics;

namespace MoteurJeuxProjetFinal.GameEngine.Components
{
    internal class BoxCollisionComponent : IComponent
    {
        // Data comming from XML
        public Vector2 size = new Vector2(0, 0);

        public bool consistance;

        // Others
    }
}