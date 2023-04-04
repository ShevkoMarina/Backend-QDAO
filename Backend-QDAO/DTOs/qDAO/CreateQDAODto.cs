using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDAO.Endpoint.DTOs.qDAO
{
    public class CreateQDAODto
    {
        public string AdminAddress { get; set; }
        public uint Delay { get; set; }
        public uint TotalSupply { get; set; }
    }
}
