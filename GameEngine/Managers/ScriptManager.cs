using System.Collections.Generic;

namespace MoteurJeuxProjetFinal.GameEngine.Managers
{
    class ScriptManager
    {
        private List<GameScript> scripts = new List<GameScript>();
        private GameEngine _gameEngine;

        public void Init(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        public void AddScript(GameScript script)
        {
            scripts.Add(script);
        }

        /// <summary>
        /// Call the Load mehod of all the script
        /// </summary>
        public void LoadAllScript()
        {
            foreach (GameScript script in scripts)
            {
                script.Load(_gameEngine.GetActionManager());
            }
        }
        /// <summary>
        /// register all the listener of the script
        /// </summary>
        public void RegisterAllScriptListener()
        {
            foreach (GameScript script in scripts)
            {
                foreach (IListener listener in script.GetListenersToRegister())
                {
                    if (listener != null)
                    {
                        _gameEngine.GetEventManager().RegisterListener(listener);
                    }
                }
            }
        }
    }
}