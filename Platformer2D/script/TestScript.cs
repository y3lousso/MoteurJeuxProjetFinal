using System.Collections.Generic;
using MoteurJeuxProjetFinal.GameEngine;
using MoteurJeuxProjetFinal.GameEngine.Components;
using MoteurJeuxProjetFinal.GameEngine.Managers;

namespace MoteurJeuxProjetFinal.Platformer2D.script
{
    
    class TestScript : GameScript
    {
        private static ActionManager _actionManager;
        private OnCollisionListener _collisionListener = new ChangeSceneListener();
        
        protected internal override void Load(ActionManager actionManager)
        {
            _actionManager = actionManager;
        }

        protected internal override List<IListener> GetListenersToRegister()
        {
            List<IListener> listeners = new List<IListener> {_collisionListener};
            return listeners;
        }
       
        
        private class ChangeSceneListener : OnCollisionListener
        {
            private int _coins;
            
            public override void OnCollision(CollisionEvent collisionEvent)
            {
                if (collisionEvent.Entity1.GetName().Equals("Player"))
                {
                    CollisionWithPlayer(collisionEvent.Entity2);
                }
                else if (collisionEvent.Entity2.GetName().Equals("Player"))
                {
                    CollisionWithPlayer(collisionEvent.Entity1);
                }
            }

            // Processing a collision with the player
            private void CollisionWithPlayer(Entity entity)
            {
                // Collision with coin -> collect him
                if (entity.GetName().Contains("coin"))
                {
                    _coins++;
                    _actionManager.ActionRemoveEntity(entity);
                    if (_coins == 5)
                    {
                        Entity door = _actionManager.ActionGetCurentScene().findEntityWithName("door");
                        ((RenderComponent) door.GetComponentOfType(typeof(RenderComponent))).image = "door.png";
                    }
                }
                // Collision with door -> change scene if enought coins
                else if (_coins == 5 && entity.GetName().Equals("door"))
                {
                    _actionManager.ActionChangeCurrentScene(1);
                }
            }
        }
    }
}