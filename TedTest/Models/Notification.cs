using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TedTest.Models
{
    public class Notification
    {
        public string message { get; set; }
    }

    public class ExpirationNotification
    {
        public List<String> messages { get; set; }
    }
}
