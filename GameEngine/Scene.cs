using System.Collections.Generic;
using System.Media;

namespace MoteurJeuxProjetFinal.GameEngine
{
    internal class Scene
    {
        private string name = "Unknown";

        // background image
        public string backgroundImage;
        // scene song
        private SoundPlayer song;

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
            song = new SoundPlayer(soundFileName);
        }

        public void PlaySong()
        {
            song.Play();
        }

        public void StopSong()
        {
            song.Stop();
        }
    }
}