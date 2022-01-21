using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace P4._0_backend.Models
{
    public class Users
    {
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string email { get; set; }

        public int userLevel { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}
