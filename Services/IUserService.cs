using P4._0_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P4._0_backend.Services
{
    public interface IUserService
    {
        Users Authenticate(string username, string password);
    }
}
