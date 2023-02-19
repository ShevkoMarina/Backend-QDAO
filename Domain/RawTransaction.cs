using System;
using System.Numerics;

namespace QDAO.Domain
{
    public class RawTransaction
    {
        public long Nonce { get; set; }
        public BigInteger Gas { get; set; } = 20000000000;
        public long GasLimit { get; set; }
        public string AddressTo { get; set; }
        public long Value { get; set; }
        public string Data { get; set; }

    }
}

