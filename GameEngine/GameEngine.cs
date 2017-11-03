using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MoteurJeuxProjetFinal
{
    class GameEngine
    {
        // GameEngine states
        private bool is_running;

        // GameEngine content
        private List<Entity> _entities = new List<Entity>();

        public void Start()
        {
            // Init game engine
            is_running = true;

            // Load ressources
            //Test
            Entity testEntity = new Entity();
            TestComponent testcomponent = new TestComponent();
            testEntity.AddComponent(testcomponent);
            _entities.Add(testEntity);
            //

            // Game loop
             while (is_running)
            {
            // Check inputs
            // ...

            // Simulate game logic
            foreach (Entity entity in _entities)
            {
                foreach (IComponent component in entity._components)
                {
                    component.Update();                     
                }
            }
            // Apply outputs
            // ...
            }

            // Unload ressources
            // ...

            // Game engine exit
            Debug.WriteLine("Game engine exited correctly.");
        }
    }
}
