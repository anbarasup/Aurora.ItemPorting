using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aurora.ItemPorting.Client.Data.Entities
{
    public class TemplateSection
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ItemField> Fields { get; set; }
    }
}
