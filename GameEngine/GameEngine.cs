﻿using System.Diagnostics;
using MoteurJeuxProjetFinal.GameEngine.Managers;
using MoteurJeuxProjetFinal.GameEngine.Systems;

namespace MoteurJeuxProjetFinal.GameEngine
{
    class GameEngine
    {
        // GameEngine states
        private bool is_running;

        // The manager
        private XmlManager _xmlManager = new XmlManager();
        private DisplayWindow _displayWindow = new DisplayWindow();
        private InputManager _inputManager = new InputManager();
        private SystemManager _systemManager = new SystemManager();
        private SceneManager _sceneManager = new SceneManager();
        private EventManager _eventManager = new EventManager();
        private ScriptManager _scriptManager = new ScriptManager();
        private ActionManager _actionManager = new ActionManager();
     

        public string imagePath;

        /// <summary>
        /// Init game engine
        /// </summary>
        public void InitForXml(string gameName)
        {
            is_running = true;

            // Init managers
            _xmlManager.Init(this);
            _inputManager.Init(this);
            _displayWindow.Init(this);
            _sceneManager.Init(this);
            _eventManager.Init(this);
            _scriptManager.Init(this);
            _actionManager.Init(this);

            // Load game file
            _xmlManager.LoadGameFile(gameName);

            // Get properties from game data file.
            GameProperties gameProperties = _xmlManager.LoadGameProperties();
            _displayWindow.InitFormProperties(gameProperties.GameName, gameProperties.ScreenWidth, gameProperties.ScreenHeight);

            // Loas all the scenes and set the current scene
            _sceneManager.InitScenes(0);
            _sceneManager.DisplayCurrentScene();

            _systemManager.Init(this);
            // Need to add them in the order they will be executed
            _systemManager.AddSystem(new InputSystem());
            _systemManager.AddSystem(new PhysicsSystem());
            _systemManager.AddSystem(new CollisionSystem());
            _systemManager.AddSystem(new MoveSystem());
            _systemManager.AddSystem(new RenderSystem());
            _systemManager.AddSystem(new EventSystem());
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
            
            // Throw a GameStartEvent
            _eventManager.AddEvent(new GameStartEvent());

            // Game loop
            while (is_running)
            {
                stopWatch.Stop();
                deltaTime = stopWatch.ElapsedMilliseconds/1000f;
                stopWatch.Reset();
                stopWatch.Start();

                // Check inputs
                System.Windows.Forms.Application.DoEvents();

                _systemManager.Update(deltaTime);          
            }
            // Game engine exit
            Debug.WriteLine("Game engine exited correctly.");
            
            // Throw a GameFinishEvent
            _eventManager.AddEvent(new GameFinishEvent());
        }

        public void CloseGame()
        { 
            _systemManager.End();
            is_running = false;           
        }

        // Managers getters
        public XmlManager GetXmlManager() { return _xmlManager; }
        public DisplayWindow GetDisplayWindow() { return _displayWindow; }
        public InputManager GetInputManager() { return _inputManager; }
        public SystemManager GetSystemManager() { return _systemManager; }
        public SceneManager GetSceneManager(){ return _sceneManager; }
        public EventManager GetEventManager(){ return _eventManager; }
        public ScriptManager GetScriptManager(){ return _scriptManager; }
        public ActionManager GetActionManager(){ return _actionManager; }


    }
}
