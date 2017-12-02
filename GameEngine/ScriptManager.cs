using System.Collections.Generic;

namespace MoteurJeuxProjetFinal.GameEngine
{
    class ScriptManager
    {
        private List<GameScript> scripts = new List<GameScript>();
        private GameEngine _gameEngine;
        
       
        private static ScriptManager INSTANCE;
        public ScriptManager() {}
        public static ScriptManager GetInstance() => INSTANCE ?? (INSTANCE = new ScriptManager());

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
                script.Load();
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
                    _gameEngine.GetEventManager().RegisterListener(listener);
                }
            }
        }
    }
}