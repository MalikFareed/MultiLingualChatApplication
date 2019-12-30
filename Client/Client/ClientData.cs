using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Client
{
    [Serializable]
    public class ClientData
    {
        public string name { get; set; }
        public string language { get; set; }
        public string msg { get; set; }

      
    }
}
