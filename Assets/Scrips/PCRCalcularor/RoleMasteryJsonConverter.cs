using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PCRCaculator
{
    public class RoleMasteryJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Dictionary<int, (int slotLevel, int enhanceLevel)>);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            if (value is Dictionary<int, (int slotLevel, int enhanceLevel)> roleMastery)
            {
                foreach (var kvp in roleMastery)
                {
                    writer.WritePropertyName(kvp.Key.ToString());
                    writer.WriteStartArray();
                    writer.WriteValue(kvp.Value.slotLevel);
                    writer.WriteValue(kvp.Value.enhanceLevel);
                    writer.WriteEndArray();
                }
            }

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = new Dictionary<int, (int slotLevel, int enhanceLevel)>();
            if (reader.TokenType == JsonToken.Null)
            {
                return result;
            }

            var obj = JObject.Load(reader);
            foreach (var prop in obj.Properties())
            {
                if (!int.TryParse(prop.Name, out int masteryId))
                {
                    continue;
                }

                result[masteryId] = ReadPair(prop.Value);
            }

            return result;
        }

        private static (int slotLevel, int enhanceLevel) ReadPair(JToken token)
        {
            if (token is JArray array)
            {
                int slotLevel = array.Count > 0 ? array[0]?.Value<int>() ?? 0 : 0;
                int enhanceLevel = array.Count > 1 ? array[1]?.Value<int>() ?? 0 : 0;
                return (slotLevel, enhanceLevel);
            }

            if (token is JObject obj)
            {
                int slotLevel = obj["slotLevel"]?.Value<int>()
                    ?? obj["Item1"]?.Value<int>()
                    ?? obj["item1"]?.Value<int>()
                    ?? 0;
                int enhanceLevel = obj["enhanceLevel"]?.Value<int>()
                    ?? obj["Item2"]?.Value<int>()
                    ?? obj["item2"]?.Value<int>()
                    ?? 0;
                return (slotLevel, enhanceLevel);
            }

            return (0, 0);
        }
    }
}
