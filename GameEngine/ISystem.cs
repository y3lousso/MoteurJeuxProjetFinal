﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoteurJeuxProjetFinal
{
    interface ISystem
    {
        void Start(GameEngine _gameEngine);

        void Update(float deltaTime);

        void End();
    }
}
