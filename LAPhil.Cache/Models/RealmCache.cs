using System;
using Realms;


namespace LAPhil.Cache.Realm
{
    public class RealmCache: RealmObject
    {
        [PrimaryKey]
        public string Key { get; set; }
        public byte[] Value { get; set; }
        public DateTimeOffset LastAccess { get; set; }
    }
}
