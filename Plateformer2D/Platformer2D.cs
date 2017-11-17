using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Resources;
using System.Globalization;
using System.Drawing;

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

            //FOR XML FILE CONTENT
            //string path = Environment.CurrentDirectory + "\\Plateformer2D" +  "\\Platformer2D.xml";
            //gameEngine.InitForXML(path);


            // FOR CODE CONTENT
            // Create a scene
            Scene scene = new Scene();
            scene.SetName("Platformer2D");
            scene.backgroundImage = Plateformer2D.Ressource_Img.background;
            Entity entity1 = new Entity();

            InputComponent input = new InputComponent();

            PhysicsComponent physic = new PhysicsComponent();

            BoxCollisionComponent boxCollision = new BoxCollisionComponent();

            PositionComponent position = new PositionComponent();

            VelocityComponent velocity = new VelocityComponent();

            RenderComponent render = new RenderComponent();
            render.image = Plateformer2D.Ressource_Img.mario;
            render.size.X = render.image.Width;
            render.size.Y = render.image.Height;

            entity1.AddComponent(input);
            entity1.AddComponent(physic);
            entity1.AddComponent(boxCollision);
            entity1.AddComponent(position);
            entity1.AddComponent(velocity);
            entity1.AddComponent(render);

            scene.AddEntity(entity1);

            gameEngine.InitForCode(scene);

            // Start the game engine loop with the xml content loaded
            gameEngine.RunGameLoop();        
        }


        /// <summary>
        /// On game engine event, react with game logic (Ex : reach certain area -> change scene 1 to scene 2 ...)
        /// </summary>
        public void ObjectifEventHandler()
        {


        }

    }
}
