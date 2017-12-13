﻿using System;

namespace MoteurJeuxProjetFinal.GameEngine.Managers.Inputs
{
    internal class Button : Input
    {
        public Button(String name, String KeyPlus)
        {
            this.KeyPlus = KeyPlus;
            this.Name = name;
            this.IsPressed = false;
        }
    }
}