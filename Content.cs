using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace net.core.webapp
{
    [DataContract(Name="liveStations")]
    public class Content
    {
        [DataMember(Name="hits")]
        public List<Station> Stations;
    }
}