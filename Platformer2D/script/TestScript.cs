using MoteurJeuxProjetFinal.GameEngine;
using MoteurJeuxProjetFinal.GameEngine.Components;
using MoteurJeuxProjetFinal.GameEngine.Managers;

namespace MoteurJeuxProjetFinal.Platformer2D.script
{
    internal class TestScript : GameScript
    {
        private static ActionManager _actionManager;
        private int _coins;

        private Entity player;
        private PhysicsComponent pc;

        protected internal override void Start(ActionManager actionManager)
        {
            _actionManager = actionManager;
            player = GetEntity();
            pc = (PhysicsComponent)player.GetComponentOfType(typeof(PhysicsComponent));
        }

        protected internal override void Update()
        {
            float vertical = InputManager.GetAxis("Vertical");
            float horizontal = InputManager.GetAxis("Horizontal");
            pc._forces.Add(new System.Numerics.Vector2(50000 * horizontal, 50000 * vertical));
        }

        public override void OnCollision(CollisionEvent collisionEvent)
        {
            /**
            switch (collisionEvent.CollisionSide)
            {
                case CollisionSide.FROM_LEFT_SIDE:
                    Debug.WriteLine("Player collides " + collisionEvent.OtherEntity.GetName() + " from left side !");
                    break;

                case CollisionSide.FROM_RIGHT_SIDE:
                    Debug.WriteLine("Player collides " + collisionEvent.OtherEntity.GetName() + " from right side !");
                    break;

                case CollisionSide.FROM_TOP_SIDE:
                    Debug.WriteLine("Player collides " + collisionEvent.OtherEntity.GetName() + " from top side !");
                    break;

                case CollisionSide.FROM_BOTTOM_SIDE:
                    Debug.WriteLine("Player collides " + collisionEvent.OtherEntity.GetName() + " from bottom side !");
                    break;

                case CollisionSide.UNKNOWN:
                    Debug.WriteLine("Player collides " + collisionEvent.OtherEntity.GetName() + " from unknown side !");
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            */

            // Collision with coin -> collect it
            if (collisionEvent.OtherEntity.GetName().Contains("coin"))
            {
                _coins++;
                _actionManager.ActionRemoveEntity(collisionEvent.OtherEntity);
                if (_coins == 5)
                {
                    Entity door = _actionManager.ActionGetCurentScene().findEntityWithName("door");
                    ((RenderComponent)door.GetComponentOfType(typeof(RenderComponent))).image = "door.png";
                }
            }
            // Collision with door -> change scene if enought coins
            else if (_coins == 5 && collisionEvent.OtherEntity.GetName().Equals("door"))
            {
                _actionManager.ActionChangeCurrentScene(1);
            }
        }
    }
}