using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoDolog
{
    public class Lego
    {
        public string LegoID { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string ColorName { get; set; }
        public int Quantity { get; set; }

        public Lego(string legoID, string name, string colorName, string categoryName, int quantity)
        {
            LegoID = legoID;
            Name = name;
            CategoryName = categoryName;
            ColorName = colorName;
            Quantity = quantity;
        }
    }
}
