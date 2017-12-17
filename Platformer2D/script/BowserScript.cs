using System;
using System.Collections.Generic;
using System.Numerics;
using Engine;
using Engine.Components;
using Engine.Managers;

namespace Platformer2D.script
{
    internal class BowserScript : GameScript
    {
        private int fireballX = 50;
        private int fireballY = 50;
        
        // point on top middle of the lava
        private int bowserX;
        private int bowserY;

        private int _numberFrameFireball;
        private int _numberFrameFireballCurrent;
        
        private ActionManager _actionManager;
        
        public override void Awake()
        {
        }

        public override void Start(ActionManager actionManager)
        {
            _actionManager = actionManager;
            
            PositionComponent pc = (PositionComponent) GetEntity().GetComponentOfType(typeof(PositionComponent));

            bowserX = (int) pc.position.X;
            bowserY = (int) pc.position.Y;
            
            _numberFrameFireball = 50 + new Random().Next(30, 50);

        }

        public override void Update()
        {
            _numberFrameFireballCurrent++;
            if (_numberFrameFireballCurrent >= _numberFrameFireball)
            {
                _numberFrameFireballCurrent = 0;
                _numberFrameFireball = 50 + new Random().Next(1, 30);
                
                Entity fireball = CreateFireball();
                _actionManager.ActionAddEntity(fireball);
                _actionManager.ActionPlaySound("fireball.wav");
            }
        }

        public override void End()
        {
        }

        public override void OnCollision(CollisionEvent collisionEvent)
        {
            if (collisionEvent.OtherEntity.GetName().Contains("Lava"))
            {
                _actionManager.ActionPlaySound("bowserDead.wav");
                
                Player3Script.Instance.SetCanMove(true);
                Entity door = _actionManager.ActionGetCurrentScene().findEntityWithName("door");
                ((RenderComponent)door.GetComponentOfType(typeof(RenderComponent))).image = "door.png";
                _actionManager.ActionRemoveEntity(GetEntity());
            }
        }

        private Entity CreateFireball()
        {
            Entity entity = new Entity();
            entity.SetName("Fireball");
            PhysicsComponent pc = new PhysicsComponent
            {
                _forces = new List<Vector2>(),
                airFrictionTweaker = 0.5f,
                useAirFriction = true,
                useGravity = true,
                masse = 1
            };
            entity.AddComponent(pc);
            entity.AddComponent(new RenderComponent
            {
                image = "fireball.png",
                size = new Vector2(fireballX,fireballY)
            });
            int random = new Random().Next(10,40);
            entity.AddComponent(new VelocityComponent
            {
                maxVelocity = 10000,
                velocity = new Vector2(-20*random, -20*random)
            });
            entity.AddComponent(new BoxCollisionComponent
            {
                isTrigger = false,
                size = new Vector2(fireballX, fireballY)
            });
            entity.AddComponent(new PositionComponent
            {
                orientation = 0f,
                position = new Vector2(bowserX - fireballX - 10, bowserY - 10 - fireballY)
            });
            entity.AddComponent(new ScriptComponent
            {
                Script = new FireballScript()
            });

            return entity;
        }
    }
}