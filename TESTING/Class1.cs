using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TESTING
{

    [Function("add")]
    public class AddTransaction : FunctionMessage
    {
        [Parameter("int256", "a", 1)]
        public virtual BigInteger A { get; set; }

        [Parameter("int256", "b", 2)]
        public virtual BigInteger B { get; set; }
    }
}
