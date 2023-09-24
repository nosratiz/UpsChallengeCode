namespace UPS.Common.Serializers;

public interface IJsonSerializer
{
    string Serialize<T>(T value);
    T? Deserialize<T>(string value);
    
    List<T> DeserializeList<T>(string value);
    object? Deserialize(string value, Type type);
}