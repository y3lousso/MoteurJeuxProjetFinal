using System.Numerics;

namespace MoteurJeuxProjetFinal.GameEngine
{
    public struct RenderNode
    {
        public string Image;
        public Vector2 Position;
        public Vector2 Size;

        public override bool Equals(object obj)
        {
            if(obj == null || obj.GetType() != typeof(RenderNode))
            {
                return false;
            }
            RenderNode rd = (RenderNode) obj;
            return Image.Equals(rd.Image) && Position.Equals(rd.Position) && Size.Equals(rd.Size);
        }
    }
}
