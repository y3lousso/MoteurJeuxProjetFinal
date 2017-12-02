using System.Collections.Generic;

namespace MoteurJeuxProjetFinal.GameEngine
{
       
    abstract class GameScript
    {        
        /// <summary>
        /// Method called by the ScriptManager to Load the script
        /// </summary>
        protected internal abstract void Load();
        
        /// <summary>
        /// Get All the listener to register in the EventManager
        /// </summary>
         internal abstract List<IListener> GetListenersToRegister();
    }
}