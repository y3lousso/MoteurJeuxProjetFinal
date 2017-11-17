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
/*
        /// <summary>
        /// Load xml file (allow us to create game without editor, you can go directly inside the xml file if you want)
        /// </summary>
        public void LoadGameContent(ref List<Scene> scenes)
        {
            XElement sceneElements = doc.Element("Game.xml").Element("GameContent").Element("Scenes");
            foreach (XElement sceneElement in sceneElements.Descendants())
            {
                Scene currentScene = new Scene();
                currentScene.SetName(sceneElement.Value);
                //XElement background = sceneElement.Element("Background");
                //currentScene.backgroundImagePath = sceneElement.Element("Background").FirstAttribute.Value;           
                scenes.Add(currentScene);
                XElement entityElements = sceneElements.Element("Scene").Element("Entities");
                foreach (XElement entityElement in entityElements.Descendants())
                {
                    Entity currentEntity = new Entity();
                    currentEntity.SetName(entityElement.Value);
                    currentScene.GetEntities().Add(currentEntity);
                    XElement componentElements = entityElements.Element("Entity").Element("Components");
                    foreach (XElement componentElement in componentElements.Descendants())
                    {
                        switch (componentElement.FirstAttribute.Value)
                        {
                            case "Player":
                                currentEntity.AddComponent(new PlayerComponent());
                                break;
                            case "Transform":
                                currentEntity.AddComponent(new TransformComponent());
                                break;
                            case "Rigidbody":
                                currentEntity.AddComponent(new RigidbodyComponent());
                                break;
                            case "Renderer":
                                RendererComponent rendererComponent = new RendererComponent();
                                currentEntity.AddComponent(rendererComponent);
                                rendererComponent.SetProperties(gameEngine.GetScreen());
                                break;
                            default:
                                throw new Exception("Undefined Component");
                        }
                    }
                }
            }
        }

        private void AddScene(string sceneName)
        {

        }
*/
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
