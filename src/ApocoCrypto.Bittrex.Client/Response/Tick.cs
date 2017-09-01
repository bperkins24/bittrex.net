using System;

namespace ApocoCrypto.Bittrex.Client.Response
{
    public class Tick
    {
        public decimal O { get; set; }

        public decimal H { get; set; }

        public decimal L { get; set; }

        public decimal C { get; set; }

        public decimal V { get; set; }

        public DateTime T { get; set; }

        public decimal BV { get; set; }
    }
}