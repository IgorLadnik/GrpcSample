using System;
using System.Collections.Generic;

namespace RemoteInterfaces
{
    [Serializable]
    public class Arg1
    {
        public string Id { get; set; }
        public List<Arg2> Arg2Props { get; set; }
    }

    [Serializable]
    public class Arg2
    {
        public string Id { get; set; }
    }
}
