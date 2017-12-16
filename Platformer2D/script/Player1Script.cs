using Engine;
using Engine.Managers;
using Engine.Components;


namespace Platformer2D.script
{
    class Player1Script : GameScript
    {
        public static Player1Script instance;

        private static ActionManager _actionManager;
        private int _coins;

        private Entity player;
        private PhysicsComponent pc;
        private bool CanJump = false;

        public override void Awake()
        {
            if (instance == null)
                instance = this;
            else
                throw new System.Exception("Can't have multiple instance of the same script.");
        }

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
            pc._forces.Add(new System.Numerics.Vector2(50000 * horizontal, 0));
            if (CanJump)
            {
                pc._forces.Add(new System.Numerics.Vector2(0, 50000000 * vertical));
                CanJump = false;
                _actionManager.ActionPlaySound("playerJump.wav");
            }
        }

        public override void End()
        {
            instance = null;
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
                CanJump = true;
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