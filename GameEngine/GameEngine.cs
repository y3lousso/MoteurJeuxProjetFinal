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

        // Xml file management
        private XML_Manager XmlManager = new XML_Manager();

        // GameEngine content
        private List<Entity> _entities = new List<Entity>();

        /// <summary>
        /// Init game engine
        /// </summary>
        public void Init()
        {            
            is_running = true;
            Entity testEntity = new Entity();
        }

        /// <summary>
        /// Game engine loop
        /// </summary>
        public void RunGameLoop()
        {
            // Game loop
            while (is_running)
            {
                // Check inputs
                // ...

                // Simulate game logic
                foreach (Entity entity in _entities)
                {
                    foreach (IComponent component in entity.GetComponents())
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

        public XML_Manager GetXmlManager() { return XmlManager; }
    }
}
