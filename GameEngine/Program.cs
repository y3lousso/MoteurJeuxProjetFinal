using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MoteurJeuxProjetFinal
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
 
        public static void Main(string[] args)
        {
            GameEngine gameEngine = new GameEngine();
            gameEngine.Start();        
        }
    }
}
