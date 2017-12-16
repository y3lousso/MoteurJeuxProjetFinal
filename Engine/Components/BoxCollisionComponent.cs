
namespace Engine.Components
{
    public class BoxCollisionComponent : IComponent
    {
        // Data comming from XML
        public Vector2 size = new Vector2(0, 0);

        public bool isTrigger = false;

        // Others
    }
}