using Engine;
using Engine.Managers;
using System.Diagnostics;

namespace MoteurJeuxProjetFinal.Platformer2D.script
{
    internal class DoorScript : GameScript
    {
        private static ActionManager _actionManager;

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
            // Collision with door -> change scene if enought coins
            if (collisionEvent.OtherEntity.GetName().Equals("Player") && PlayerScript.instance.GetCoins() == 5)
            {
                _actionManager.ActionChangeCurrentScene(_actionManager.ActionGetCurrentSceneIndex() + 1);
            }
        }

        public override void OnClick(EntityClickEvent entityClickEvent)
        {
        }
    }
}