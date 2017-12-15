using Engine;
using System;

namespace MoteurJeuxProjetFinal.Platformer2D
{
    internal static class Platformer2D
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main(string[] args)
        {
            // Create instance of game engine
            GameEngine gameEngine = new GameEngine();

            //FOR XML FILE CONTENT
            string path = Environment.CurrentDirectory;
            gameEngine.imagePath = path + "\\img\\";
            gameEngine.audioPath = path + "\\sound\\";
            gameEngine.inputsPath = path + "\\inputs.json";
            gameEngine.InitForXml(path + "\\data.xml");

            gameEngine.RunGameLoop();
        }
    }
}