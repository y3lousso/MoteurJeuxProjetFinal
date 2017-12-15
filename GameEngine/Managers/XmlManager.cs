using MoteurJeuxProjetFinal.GameEngine.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace MoteurJeuxProjetFinal.GameEngine.Managers
{
    internal class XmlManager
    {
        private GameEngine _gameEngine;
        private XDocument _doc;

        public void Init(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        /// <summary>
        /// Create xml file (allow us to create game without editor, you can go directly inside the xml file if you want)
        /// </summary>
        public void LoadGameFile(string fileName)
        {
            _doc = XDocument.Load(fileName);
        }

        /// <summary>
        /// Load game properties from xml file
        /// </summary>
        public GameProperties LoadGameProperties()
        {
            GameProperties gameProperties = new GameProperties();
            XElement gamePropertyElement = _doc.Element("Game.xml")?.Element("GameProperties");

            if (gamePropertyElement != null)
            {
                gameProperties.GameName = gamePropertyElement.Element("Name")?.Value;
                gameProperties.ScreenWidth = int.Parse(gamePropertyElement.Element("Width")?.Value);
                gameProperties.ScreenHeight = int.Parse(gamePropertyElement.Element("Height")?.Value);
            }
            return gameProperties;
        }

        /// <summary>
        /// Load xml file (allow us to create game without editor, you can go directly inside the xml file if you want)
        /// </summary>
        public void LoadGameContent(ref List<Scene> scenes)
        {
            // Scenes
            XElement sceneList = _doc.Element("Game.xml")?.Element("GameContent")?.Element("Scenes");
            if (sceneList != null)
                foreach (XElement scene in sceneList.Elements())
                {
                    LoadScene(ref scenes, scene);
                }
        }

        public void LoadScene(ref List<Scene> scenes, XElement scene)
        {
            // Scene
            Scene currentScene = new Scene();
            currentScene.SetName(scene.Attribute("Name")?.ToString());
            currentScene.backgroundImage = scene.Element("BackgroundImage")?.Value;
            currentScene.SetSong(_gameEngine.audioPath + scene.Element("BackgroundSound")?.Value);
            scenes.Add(currentScene);

            // Elements
            XElement entityElements = scene.Element("Entities");
            if (entityElements != null)
                foreach (XElement entityElement in entityElements.Elements())
                {
                    LoadEntity(currentScene, entityElement);
                }
        }

        public void LoadEntity(Scene currentScene, XElement entity)
        {
            // Element
            Entity currentEntity = new Entity();
            currentEntity.SetName(entity.FirstAttribute.Value);
            currentScene.GetEntities().Add(currentEntity);

            // Components
            XElement components = entity.Element("Components");
            if (components != null)
                foreach (XElement component in components.Elements())
                {
                    LoadComponent(currentEntity, component);
                }
        }

        public void LoadComponent(Entity currentEntity, XElement component)
        {
            //Component
            switch (component.Attribute("Type")?.Value)
            {
                case "Physics":
                    PhysicsComponent physicsComponent = new PhysicsComponent();
                    physicsComponent.masse = int.Parse(component.Element("masse")?.Value);
                    physicsComponent.useGravity = bool.Parse(component.Element("useGravity")?.Value);
                    physicsComponent.useAirFriction = bool.Parse(component.Element("useAirFriction")?.Value);
                    physicsComponent.airFrictionTweaker = float.Parse(component.Element("airFrictionTweaker")?.Value, CultureInfo.InvariantCulture);
                    currentEntity.AddComponent(physicsComponent);
                    break;

                case "BoxCollision":
                    BoxCollisionComponent boxCollisionComponent = new BoxCollisionComponent();
                    boxCollisionComponent.size.X = float.Parse(component.Element("sizeX")?.Value);
                    boxCollisionComponent.size.Y = float.Parse(component.Element("sizeY")?.Value);
                    boxCollisionComponent.isTrigger = bool.Parse(component.Element("isTrigger")?.Value);
                    currentEntity.AddComponent(boxCollisionComponent);
                    break;

                case "Position":
                    PositionComponent positionComponent = new PositionComponent();
                    positionComponent.position.X = float.Parse(component.Element("posX")?.Value);
                    positionComponent.position.Y = float.Parse(component.Element("posY")?.Value);
                    positionComponent.orientation = float.Parse(component.Element("orientation")?.Value);
                    currentEntity.AddComponent(positionComponent);
                    break;

                case "Velocity":
                    VelocityComponent velocityComponent = new VelocityComponent();
                    velocityComponent.maxVelocity = float.Parse(component.Element("maxVelocity")?.Value);
                    currentEntity.AddComponent(velocityComponent);
                    break;

                case "Render":
                    RenderComponent renderComponent = new RenderComponent();
                    renderComponent.image = component.Element("image")?.Value;
                    renderComponent.size.X = int.Parse(component.Element("sizeX")?.Value);
                    renderComponent.size.Y = int.Parse(component.Element("sizeY")?.Value);
                    currentEntity.AddComponent(renderComponent);
                    break;

                case "Script":
                    ScriptComponent scriptComponent = new ScriptComponent();
                    scriptComponent.Script = CreateScriptInstance(component.Element("scriptName")?.Value);
                    currentEntity.AddComponent(scriptComponent);
                    break;

                default:
                    throw new Exception("Undefined Component");
            }
        }

        private GameScript CreateScriptInstance(string scriptName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type type = assembly.GetTypes().First(t => t.Name.Equals(scriptName));

            return (GameScript)Activator.CreateInstance(type);
        }
    }

    public struct GameProperties
    {
        public string GameName;
        public int ScreenWidth;
        public int ScreenHeight;
    }
}