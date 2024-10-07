using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT_TEST_ASSIGNMENT.Model
{
    public class CryptoMainModel
    {
        public string CoinId { get; set; }
        public int Rank { get; set; }


        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal CoinCapitalization { get; set; }

        public string Explorer {  get; set; }
    }
}
