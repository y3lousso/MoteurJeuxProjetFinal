using System.Numerics;
using Engine;
using Engine.Managers;
using Engine.Components;


namespace Platformer2D.script
{
    class Player1Script : GameScript
    {
        public static Player1Script Instance;

        private static ActionManager _actionManager;
        private int _coins;

        private PhysicsComponent _pc;
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
            float vertical = InputManager.GetAxis("Vertical");
            float horizontal = InputManager.GetAxis("Horizontal");
            _pc._forces.Add(new Vector2(50000 * horizontal,0)); // hozitontal move
            if (_canJump && vertical < 0)
            {
                _pc._forces.Add(new Vector2(0, 50000000* vertical));
                _canJump = false;
                _actionManager.ActionPlaySound("playerJump.wav");
            }
            else if (vertical > 0)
                _pc._forces.Add(new Vector2(0, 10000000* vertical));
        }

        public override void End()
        {
            Instance = null;
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
                    Entity door = _actionManager.ActionGetCurrentScene().findEntityWithName("door");
                    ((RenderComponent)door.GetComponentOfType(typeof(RenderComponent))).image = "door.png";
                    _actionManager.ActionPlaySound("doorOpen.wav");
                }
            }
            // Collision with coin -> collect it
            if (collisionEvent.OtherEntity.GetName().Contains("Floor"))
            {
                _canJump = true;
            }
        }

        public override void OnClick(EntityClickEvent entityClickEvent)
        {
        }

        public int GetCoins()
        {
            return _coins;
        }
    }
}