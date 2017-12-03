using System.Collections.Generic;
using System.Linq;

namespace MoteurJeuxProjetFinal.GameEngine.Managers
{
    class SceneManager
    {

        private GameEngine _gameEngine;
        
        private List<Scene> _scenes = new List<Scene>();
        private int _currentSceneIndex = -1;

        public void Init(GameEngine _gameEngine)
        {
            this._gameEngine = _gameEngine;
        }

        ///  <summary>
        /// Init all the scenes from the xml file, and set the first scene
        /// </summary>
        public void InitScenes(int firstSceneIndex)
        {
            _gameEngine.GetXmlManager().LoadGameContent(ref _scenes);
            _currentSceneIndex = firstSceneIndex;
        }

        /// <summary>
        /// Get all the scenes
        /// </summary>
        public List<Scene> getAllScenes()
        {
            return _scenes;
        }
        
        /// <summary>
        /// Get the current scene displayed in the game
        /// </summary>
        public Scene GetCurrentScene()
        {
            return _currentSceneIndex == -1 ? null : _scenes.ElementAt(_currentSceneIndex);
        }
        /// <summary>
        ///  Display the current scene in the window
        /// </summary>
        public void DisplayCurrentScene()
        {
            Scene scene = _scenes.ElementAt(_currentSceneIndex);
            _gameEngine.GetDisplayWindow().DisplayScene(scene);
            _gameEngine.GetEventManager().AddEvent(new NewSceneDisplayedEvent(scene)); // Event new scene displayed
        }

        /// <summary>
        /// Change the current scene displayed
        /// </summary>
        public void ChangeCurrentScene(Scene scene)
        {
            Scene oldScene = GetCurrentScene();
            
            // Change the current scene index
            int index = _scenes.IndexOf(scene);
            if (index == -1)
            {
                // Add the scene if not in the scene manager
                _scenes.Add(scene);
                _currentSceneIndex = _scenes.Count - 1;
            }
            else
            {
                _currentSceneIndex = index;
            }
            _gameEngine.GetEventManager().AddEvent(new SceneChangeEvent(oldScene, scene)); // Event change changed

        }
        
    }
}