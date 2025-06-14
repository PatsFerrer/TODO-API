using System.Text.Json;
using System.Text.Json.Serialization;

namespace TodoListApi.Utils
{
    public class DateTimeUtcConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Lê a data em UTC
            return DateTime.Parse(reader.GetString()!).ToUniversalTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // Serializa no formato ISO 8601 com sufixo 'Z' (UTC)
            writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fff zzz"));
        }
    }
}
