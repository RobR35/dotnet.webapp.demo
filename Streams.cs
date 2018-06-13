using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace net.core.webapp
{
    [DataContract(Name="streams")]
    public class Streams
    {
        [DataMember(Name="shoutcast_stream")]
        public string Stream;
    }
}
