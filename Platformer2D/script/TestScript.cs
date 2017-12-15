﻿using Engine;
using Engine.Components;
using Engine.Managers;
using System.Diagnostics;

namespace MoteurJeuxProjetFinal.Platformer2D.script
{
    public class TestScript : GameScript
    {
        private static ActionManager _actionManager;
        private int _coins;

        private Entity player;
        private PhysicsComponent pc;

        public override void Start(ActionManager actionManager)
        {
            _actionManager = actionManager;
            player = GetEntity();
            pc = (PhysicsComponent)player.GetComponentOfType(typeof(PhysicsComponent));
        }

        public override void Update()
        {
            float vertical = InputManager.GetAxis("Vertical");
            float horizontal = InputManager.GetAxis("Horizontal");
            pc._forces.Add(new System.Numerics.Vector2(50000 * horizontal, 50000 * vertical));
        }

        public override void OnCollision(CollisionEvent collisionEvent)
        {
            // Collision with coin -> collect it
            if (collisionEvent.OtherEntity.GetName().Contains("coin"))
            {
                _coins++;
                _actionManager.ActionRemoveEntity(collisionEvent.OtherEntity);
                _actionManager.ActionPlaySound("coinGetSound.wav");
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

        public override void OnClick(EntityClickEvent entityClickEvent)
        {
            Debug.WriteLine("Player clicked !");
        }
    }
}