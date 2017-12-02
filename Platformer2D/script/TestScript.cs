using System.Collections.Generic;
using System.Diagnostics;
using MoteurJeuxProjetFinal.GameEngine;

namespace MoteurJeuxProjetFinal.Platformer2D.script
{
    
    class TestScript : GameScript
    {
        private OnCollisionListener _collisionListener = new ChangeSceneListener();
        
        protected internal override void Load()
        {
        }

        internal override List<IListener> GetListenersToRegister()
        {
            List<IListener> listeners = new List<IListener> {_collisionListener};
            return listeners;
        }
       
        
        private class ChangeSceneListener : OnCollisionListener
        {
            public override void OnCollision(CollisionEvent collisionEvent)
            {
                Debug.WriteLine(collisionEvent.Entity1.GetName() + " // " + collisionEvent.Entity2.GetName());
                
                // Remove the door in the current scene
                if (string.Equals(collisionEvent.Entity1.GetName(), "Door1") && string.Equals(collisionEvent.Entity2.GetName(), "Player"))
                {
                    Debug.WriteLine(collisionEvent.Entity1.GetName() + " Deleted !");
                    collisionEvent.CurrentScene.RemoveEntity(collisionEvent.Entity1);   
                }
                else if (string.Equals(collisionEvent.Entity2.GetName(), "Door1") && string.Equals(collisionEvent.Entity1.GetName(), "Player"))
                {
                    Debug.WriteLine(collisionEvent.Entity2.GetName() + " Deleted !");
                    collisionEvent.CurrentScene.RemoveEntity(collisionEvent.Entity2);   
                }
            }
        }
    }
}