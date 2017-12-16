using MoteurJeuxProjetFinal.GameEngine.Managers;
using MoteurJeuxProjetFinal.GameEngine.Systems;
using System.Diagnostics;

namespace MoteurJeuxProjetFinal.GameEngine
{
    internal class GameEngine
    {
        // GameEngine states
        private bool is_running;

        // The manager
        private XmlManager _xmlManager = new XmlManager();

        private DisplayWindow _displayWindow = new DisplayWindow();
        private InputManager _inputManager = InputManager.Instance;
        private SystemManager _systemManager = new SystemManager();
        private SceneManager _sceneManager = new SceneManager();
        private EventManager _eventManager = new EventManager();
        private ActionManager _actionManager = new ActionManager();
        private SoundManager _soundManager = new SoundManager();

        public string imagePath;
        public string inputsPath;
        public string audioPath;

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
            _soundManager.Init(this);
            _sceneManager.Init(this);
            _eventManager.Init(this);
            _actionManager.Init(this);

            // Load game file
            _xmlManager.LoadGameFile(gameName);

            // Get properties from game data file.
            GameProperties gameProperties = _xmlManager.LoadGameProperties();
            _displayWindow.InitFormProperties(gameProperties.GameName, gameProperties.ScreenWidth, gameProperties.ScreenHeight);

            // Loas all the scenes and set the current scene
            _sceneManager.InitScenes(2);

            _systemManager.Init(this);
            // Need to add them in the order they will be executed

            _systemManager.AddSystem(new MoveSystem());
            _systemManager.AddSystem(new PhysicsSystem());
            _systemManager.AddSystem(new CollisionSystem());
            _systemManager.AddSystem(new RenderSystem());
            _systemManager.AddSystem(new EventSystem());
            _systemManager.AddSystem(new ScriptSystem());
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
                deltaTime = stopWatch.ElapsedMilliseconds / 1000f;
                stopWatch.Reset();
                stopWatch.Start();

                // Check inputs
                System.Windows.Forms.Application.DoEvents();

                _systemManager.Update(deltaTime);
            }
            // Throw a GameFinishEvent
            _eventManager.AddEvent(new GameFinishEvent());
            // Stop song
            _soundManager.StopBackgroundSound();
              
            // Game engine exit
            Debug.WriteLine("Game engine exited correctly.");
        }

        public void CloseGame()
        {
            _systemManager.End();
            is_running = false;
        }

        // Managers getters
        public XmlManager GetXmlManager() { return _xmlManager; }

        public DisplayWindow GetDisplayWindow()
        {
            return _displayWindow;
        }

        public InputManager GetInputManager()
        {
            return _inputManager;
        }

        public SystemManager GetSystemManager()
        {
            return _systemManager;
        }

        public SceneManager GetSceneManager()
        {
            return _sceneManager;
        }

        public EventManager GetEventManager()
        {
            return _eventManager;
        }

        public ActionManager GetActionManager()
        {
            return _actionManager;
        }

        public SoundManager GetSoundManager()
        {
            return _soundManager;
        }
    }
}