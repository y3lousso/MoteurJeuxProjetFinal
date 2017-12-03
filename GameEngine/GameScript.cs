using System.Collections.Generic;
using MoteurJeuxProjetFinal.GameEngine.Managers;

namespace MoteurJeuxProjetFinal.GameEngine
{
       
    abstract class GameScript
    {        
        /// <summary>
        /// Method called by the ScriptManager to Load the script
        /// Give an instance of ActionManager to allow the script to act on the game engine
        /// </summary>
        protected internal abstract void Load(ActionManager actionManager);
        
        /// <summary>
        /// Get All the listener to register in the EventManager
        /// </summary>
        protected internal abstract List<IListener> GetListenersToRegister();
    }
}