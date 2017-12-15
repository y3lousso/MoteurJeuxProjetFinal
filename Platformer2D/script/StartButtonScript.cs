using System;
using MoteurJeuxProjetFinal.GameEngine;
using MoteurJeuxProjetFinal.GameEngine.Components;
using MoteurJeuxProjetFinal.GameEngine.Managers;

namespace MoteurJeuxProjetFinal.Platformer2D.script
{
    internal class StartButtonScript : GameScript
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

        }

        public override void OnClick(EntityClickEvent entityClickEvent)
        {
            _actionManager.ActionChangeCurrentScene(1);
        }
    }
}

