using System;
using System.Collections.Generic;

namespace DBFirst_EF.Models
{
    public partial class Shipper
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        // Normal tablodaki fieldlar
        public int ShipperId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? Phone { get; set; }

        // Bağlantılı olduğu diğer tablo veya tablolar
        public virtual ICollection<Order> Orders { get; set; }
    }
}
