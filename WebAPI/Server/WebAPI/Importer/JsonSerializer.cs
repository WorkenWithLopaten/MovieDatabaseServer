using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

public static class JSONSerializer<TType> where TType : class
{
    /// <summary>
    /// Serializes an object to JSON
    /// </summary>
    public static string Serialize(TType instance)
    {
        var serializer = new DataContractJsonSerializer(typeof(TType));
        using (var stream = new MemoryStream())
        {
            serializer.WriteObject(stream, instance);
            return Encoding.Default.GetString(stream.ToArray());
        }
    }

    /// <summary>
    /// DeSerializes an object from JSON
    /// </summary>
    public static TType DeserializeJson(string result)
    {
        DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(TType));
        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(result)))
        {
            ms.Position = 0;
            return (TType)jsonSer.ReadObject(ms);
        }
    }
}