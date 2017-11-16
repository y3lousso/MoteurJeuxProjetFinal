using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace MoteurJeuxProjetFinal
{
    class TransformComponent : IComponent
    {
        // Position in the scene
        Vector2 currentPosition;

        // Next Frame position for collision in the scene
        Vector2 nextPosition;

        // Orientation in the scene : 0 degree means straight right
        float orientation;

        public void Update(float deltaTime)
        {

        }

        public void CalculateNextFramePosition(Vector2 velocity, float deltaTime)
        {
            nextPosition = velocity * deltaTime + currentPosition;
        }

        public void ApplyNextFramePosition()
        {
            currentPosition = nextPosition;
        }

        public Vector2 GetCurrentPosition()
        {
            return currentPosition;
        }

    }
}
