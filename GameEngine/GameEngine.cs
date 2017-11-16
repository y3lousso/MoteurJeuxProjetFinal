using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Input;
using System.Drawing;

namespace MoteurJeuxProjetFinal
{
    class GameEngine
    {
        // GameEngine states
        private bool is_running;

        // Xml file management
        private XML_Manager xmlManager = new XML_Manager();

        // Screen
        private Screen gameScreen = new Screen();

        // InputManager
        private InputManager inputManager = new InputManager();

        // GameEngine content
        private List<Scene> _scenes = new List<Scene>();
        Scene currentScene;

        /// <summary>
        /// Init game engine
        /// </summary>
        public void Init(string gameName)
        {            
            is_running = true;

            // Init inputs manager
            inputManager.Init(this);
            gameScreen.Init(this);

            // Load game file
            xmlManager.LoadGameFile(gameName);
            // Get properties from game data file.
            GameProperties gameProperties = xmlManager.LoadGameProperties();
            gameScreen.InitForm(gameProperties.gameName, gameProperties.screenWidth, gameProperties.screenHeight);

            //Load all entities from xml file
            xmlManager.LoadGameContent( ref _scenes);

            currentScene = _scenes[0];
            if(currentScene.GetName() == "Scene1")
            {
                Console.WriteLine("P'tite bière maintenant :)");
            }
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
                // inputManager.Check

                // Simulate game logic
                /*foreach (Entity entity in currentScene.GetEntities())
                {
                    foreach (IComponent component in entity.GetComponents())
                    {
                        component.Update();
                    }
                }*/
                // Apply outputs
                // form.UpdateSprites ...
                
            }

            // Unload ressources
            // ...

            // Game engine exit
            Debug.WriteLine("Game engine exited correctly.");
        }

        
        public void CloseGame() {
            is_running = false;          
        }


        public XML_Manager GetXmlManager() { return xmlManager; }
        public Screen GetScreen() { return gameScreen; }
        public InputManager GetInputManager() { return inputManager; }
    }
}
