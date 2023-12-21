using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnionAPİ.Domain.Entities
{
    public class Tag:BaseNameableEntity
    {
        public ICollection<TagProduct>? TagProducts { get; set; }
    }
}
