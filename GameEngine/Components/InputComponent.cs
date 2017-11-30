using System.Numerics;

namespace MoteurJeuxProjetFinal.GameEngine.Components
{
    class InputComponent : IComponent
    {
        // Data comming from XML
        public float inputTweaker = 500f;

        // Others
        public Vector2 inputXY = new Vector2(0, 0);
        
    }
}
