using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
    public class ListProductResponse : Response
    {
        public List<Product> ProductList { get; set; }

    }
}
