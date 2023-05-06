using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDAO.Endpoint.DTOs.User
{
    public class AddUserDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Account { get; set; }
    }
}
