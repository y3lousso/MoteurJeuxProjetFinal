using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace MoteurJeuxProjetFinal
{
    class XML_Manager
    {
        GameEngine gameEngine;
        public XDocument doc;

        public void Init(GameEngine _gameEngine)
        {
            gameEngine = _gameEngine;
        }

        /// <summary>
        /// Create xml file (allow us to create game without editor, you can go directly inside the xml file if you want)
        /// </summary>
        public void LoadGameFile(string fileName)
        {
            doc = XDocument.Load(fileName);           
        }

        /// <summary>
        /// Load game properties from xml file
        /// </summary>
        public GameProperties LoadGameProperties()
        {            
            GameProperties gameProperties = new GameProperties();
            XElement gamePropertyElement = doc.Element("Game.xml").Element("GameProperties");

            gameProperties.gameName = gamePropertyElement.Element("Name").Value;
            gameProperties.screenWidth = Int32.Parse(gamePropertyElement.Element("Width").Value);
            gameProperties.screenHeight = Int32.Parse(gamePropertyElement.Element("Height").Value);
            return gameProperties;
        }

        /// <summary>
        /// Load xml file (allow us to create game without editor, you can go directly inside the xml file if you want)
        /// </summary>
        public void LoadGameContent(ref List<Scene> _scenes)
        {
            // Scenes
            XElement scenes = doc.Element("Game.xml").Element("GameContent").Element("Scenes");
            foreach (XElement scene in scenes.Elements())
            {
                LoadScene(ref _scenes, scene);                
            }
        }

        public void LoadScene(ref List<Scene> _scenes, XElement scene)
        {
            // Scene
            Scene currentScene = new Scene();
            currentScene.SetName(scene.Attribute("Name").ToString());
            currentScene.backgroundImage = scene.Element("BackgroundImage").Value;
            _scenes.Add(currentScene);

            // Elements
            XElement entityElements = scene.Element("Entities");
            foreach (XElement entityElement in entityElements.Elements())
            {
                LoadEntity(currentScene, entityElement);               
            }
        }

        public void LoadEntity(Scene currentScene, XElement entity)
        {
            // Element
            Entity currentEntity = new Entity();
            currentEntity.SetName(entity.FirstAttribute.ToString());
            currentScene.GetEntities().Add(currentEntity);

            // Components
            XElement components = entity.Element("Components");
            foreach (XElement component in components.Elements())
            {
                LoadComponent(currentEntity,  component);               
            }
        }

        public void LoadComponent(Entity currentEntity, XElement component)
        {
            //Component
            switch (component.Attribute("Type").Value)
            {
                case "Input":
                    InputComponent inputComponent = new InputComponent();
                    inputComponent.inputTweaker = float.Parse(component.Element("inputTweaker").Value);
                    currentEntity.AddComponent(inputComponent);
                    break;
                case "Physics":
                    PhysicsComponent physicsComponent = new PhysicsComponent();
                    physicsComponent.masse = int.Parse(component.Element("masse").Value);
                    physicsComponent.useGravity = bool.Parse(component.Element("useGravity").Value);
                    physicsComponent.useAirFriction = bool.Parse(component.Element("useAirFriction").Value);
                    physicsComponent.airFrictionTweaker = float.Parse(component.Element("airFrictionTweaker").Value);
                    currentEntity.AddComponent(physicsComponent);
                    break;
                case "BoxCollision":
                    BoxCollisionComponent boxCollisionComponent = new BoxCollisionComponent();
                    boxCollisionComponent.size.X = float.Parse(component.Element("sizeX").Value);
                    boxCollisionComponent.size.Y = float.Parse(component.Element("sizeY").Value);
                    currentEntity.AddComponent(boxCollisionComponent);
                    break;
                case "Position":
                    PositionComponent positionComponent = new PositionComponent();
                    positionComponent.position.X = float.Parse(component.Element("posX").Value);
                    positionComponent.position.Y = float.Parse(component.Element("posY").Value);
                    positionComponent.orientation = float.Parse(component.Element("orientation").Value);
                    currentEntity.AddComponent(positionComponent);
                    break;
                case "Velocity":
                    VelocityComponent velocityComponent = new VelocityComponent();
                    velocityComponent.maxVelocity = float.Parse(component.Element("maxVelocity").Value);
                    currentEntity.AddComponent(velocityComponent);
                    break;
                case "Render":
                    RenderComponent renderComponent = new RenderComponent();
                    renderComponent.image = component.Element("image").Value;
                    renderComponent.size.X = int.Parse(component.Element("sizeX").Value);
                    renderComponent.size.Y = int.Parse(component.Element("sizeY").Value);
                    currentEntity.AddComponent(renderComponent);
                    break;
                default:
                    throw new Exception("Undefined Component");
            }
        }




        private void AddScene(string sceneName)
        {

        }

        /*private void AddEntityToScene(string entity, string sceneName, XmlTextWriter writer)
        {
            writer.WriteStartAttribute(sceneName, entity, writer);
            writer.WriteString(entity);
            writer.WriteEndElement();
        }*/
    }

    public struct GameProperties
    {
        public string gameName;
        public int screenWidth;
        public int screenHeight;
    }
}
