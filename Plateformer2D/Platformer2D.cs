using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MoteurJeuxProjetFinal
{
    class Platformer2D
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            // Create instance of game engine
            GameEngine gameEngine = new GameEngine();
            string path = Environment.CurrentDirectory + "\\Plateformer2D" +  "\\Platformer2D.xml";
            gameEngine.Init(path);

            // Start the game engine loop with the xml content loaded
            gameEngine.RunGameLoop();        
        }
    }
}
