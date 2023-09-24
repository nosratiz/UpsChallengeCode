using System.Text.Json;
using System.Text.Json.Serialization;

namespace UPS.Common.Serializers;

public class TextJsonSerializer : IJsonSerializer
{
    private static readonly JsonSerializerOptions? Options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        
        Converters = {new JsonStringEnumConverter()}
    };

    public string Serialize<T>(T value) => JsonSerializer.Serialize(value, Options);

    public T? Deserialize<T>(string value) => JsonSerializer.Deserialize<T>(value, Options);
    public List<T> DeserializeList<T>(string value)
    {
        var list = JsonSerializer.Deserialize<List<T>>(value, Options);
        return list ?? new List<T>();
    }

    public object? Deserialize(string value, Type type) => JsonSerializer.Deserialize(value, type, Options);
}