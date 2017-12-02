using System.Diagnostics;
using MoteurJeuxProjetFinal.GameEngine.Systems;

namespace MoteurJeuxProjetFinal.GameEngine
{
    class GameEngine
    {
        // GameEngine states
        private bool is_running;

        // Xml file management
        private XML_Manager _xmlManager = new XML_Manager();
        // Screen
        private DisplayWindow _displayWindow = new DisplayWindow();
        // InputManager
        private InputManager _inputManager = new InputManager();
        // List of systems
        private SystemManager _systemManager = new SystemManager();
        // Scene mananger : to handle the differents scene
        private SceneManager _sceneManager = new SceneManager();
        // Event manager 
        private EventManager _eventManager = new EventManager();

        public string imagePath;

        /// <summary>
        /// Init game engine
        /// </summary>
        public void InitForXML(string gameName)
        {
            is_running = true;

            // Init managers
            _xmlManager.Init(this);
            _inputManager.Init(this);
            _displayWindow.Init(this);
            _sceneManager.Init(this);
            _eventManager.Init(this);

            // Load game file
            _xmlManager.LoadGameFile(gameName);

            // Get properties from game data file.
            GameProperties gameProperties = _xmlManager.LoadGameProperties();
            _displayWindow.InitFormProperties(gameProperties.gameName, gameProperties.screenWidth, gameProperties.screenHeight);

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
        /// Init game engine
        /// </summary>
       /* public void InitForCode(Scene scene)
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
        }*/

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

        public XML_Manager GetXmlManager() { return _xmlManager; }
        public DisplayWindow GetDisplayWindow() { return _displayWindow; }
        public InputManager GetInputManager() { return _inputManager; }
        public SceneManager GetSceneManager(){ return _sceneManager; }
        public EventManager GetEventManager(){ return _eventManager; }

    }
}
