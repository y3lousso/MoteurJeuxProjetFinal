using System.Collections.Generic;
using System.Linq;

namespace MoteurJeuxProjetFinal.GameEngine.Managers
{
    class SceneManager
    {

        private GameEngine _gameEngine;
        
        private List<Scene> _scenes = new List<Scene>();
        private int _currentSceneIndex = -1;

        public void Init(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        ///  <summary>
        /// Init all the scenes from the xml file, and set the first scene
        /// </summary>
        public void InitScenes(int firstSceneIndex)
        {
            _gameEngine.GetXmlManager().LoadGameContent(ref _scenes);
            _currentSceneIndex = firstSceneIndex;
            _gameEngine.GetDisplayWindow().DisplayScene(GetScene(firstSceneIndex));
        }

        /// <summary>
        /// Get all the scenes
        /// </summary>
        public List<Scene> GetAllScenes()
        {
            return _scenes;
        }

        /// <summary>
        /// Get a scene with it index
        /// </summary>
        public Scene GetScene(int indexScene)
        {
            if (indexScene >= 0 && indexScene < _scenes.Count)
            {
                return _scenes.ElementAt(indexScene);
            }
            return null;
        }

        /// <summary>
        /// Get a scene with it name
        /// </summary>
        public Scene GetScene(string name)
        {
            return _scenes.Find(scene => scene.GetName().Equals(name));
        }
        
        
        
        /// <summary>
        /// Get the current scene displayed in the game
        /// </summary>
        public Scene GetCurrentScene()
        {
            return _currentSceneIndex == -1 ? null : _scenes.ElementAt(_currentSceneIndex);
        }

        /// <summary>
        /// Change the current scene displayed
        /// </summary>
        public void ChangeCurrentScene(Scene scene)
        {   
            
            Scene oldScene = GetCurrentScene();
            // TODO NOT WOrKing !!!!!!
            //_gameEngine.GetEventManager().AddEvent(new SceneChangeEvent(oldScene, scene)); // Event change changed
            
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
            // Clear the datas of all the systems
            foreach (ISystem system in _gameEngine.GetSystemManager().GetAllSystems())
            {
                system.InitEntities(_gameEngine.GetSceneManager().GetCurrentScene().GetEntities());
            }
            // Display the new scene
            _gameEngine.GetDisplayWindow().DisplayScene(scene);
           
        }
        
    }
}