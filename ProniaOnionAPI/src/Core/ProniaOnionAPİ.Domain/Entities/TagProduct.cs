using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Domain.Entities
{
    public class TagProduct:BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
        
    }
}
