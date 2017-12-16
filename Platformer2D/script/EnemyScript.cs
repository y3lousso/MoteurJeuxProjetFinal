using System.Numerics;
using Engine;
using Engine.Components;
using Engine.Managers;

namespace Platformer2D.script
{
    class EnemyScript : GameScript
    {
        private ActionManager _actionManager;
        private Vector2 _speed = new Vector2(100,0);
        private VelocityComponent _velocityComponent;

        public override void Awake()
        {
        }

        public override void Start(ActionManager actionManager)
        {
            _actionManager = actionManager;
            _velocityComponent = (VelocityComponent) GetEntity().GetComponentOfType(typeof(VelocityComponent));
            _velocityComponent.velocity = _speed;
        }

        public override void Update()
        {
        }

        public override void End()
        {
        }

        public override void OnCollision(CollisionEvent collisionEvent)
        {
            if (collisionEvent.CollisionSide == CollisionSide.FROM_RIGHT_SIDE)
            {
                _velocityComponent.velocity = _speed;
                ((RenderComponent) GetEntity().GetComponentOfType(typeof(RenderComponent))).image = "goomba_right.png";
            }
            else if (collisionEvent.CollisionSide == CollisionSide.FROM_LEFT_SIDE)
            {
                _velocityComponent.velocity = -_speed;

                ((RenderComponent) GetEntity().GetComponentOfType(typeof(RenderComponent))).image = "goomba_left.png";
            }
        }
    }
}