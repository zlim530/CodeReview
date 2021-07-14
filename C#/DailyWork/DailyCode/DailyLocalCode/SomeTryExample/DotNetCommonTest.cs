using DotNetCommon;
using DotNetCommon.Extensions;
using System;
using System.Collections.Generic;

namespace DotNetCommonTest
{
    public class DotNetCommonTest
    {
        static void Main0(string[] args)
        {
            var departs = new List<Depart>
            {
                new Depart(){id=1,name="总公司",pid=null,children=null},
                new Depart(){id=2,name="郑州区",pid=1,children=null},
                new Depart(){id=3,name="武汉区",pid=1,children=null},
                new Depart(){id=4,name="中原区",pid=2,children=null},
                new Depart(){id=5,name="二七区",pid=2,children=null},
            };

            var tree = departs.FetchToTree(i => i.id, i => i.pid, i => i.children, i => i.pid == null || i.pid == 0);
            Console.WriteLine(tree);
        }
    }

    public class Depart
    {
        public int id { get; set; }
        public string name { get; set; }
        public int? pid { get; set; }
        public List<Depart> children { get; set; }
    }
}
