using System.Numerics;
using Engine;
using Engine.Managers;
using Engine.Components;


namespace Platformer2D.script
{
    class Player3Script : GameScript
    {
        public static Player3Script Instance;

        private static ActionManager _actionManager;
        
        private int _life = 3;
        private bool _canBeHurt = true;
        private int _recoveryNumberFrames = 40;
        private int _recoveryCurent;

        private PhysicsComponent _pc;
        private bool _canMove = true;
        private bool _canJump;

        public override void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                throw new System.Exception("Can't have multiple instance of the same script.");
        }

        public override void Start(ActionManager actionManager)
        {
            _actionManager = actionManager;
            _pc = (PhysicsComponent) GetEntity().GetComponentOfType(typeof(PhysicsComponent));
        }

        public override void Update()
        {
            if (_canMove)
            {
                float vertical = InputManager.GetAxis("Vertical");
                float horizontal = InputManager.GetAxis("Horizontal");
                _pc._forces.Add(new Vector2(50000 * horizontal, 0)); // hozitontal move
                if (_canJump && vertical < 0)
                {
                    _pc._forces.Add(new Vector2(0, 50000000 * vertical));
                    _canJump = false;
                    _actionManager.ActionPlaySound("playerJump.wav");
                }
                else if (vertical > 0)
                    _pc._forces.Add(new Vector2(0, 10000000 * vertical));
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
            Instance = null;
        }

        public override void OnCollision(CollisionEvent collisionEvent)
        {
            // Collision with tile -> jump
            if (collisionEvent.OtherEntity.GetName().Contains("Tile"))
            {
                _canJump = true;
            }
            else if (collisionEvent.OtherEntity.GetName().Contains("Lava") || 
                     collisionEvent.OtherEntity.GetName().Contains("Fireball") || 
                     collisionEvent.OtherEntity.GetName().Contains("Bowser"))
            {
                if (_canBeHurt)
                    LostAlife(collisionEvent);
            }
            else if (collisionEvent.OtherEntity.GetName().Equals("Hammer"))
            {
                _actionManager.ActionPlaySound("hammer.wav");

                _actionManager.ActionRemoveEntity(_actionManager.ActionGetCurrentScene()
                    .findEntityWithName("Hammer"));
                _actionManager.ActionRemoveEntity(_actionManager.ActionGetCurrentScene()
                    .findEntityWithName("Tile5"));
                _actionManager.ActionRemoveEntity(collisionEvent.OtherEntity);
                
                ((VelocityComponent) GetEntity().GetComponentOfType(typeof(VelocityComponent))).velocity = new Vector2(0,0);
                _canMove = false;
                
                Entity bowser = _actionManager.ActionGetCurrentScene().findEntityWithName("Bowser");
                ((VelocityComponent) bowser.GetComponentOfType(typeof(VelocityComponent))).velocity = new Vector2(0,-1000);
               
            }
            else if (collisionEvent.OtherEntity.GetName().Equals("door")) 
            {
                _actionManager.ActionChangeCurrentScene(4);
            }
        }

        private void LostAlife(CollisionEvent collisionEvent)
        {
            _life--;
            if (_life > 0)
            {
                _canBeHurt = false;
                

                Entity life = _actionManager.ActionGetCurrentScene().findEntityWithName("life");
                ((RenderComponent) life.GetComponentOfType(typeof(RenderComponent))).image =  "life" + _life + ".png";
                ((RenderComponent) GetEntity().GetComponentOfType(typeof(RenderComponent))).image = "marioHurt.png";
                _actionManager.ActionPlaySound("lostAlife.wav");

                int y = 0;
                if (collisionEvent.CollisionSide == CollisionSide.FROM_TOP_SIDE)
                {
                    y = -1;
                }
                else if (collisionEvent.CollisionSide == CollisionSide.FROM_BOTTOM_SIDE)
                {
                    y = 1;
                }
                int x = 0;
                if (collisionEvent.CollisionSide == CollisionSide.FROM_LEFT_SIDE)
                {
                    x = -1;
                }
                else if (collisionEvent.CollisionSide == CollisionSide.FROM_RIGHT_SIDE)
                {
                    x = 1;
                }
                _pc._forces.Add(new Vector2(1000000*x, 1000000*y));
            }
            else
            {
                Dead();
            }
        }

        private void Dead()
        {
            _actionManager.ActionPlaySound("playerDead.wav");
            _life = 3;
            Entity life = _actionManager.ActionGetCurrentScene().findEntityWithName("life");
            ((RenderComponent) life.GetComponentOfType(typeof(RenderComponent))).image =  "life3.png";
            _actionManager.ActionRemoveEntity(life);
            _actionManager.ActionAddEntity(life);

                    
            ((RenderComponent) GetEntity().GetComponentOfType(typeof(RenderComponent))).image = "mario.png";
            ((PositionComponent) GetEntity().GetComponentOfType(typeof(PositionComponent))).position = new Vector2(50,350);
        }

        public override void OnClick(EntityClickEvent entityClickEvent)
        {
        }

        public void SetCanMove(bool canMove)
        {
            _canMove = true;
        }
    }
}