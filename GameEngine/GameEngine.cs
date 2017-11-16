using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            xmlManager.Init(this);
            inputManager.Init(this);
            gameScreen.Init(this);

            // Load game file
            xmlManager.LoadGameFile(gameName);
            // Get properties from game data file.
            GameProperties gameProperties = xmlManager.LoadGameProperties();
            gameScreen.InitForm(gameProperties.gameName, gameProperties.screenWidth, gameProperties.screenHeight);

            //Load all entities from xml file
            xmlManager.LoadGameContent(ref _scenes);

            currentScene = _scenes[0];
            if (currentScene.GetName() == "Scene1")
            {
                Console.WriteLine("P'tite bière maintenant :)");
            }
        }

        /// <summary>
        /// Game engine loop
        /// </summary>
        public void RunGameLoop()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            float deltaTime;

            // Game loop
            while (is_running)
            {
                // Get deltaTime
                stopwatch.Stop();
                deltaTime = stopwatch.ElapsedMilliseconds;
                stopwatch.Reset();
                stopwatch.Start();

                // Check inputs
                System.Windows.Forms.Application.DoEvents();

                // Add force via inputs to player 
                foreach (Entity entity in currentScene.GetEntities())
                {
                    foreach (IComponent component in entity.GetComponents())
                    {
                        if (component.GetType() == typeof(PlayerComponent))
                        {
                            ((PlayerComponent)component).SetInputForce(inputManager.inputs.inputXY);
                            ((RigidbodyComponent)entity.GetComponentOfType(typeof(RigidbodyComponent))).AddForce(((PlayerComponent)component).GetInputForce());
                        }
                    }
                }

                // Calculate all rigidbodies + Next frame position
                foreach (Entity entity in currentScene.GetEntities())
                {
                    foreach (IComponent component in entity.GetComponents())
                    {
                        if (component.GetType() == typeof(RigidbodyComponent))
                        {
                            component.Update(deltaTime);
                            ((TransformComponent)entity.GetComponentOfType(typeof(TransformComponent))).CalculateNextFramePosition(((RigidbodyComponent)component).velocity, deltaTime);
                        }
                    }
                }

                //Calculate Collision
                foreach (Entity entity in currentScene.GetEntities())
                {
                    foreach (IComponent component in entity.GetComponents())
                    {
                        if (component.GetType() == typeof(CubeColliderComponent))
                        {
                            // algo de collision -> add force -> recalculate their rigidbodies -> calculate Next frame position 
                        }
                    }
                }

                // Apply Next Frame Transform + 
                foreach (Entity entity in currentScene.GetEntities())
                {
                    foreach (IComponent component in entity.GetComponents())
                    {
                        if (component.GetType() == typeof(TransformComponent))
                        {
                            ((TransformComponent)component).ApplyNextFramePosition();
                        }
                    }
                }

                // Apply Renderer to screen
                gameScreen.ClearScreen();
                foreach (Entity entity in currentScene.GetEntities())
                {
                    foreach (IComponent component in entity.GetComponents())
                    {
                        if (component.GetType() == typeof(RendererComponent))
                        {
                            ((RendererComponent)component).UpdateRenderer(((TransformComponent)entity.GetComponentOfType(typeof(TransformComponent))).GetCurrentPosition());
                        }
                    }
                }

                // 
                // Apply outputs
                // form.UpdateSprites ...
                Thread.Sleep(20);
                gameScreen.UpdateTest();

            }

            // Unload ressources
            // ...

            // Game engine exit
            Debug.WriteLine("Game engine exited correctly.");
        }




        public void CloseGame()
        {
            is_running = false;
        }


        public XML_Manager GetXmlManager() { return xmlManager; }
        public Screen GetScreen() { return gameScreen; }
        public InputManager GetInputManager() { return inputManager; }
    }
}
