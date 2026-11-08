using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PCRCalculatorServer
{
    // Unity's Newtonsoft converter sends mastery pairs as [slotLevel, enhanceLevel].
    internal sealed class RoleMasteryPairJsonConverter : JsonConverter<(int, int)>
    {
        public override (int, int) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var document = JsonDocument.ParseValue(ref reader);
            var value = document.RootElement;
            if (value.ValueKind == JsonValueKind.Array)
                return (value.GetArrayLength() > 0 ? value[0].GetInt32() : 0,
                    value.GetArrayLength() > 1 ? value[1].GetInt32() : 0);
            if (value.ValueKind == JsonValueKind.Object)
                return (ReadLevel(value, "slotLevel", "Item1", "item1"),
                    ReadLevel(value, "enhanceLevel", "Item2", "item2"));
            throw new JsonException("A role mastery pair must be an array or object.");
        }

        private static int ReadLevel(JsonElement value, params string[] names)
        {
            foreach (var name in names)
                if (value.TryGetProperty(name, out var level))
                    return level.ValueKind == JsonValueKind.Null ? 0 : level.GetInt32();
            return 0;
        }

        public override void Write(Utf8JsonWriter writer, (int, int) value, JsonSerializerOptions options)
        {
            // Preserve the server's existing tuple-object response format.
            writer.WriteStartObject();
            writer.WriteNumber("Item1", value.Item1);
            writer.WriteNumber("Item2", value.Item2);
            writer.WriteEndObject();
        }
    }
}
