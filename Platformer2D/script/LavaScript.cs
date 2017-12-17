using System;
using System.Collections.Generic;
using System.Numerics;
using Engine;
using Engine.Components;
using Engine.Managers;

namespace Platformer2D.script
{
    public class LavaScript : GameScript
    {
        private ActionManager _actionManager;

        private int fireballX = 50;
        private int fireballY = 50;

        // point on top middle of the lava
        private int lavaMiddleX;
        private int lavaY;

        private int _numberFrameFireball;
        private int _numberFrameFireballCurrent;
        
        public override void Awake()
        {
        }

        public override void Start(ActionManager actionManager)
        {
            _actionManager = actionManager;
            PositionComponent pc = (PositionComponent) GetEntity().GetComponentOfType(typeof(PositionComponent));
            RenderComponent rc = (RenderComponent) GetEntity().GetComponentOfType(typeof(RenderComponent));

            lavaMiddleX = (int) (pc.position.X + rc.size.X / 2);
            lavaY = (int) pc.position.Y;

            _numberFrameFireball = 50 + new Random().Next(1, 50);
        }

        public override void Update()
        {
            _numberFrameFireballCurrent++;
            if (_numberFrameFireballCurrent >= _numberFrameFireball)
            {
                _numberFrameFireballCurrent = 0;
                _numberFrameFireball = 50 + new Random().Next(1, 50);
                
                Entity fireball = CreateFireball();
                _actionManager.ActionAddEntity(fireball);
            }
        }

        public override void End()
        {
        }

        public override void OnCollision(CollisionEvent collisionEvent)
        {
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
            pc._forces.Add(new Vector2(0,-1500000));
            entity.AddComponent(pc);
            entity.AddComponent(new RenderComponent
            {
                image = "fireball.png",
                size = new Vector2(fireballX,fireballY)
            });
            entity.AddComponent(new VelocityComponent
            {
                maxVelocity = 700
            });
            entity.AddComponent(new BoxCollisionComponent
            {
                isTrigger = false,
                size = new Vector2(fireballX, fireballY)
            });
            entity.AddComponent(new PositionComponent
            {
                orientation = 0f,
                position = new Vector2(lavaMiddleX - fireballX / 2, lavaY - 10 - fireballY)
            });
            entity.AddComponent(new ScriptComponent
            {
                Script = new FireballScript()
            });

            return entity;
        }
    }
}