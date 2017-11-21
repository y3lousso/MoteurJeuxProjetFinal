using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Resources;
using System.Globalization;
using System.Drawing;

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
            GameEngine gameEngine = new GameEngine();

            //FOR XML FILE CONTENT
            string path = Environment.CurrentDirectory + "\\Platformer2D";
            gameEngine.imagePath = path + "\\img\\";
            gameEngine.InitForXML(path+ "\\data.xml");
            


            // FOR CODE CONTENT
            // Create a scene
            /*Scene scene = new Scene();
            scene.SetName("Platformer2D");
            scene.backgroundImage = Ressource_Img.background;

            Entity floor = new Entity();
            BoxCollisionComponent boxCollision2 = new BoxCollisionComponent();
            PositionComponent position2 = new PositionComponent();
            position2.position.X = 50;
            position2.position.X = 1000;
            RenderComponent render2 = new RenderComponent();
            render2.image = Image.FromFile(path + "\\img\\background.png");
            render2.size.X = render2.image.Width;
            render2.size.Y = render2.image.Height;

            floor.AddComponent(boxCollision2);
            floor.AddComponent(position2);
            floor.AddComponent(render2);
            scene.AddEntity(floor);

            Entity player = new Entity();
            InputComponent input = new InputComponent();
            PhysicsComponent physic = new PhysicsComponent();
            BoxCollisionComponent boxCollision = new BoxCollisionComponent();
            PositionComponent position = new PositionComponent();
            VelocityComponent velocity = new VelocityComponent();
            RenderComponent render = new RenderComponent();
            render.image = Ressource_Img.mario;
            render.size.X = render.image.Width;
            render.size.Y = render.image.Height;

            player.AddComponent(input);
            player.AddComponent(physic);
            player.AddComponent(boxCollision);
            player.AddComponent(position);
            player.AddComponent(velocity);
            player.AddComponent(render);
        
            scene.AddEntity(player);
                        
            gameEngine.InitForCode(scene);*/

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
