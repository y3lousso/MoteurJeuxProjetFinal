using System;
using System.Collections.Generic;

namespace MoteurJeuxProjetFinal.GameEngine
{
    internal class Entity
    {
        // All entities need a transform
        // If an entity has a player component, it needs a rigidbody component too

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

        public IComponent GetComponentOfType(Type type)
        {
            foreach (IComponent component in _components)
            {
                if (component.GetType() == type)
                {
                    return component;
                }
            }
            // Component of this type not found
            return null;
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