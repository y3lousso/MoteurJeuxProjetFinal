using MoteurJeuxProjetFinal.GameEngine;
using MoteurJeuxProjetFinal.GameEngine.Managers;

namespace MoteurJeuxProjetFinal.Platformer2D.script
{
    internal class Door1Script : GameScript
    {
        private static ActionManager _actionManager;


        protected internal override void Awake()
        {

        }

        protected internal override void Start(ActionManager actionManager)
        {
            _actionManager = actionManager;
        }

        protected internal override void Update()
        {

        }

        protected internal override void End()
        {
            
        }

        public override void OnCollision(CollisionEvent collisionEvent)
        {
            // Collision with door -> change scene if enought coins
            if (collisionEvent.OtherEntity.GetName().Equals("Player") && Player1Script.instance.GetCoins() == 5)
            {
                _actionManager.ActionChangeCurrentScene(_actionManager.ActionGetCurrentSceneIndex() + 1);
            }
        }

        public override void OnClick(EntityClickEvent entityClickEvent)
        {

        }
    }
}