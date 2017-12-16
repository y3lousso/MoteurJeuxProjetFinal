using System;
using System.Diagnostics;
using System.Numerics;
using Engine;
using Engine.Managers;
using Engine.Components;


namespace Platformer2D.script
{
    public class Player2Script : GameScript
    {
        public static Player2Script instance;

        private static ActionManager _actionManager;
        
        private int _life = 3;
        private bool _canBeHurt = true;
        private int _recoveryNumberFrames = 40;
        private int _recoveryCurent;
        
        private int _enemyKilled;

        private PhysicsComponent _pc;
        private bool _canJump;

        public override void Awake()
        {
            if (instance == null)
                instance = this;
            else
                throw new Exception("Can't have multiple instance of the same script.");
        }

        public override void Start(ActionManager actionManager)
        {
            _actionManager = actionManager;
            _pc = (PhysicsComponent) GetEntity().GetComponentOfType(typeof(PhysicsComponent));
        }

        public override void Update()
        {
            float vertical = InputManager.GetAxis("Vertical");
            float horizontal = InputManager.GetAxis("Horizontal");
            _pc._forces.Add(new Vector2(50000 * horizontal,0));
            if (_canJump && Math.Abs(vertical) > 0.01)
            {
                _pc._forces.Add(new Vector2(0, 50000000* vertical));
                _canJump = false;
                _actionManager.ActionPlaySound("playerJump.wav");
            }

            if (!_canBeHurt)
            {
                _recoveryCurent++;
                if (_recoveryCurent >= _recoveryNumberFrames)
                {
                    ((RenderComponent)GetEntity().GetComponentOfType(typeof(RenderComponent))).image = "mario.png";
                    _recoveryCurent = 0;
                    _canBeHurt = true;
                }
            }
        }

        public override void End()
        {
            instance = null;
        }

        public override void OnCollision(CollisionEvent collisionEvent)
        {
            
            // Collision with floor -> reset jump
            if (collisionEvent.OtherEntity.GetName().Contains("Floor"))
            {
                _canJump = true;
            }
            
            
            // Collision with enemy
            else if (collisionEvent.OtherEntity.GetName().Contains("Enemy") && _canBeHurt && 
                (collisionEvent.CollisionSide == CollisionSide.FROM_RIGHT_SIDE || 
                collisionEvent.CollisionSide == CollisionSide.FROM_LEFT_SIDE))
            {
                // Lost a life
                _life--;
                if (_life > 0 && collisionEvent.CollisionSide != CollisionSide.FROM_TOP_SIDE)
                {
                    _canBeHurt = false;

                    Entity life = _actionManager.ActionGetCurrentScene().findEntityWithName("life");
                    Debug.WriteLine("Life : " + _life);
                    ((RenderComponent) life.GetComponentOfType(typeof(RenderComponent))).image =  "life" + _life + ".png";
                    ((RenderComponent) GetEntity().GetComponentOfType(typeof(RenderComponent))).image = "marioHurt.png";
                    _actionManager.ActionPlaySound("lostAlife.wav");
                    if (collisionEvent.CollisionSide == CollisionSide.FROM_RIGHT_SIDE)
                    {
                        _pc._forces.Add(new Vector2(1000000, 0));
                    }
                    else if (collisionEvent.CollisionSide == CollisionSide.FROM_LEFT_SIDE)
                    {
                        _pc._forces.Add(new Vector2(-1000000, 0));
                    }
                }
                // dead 
                else
                {
                    _actionManager.ActionPlaySound("playerDead.wav");
                    _life = 3;
                    Entity life = _actionManager.ActionGetCurrentScene().findEntityWithName("life");
                    Debug.WriteLine("Life : " + _life);
                    ((RenderComponent) life.GetComponentOfType(typeof(RenderComponent))).image =  "life3.png";
                    _actionManager.ActionRemoveEntity(life);
                    _actionManager.ActionAddEntity(life);

                    
                    ((RenderComponent) GetEntity().GetComponentOfType(typeof(RenderComponent))).image = "mario.png";
                    ((PositionComponent) GetEntity().GetComponentOfType(typeof(PositionComponent))).position = new Vector2(0,550);
                }
            }
            // Kill an ennemy
            if (collisionEvent.OtherEntity.GetName().Contains("Enemy") && collisionEvent.CollisionSide == CollisionSide.FROM_TOP_SIDE)
            {
                _pc._forces.Add(new Vector2(0, -1000000));
                _actionManager.ActionRemoveEntity(collisionEvent.OtherEntity);
                _enemyKilled++;
                _actionManager.ActionPlaySound("enemyDead.wav");
                if (_enemyKilled == 2)
                {
                    Entity door = _actionManager.ActionGetCurrentScene().findEntityWithName("door");
                    ((RenderComponent)door.GetComponentOfType(typeof(RenderComponent))).image = "door.png";
                    _actionManager.ActionPlaySound("doorOpen.wav");
                }
            }
        }

        public override void OnClick(EntityClickEvent entityClickEvent)
        {

        }

        public int GetEnnemyKilled() { return _enemyKilled; }
    }
}