using Engine;
using Engine.Managers;

namespace Platformer2D.script
{
    public class FireballScript : GameScript
    {
        private ActionManager _actionManager;
        
        public override void Awake()
        {
        }

        public override void Start(ActionManager actionManager)
        {
            _actionManager = actionManager;
        }

        public override void Update()
        {
        }

        public override void End()
        {
        }

        public override void OnCollision(CollisionEvent collisionEvent)
        {
            _actionManager.ActionRemoveEntity(GetEntity());
        }
    }
}