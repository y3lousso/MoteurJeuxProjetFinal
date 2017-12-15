using System.Numerics;

namespace Engine.Components
{
    public class RenderComponent : IComponent
    {
        // Data comming from XML
        public string image;

        public Vector2 size = new Vector2(500, 500);

        // Others
    }
}