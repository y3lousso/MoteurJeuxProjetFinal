using System.Collections.Generic;

namespace MoteurJeuxProjetFinal.GameEngine
{
    class Scene
    {
        private string name = "Unknown";

        // background image
        public string backgroundImage;

        private List<Entity> _entities = new List<Entity>();

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }

        public List<Entity> GetEntities()
        {
            return _entities;
        }

        public void RemoveEntity(Entity entity)
        {
            _entities.Remove(entity);
        }

        public void SetName(string sceneName)
        {
            name = sceneName;
        }

        public string GetName()
        {
            return name;
        }
    }
}
