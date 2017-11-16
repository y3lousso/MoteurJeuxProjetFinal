using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoteurJeuxProjetFinal
{
    class Entity
    {
        private string name = "Unknown";

        private List<IComponent> _components = new List<IComponent>();

        public void AddComponent(IComponent component)
        {
            _components.Add(component);
        }

        public List<IComponent> GetComponents()
        {
            return _components;
        }

        public void RemoveComponent(IComponent component)
        {
            _components.Remove(component);
        }

        public void SetName(string entityName)
        {
            name = entityName;
        }

        public string GetName()
        {
            return name;
        }
    }
}
