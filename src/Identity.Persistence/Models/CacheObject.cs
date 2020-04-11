using System;

namespace Identity.Persistence.Models
{
    public class CacheObject
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }
}