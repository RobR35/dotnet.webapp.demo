using System;
using System.Runtime.Serialization;

namespace net.core.webapp
{
    [DataContract(Name="hits")]
    public class Station
    {
        [DataMember(Name="name")]
        public string Name;
    }
}