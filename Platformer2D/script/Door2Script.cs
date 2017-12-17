using Engine;
using Engine.Managers;
using Platformer2D.script;

namespace Platformer2D.script
{
    class Door2Script : GameScript
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
                 if (collisionEvent.OtherEntity.GetName().Equals("Player") && Player2Script.Instance.GetEnnemyKilled() == 2)
                 {
                     _actionManager.ActionChangeCurrentScene(3);
                 }
             }

             public override void OnClick(EntityClickEvent entityClickEvent)
             {

             }
         }
}