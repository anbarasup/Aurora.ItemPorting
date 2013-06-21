using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aurora.ItemPorting.Client.Data.Entities
{
    public class Item : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
        public bool HasChildern { get; set; }
        public IEnumerable<ItemField> Fields { get; set; }
    }
}
