using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4._0_backend.Models
{
    public class Activity
    {
        public int ID { get; set; }
        public int UsersId { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime Created_at { get; set; }
    }
}
