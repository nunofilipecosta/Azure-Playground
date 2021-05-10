using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleMessageApp.Client.Models
{
    public class StorageAccountSettings
    {
        public string ConnectionString { get; set; }

        public string QueueName { get; set; }
    }
}
