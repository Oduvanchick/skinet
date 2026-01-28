using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entitites
{
    public class ShoppingCart
    {
        public string Id { get; set; }
        public List<CartItem> Items { get; set; } = [];
    }
}