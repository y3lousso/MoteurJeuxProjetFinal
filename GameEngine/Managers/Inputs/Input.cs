using Newtonsoft.Json;
using System;

namespace MoteurJeuxProjetFinal.GameEngine.Managers.Inputs
{
    internal class Input
    {
        public String Name { get; set; }
        public String KeyPlus { get; set; }

        [JsonIgnore]
        public bool IsPressed { get; set; }
    }
}