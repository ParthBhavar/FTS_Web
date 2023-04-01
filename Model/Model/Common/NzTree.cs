using System;
using System.Collections.Generic;
using System.Text;

namespace FTS.Model.Common
{
    public class NzTree
    {
        public string title { get; set; }
        public string value { get; set; }
        public string key { get; set; }
        public bool expanded { get; set; }        
        public List<NzTreechildren> children { get; set; }
    }

    public class NzTreechildren
    {
        public string title { get; set; }
        public string value { get; set; }
        public string key { get; set; }
        public bool isLeaf { get; set; }
        public List<NzTreechildren> children { get; set; }

        public NzTreechildren()
        {
            isLeaf = true;
        }
    }
}
