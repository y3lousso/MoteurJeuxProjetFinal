using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MoteurJeuxProjetFinal
{
    class PlayerComponent : IComponent
    {
        private Vector2 inputForce = new Vector2(0,0);

        public void Update(float deltaTime)
        {

        }
        
        public Vector2 GetInputForce()
        {
            return inputForce;
        }
        
        public void SetInputForce(Vector2 _inputForce)
        {
            inputForce = _inputForce;
        }
    }
}
