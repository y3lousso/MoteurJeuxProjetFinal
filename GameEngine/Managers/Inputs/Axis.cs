using Newtonsoft.Json;
using System;

namespace MoteurJeuxProjetFinal.GameEngine.Managers.Inputs
{
    internal class Axis : Input
    {
        public Axis(String name, String KeyPlus, String KeyMinus)
        {
            this.Name = name;
            this.KeyMinus = KeyMinus;
            this.KeyPlus = KeyPlus;
            this.IsPressed = false;
            this.Value = 0f;
        }

        public String KeyMinus { get; set; }

        [JsonIgnore]
        public float Value { get; set; }
    }
}