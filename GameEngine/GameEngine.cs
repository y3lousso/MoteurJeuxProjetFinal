﻿using System;
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
        private DisplayWindow displayWindow = new DisplayWindow();
        // InputManager
        private InputManager inputManager = new InputManager();

        // List of systems
        private SystemManager systemManager = new SystemManager();

        // GameEngine content
        private List<Scene> _scenes = new List<Scene>();
        Scene currentScene;

 /*       /// <summary>
        /// Init game engine
        /// </summary>
        public void InitForXML(string gameName)
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
            gameScreen.DisplayScene(currentScene);
        }
*/
        /// <summary>
        /// Init game engine
        /// </summary>
        public void InitForCode(Scene scene)
        {
            is_running = true;
            currentScene = scene;

            // Init inputs manager
            inputManager.Init(this);
            displayWindow.Init(this);
            displayWindow.InitForm("Plateformer2D", 1280, 720);
            

            systemManager.Init(this);

            // Need to add them in the order they will be executed
            systemManager.AddSystem(new InputSystem());
            systemManager.AddSystem(new PhysicsSystem());
            systemManager.AddSystem(new CollisionSystem());
            systemManager.AddSystem(new MoveSystem());
            systemManager.AddSystem(new RenderSystem());
        }

        /// <summary>
        /// Game engine loop
        /// </summary>
        public void RunGameLoop()
        {
            // Init Timer
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            float deltaTime;

            // Game loop
            while (is_running)
            {
                stopWatch.Stop();
                deltaTime = stopWatch.ElapsedMilliseconds/1000f;
                stopWatch.Reset();
                stopWatch.Start();

                // Check inputs
                System.Windows.Forms.Application.DoEvents();

                systemManager.Update(deltaTime);          
            }
            // Game engine exit
            Debug.WriteLine("Game engine exited correctly.");
        }




        public void CloseGame()
        { 
            systemManager.End();
            is_running = false;           
        }

        public XML_Manager GetXmlManager() { return xmlManager; }
        public DisplayWindow GetDisplayWindow() { return displayWindow; }
        public InputManager GetInputManager() { return inputManager; }
        public Scene GetCurrentScene() { return currentScene; }
    }
}
