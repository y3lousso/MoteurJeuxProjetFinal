using MoteurJeuxProjetFinal.GameEngine;
using MoteurJeuxProjetFinal.GameEngine.Components;
using MoteurJeuxProjetFinal.GameEngine.Managers;

namespace MoteurJeuxProjetFinal.Platformer2D.script
{
    internal class TestScript : GameScript
    {
        private static ActionManager _actionManager;
        private int _coins;

        protected internal override void Start(ActionManager actionManager)
        {
            _actionManager = actionManager;
        }

        protected internal override void Update()
        {
        }

        public override void OnCollision(CollisionEvent collisionEvent)
        {
            // Collision with coin -> collect him
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