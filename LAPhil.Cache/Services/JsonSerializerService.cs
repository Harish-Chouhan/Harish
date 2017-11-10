using System.Text;
using Newtonsoft.Json;

namespace LAPhil.Cache
{
    public class JsonSerializerService: ICacheSerializer
    {
        public byte[] Serialize<T>(T objectToSerialize)
        {
            var result = JsonConvert.SerializeObject(objectToSerialize);
            return Encoding.UTF8.GetBytes(result);
        }

        public T Deserialize<T>(byte[] objectToDeserialize)
        {
            var json = Encoding.UTF8.GetString(objectToDeserialize);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
