using System;
using Engine;
using Engine.Managers;

namespace Platformer2D.script
{
    public class StartButtonScript : GameScript
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
        }

        public override void OnClick(EntityClickEvent entityClickEvent)
        {
            _actionManager.ActionChangeCurrentScene(1);
        }
    }
}