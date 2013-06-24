using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Aurora.ItemPorting.Client.Data.Entities
{
    public class Template : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
        public IEnumerable<TemplateSection> Sections { get; set; }
    }
}
