using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MoteurJeuxProjetFinal.GameEngine.Managers.Inputs
{
    /// <summary>
    /// Custom converter to convert objects to and from JSON
    /// </summary>
    /// <typeparam name="T">The type of object being passed in</typeparam>
    public abstract class CustomJsonConverter<T> : JsonConverter
    {
        /// <summary>
        /// Abstract method which implements the appropriate create method
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        internal abstract T Create(Type objectType, JObject jsonObject);

        /// <summary>
        /// Determines whether an instance of the current System.Type can be assigned from an instance of the specified Type.
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Reads JSON and returns the appropriate object
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // load the json string
            var jsonObject = JObject.Load(reader);

            // instantiate the appropriate object based on the json string
            var target = Create(objectType, jsonObject);

            // populate the properties of the object
            serializer.Populate(jsonObject.CreateReader(), target);

            // return the object
            return target;
        }

        /// <summary>
        /// Creates the JSON based on the object passed in
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    internal class InputConverter : CustomJsonConverter<Inputs.Input>
    {
        internal override Input Create(Type objectType, JObject jsonObject)
        {
            string typeName = (jsonObject["Type"])?.ToString();
            if (typeName != null)
            {
                switch (typeName)
                {
                    case "Axis":
                        var keyPlus = (jsonObject["KeyPlus"])?.ToString();
                        var keyMinus = (jsonObject["KeyMinus"])?.ToString();
                        var name = (jsonObject["Name"])?.ToString();
                        return new Axis(name, keyPlus, keyMinus);

                    case "Button":
                        var keyPlusB = (jsonObject["KeyPlus"])?.ToString();
                        var nameB = (jsonObject["Name"])?.ToString();
                        return new Inputs.Button(nameB, keyPlusB);

                    default:
                        return null;
                }
            }
            else
                return null;
        }
    }
}