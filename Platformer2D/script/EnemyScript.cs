using System.Diagnostics;
using System.Numerics;
using MoteurJeuxProjetFinal.GameEngine;
using MoteurJeuxProjetFinal.GameEngine.Components;
using MoteurJeuxProjetFinal.GameEngine.Managers;

namespace MoteurJeuxProjetFinal.Platformer2D.script
{
    class EnemyScript : GameScript
    {
        private ActionManager _actionManager;
        private Vector2 _speed = new Vector2(100,0);
        private VelocityComponent _velocityComponent;
        
        protected internal override void Awake()
        {
        }

        protected internal override void Start(ActionManager actionManager)
        {
            _actionManager = actionManager;
            _velocityComponent = (VelocityComponent) GetEntity().GetComponentOfType(typeof(VelocityComponent));
            _velocityComponent.velocity = _speed;
        }

        protected internal override void Update()
        {
        }

        protected internal override void End()
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