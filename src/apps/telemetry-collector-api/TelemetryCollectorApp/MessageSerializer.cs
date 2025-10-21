using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Confluent.Kafka;

namespace TelemetryCollectorApp;

public class MessageSerializer<TMessage> : ISerializer<TMessage>, IDeserializer<TMessage>
{
    private readonly JsonSerializerOptions _options;

    public MessageSerializer(JsonSerializerOptions options=null)
    {
        _options = options??_defaultOptions;
    }
    private static readonly JsonSerializerOptions _defaultOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
    };

    public byte[] Serialize(TMessage data, SerializationContext context)
    {
        return data switch
        {
            string s => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data, _options)),
            _ => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data, _options))
        };
    }

    public TMessage Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return (isNull) 
            ? default 
            : JsonSerializer.Deserialize<TMessage>(data, _options);
    }
}
