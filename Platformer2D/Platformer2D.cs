using System;
using MoteurJeuxProjetFinal.GameEngine;
using MoteurJeuxProjetFinal.Platformer2D.script;

namespace MoteurJeuxProjetFinal.Platformer2D
{
    class Platformer2D
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            // Create instance of game engine
            GameEngine.GameEngine gameEngine = new GameEngine.GameEngine();
            
            // Add the scripts in the game engine
            ScriptManager.GetInstance().AddScript(new TestScript());

            //FOR XML FILE CONTENT
            string path = Environment.CurrentDirectory + "\\Platformer2D";
            gameEngine.imagePath = path + "\\img\\";
            gameEngine.InitForXML(path+ "\\data.xml");
            
            // Load the scripts and register them
            ScriptManager.GetInstance().LoadAllScript();
            ScriptManager.GetInstance().RegisterAllScriptListener();

            gameEngine.RunGameLoop();        
        }
    }
}
