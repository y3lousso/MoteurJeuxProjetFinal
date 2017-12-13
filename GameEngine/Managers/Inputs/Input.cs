using Newtonsoft.Json;
using System;

namespace MoteurJeuxProjetFinal.GameEngine.Managers.Inputs
{
    internal abstract class Input
    {
        public String Name { get; set; }
        public String KeyPlus { get; set; }

        [JsonIgnore]
        public bool IsPressed { get; set; }

        protected string _type;
        public string Type { get => _type; }

        public override string ToString()
        {
            return Name;
        }
    }
}