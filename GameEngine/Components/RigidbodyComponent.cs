using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MoteurJeuxProjetFinal
{
    class RigidbodyComponent : IComponent
    {
        List<Vector2> _forces = new List<Vector2>();

        public Vector2 velocity = new Vector2(0, 0);
        float masse = 1000;

        // Use gravity
        bool UseGravity = false;

        public void Update(float deltaTime)
        {            
            if(UseGravity == true)
            {
                _forces.Add(new Vector2(0, -9.81f * masse));
            }
            // CalculateSumForces
            Vector2 sumForces = new Vector2(0,0);
            foreach (Vector2 force in _forces)
            {
                sumForces += force;
            }

            // Calculate velocity : v = a*t + v0 
            velocity += (sumForces / masse) * deltaTime;
            _forces.Clear();
        }

        public void AddForce(Vector2 force)
        {
            _forces.Add(force);
        }
    }
}
