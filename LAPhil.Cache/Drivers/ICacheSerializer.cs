using System;
namespace LAPhil.Cache
{
    public interface ICacheSerializer
    {
        byte[] Serialize<T>(T objectToSerialize);
        T Deserialize<T>(byte[] objectToDeserialize);
    }
}
