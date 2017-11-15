using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MoteurJeuxProjetFinal
{
    class Game
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            // Create instance of game engine
            GameEngine gameEngine = new GameEngine();
            gameEngine.Init();

            // Create game content


            // Load content from xml file to game engine

            // for test
            gameEngine.GetXmlManager().CreateGameContentFromXMLFile("game1.xml");

            // for the while, we load one specific file, afterward we'll need to load a file specified within the editor
            gameEngine.GetXmlManager().LoadGameContentFromXMLFile("game1.xml");

            // Start the game engine loop with the xml content loaded
            gameEngine.RunGameLoop();        
        }
    }
}
