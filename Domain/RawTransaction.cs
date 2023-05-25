using System;
using System.Numerics;

namespace QDAO.Domain
{
    public class RawTransaction
    {
        public long Nonce { get; set; }
        public long Gas { get; set; } = 20000000000;
        public long GasLimit { get; set; } = 400000;
        public string AddressTo { get; set; }
        public long Value { get; set; } = 0;
        public string Data { get; set; }
    }
}

