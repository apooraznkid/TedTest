using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TedTest.Models
{
    public class Item
    {
        public Item(string label, string type, DateTime expiration)
        {
            this.label = label;
            this.type = type;
            this.expiration = expiration;
        }
       
        public string label { get; set; }
        public string type { get; set; }
        public DateTime expiration { get; set; }
    }
}
