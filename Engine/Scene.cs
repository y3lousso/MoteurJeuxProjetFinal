using System.Collections.Generic;

namespace Engine
{
    public class Scene
    {
        private string name = "Unknown";

        // background image
        public string backgroundImage;

        // background sound
        public string backgroundSound;

        private List<Entity> _entities = new List<Entity>();

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }

        public void ReplaceEntity(Entity oldEntity, Entity newEntity)
        {
            int index = _entities.IndexOf(oldEntity);
            if (index != -1)
            {
                _entities[index] = newEntity;
            }
            // Add the new entity if the old is not found !
            else
            {
                _entities.Add(newEntity);
            }
        }

        public List<Entity> GetEntities()
        {
            return _entities;
        }

        public Entity findEntityWithName(string entityName)
        {
            return _entities.Find(e => e.GetName().Equals(entityName));
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

        public void SetSong(string soundFileName)
        {
            backgroundSound = soundFileName;
        }
    }
}