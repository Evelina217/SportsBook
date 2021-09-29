using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppForSportsBook
{
    class Model
    {
        public class Rootobject
        {
            public Result Results { get; set; }
        }
        public class Result
        {
            public long Id { get; set; }
            public int SportTypeId { get; set; }
            public string Name { get; set; }
            public int EventCode { get; set; }
            public DateTime EventDate { get; set; }
            public int Status { get; set; }
            public string ExtId { get; set; }
            public string EventType { get; set; }
            public string Node { get; set; }
            public bool IsLiveEvent { get; set; }
            public bool IsParlay { get; set; }
            public bool IsBetpalEvent { get; set; }
            public bool IsVirtual { get; set; }
            public Competitor[] Competitors { get; set; }
        }
        public class Competitor
        {
            public string Name { get; set; }
            public int Order { get; set; }
        }
    }
}
