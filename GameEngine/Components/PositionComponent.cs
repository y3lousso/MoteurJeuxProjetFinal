using System.Numerics;

namespace MoteurJeuxProjetFinal.GameEngine.Components
{
    internal class PositionComponent : IComponent
    {
        // Data comming from XML
        public Vector2 position = new Vector2(600, 100);

        public float orientation = 0f;

        // Others
    }
}