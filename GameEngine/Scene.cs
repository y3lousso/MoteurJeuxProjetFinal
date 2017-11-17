using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MoteurJeuxProjetFinal
{
    class Scene
    {
        private string name = "Unknown";

        // background image
        public Image backgroundImage;

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
