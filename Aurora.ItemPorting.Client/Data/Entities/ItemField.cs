using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aurora.ItemPorting.Client.Data.Entities
{
    public class ItemField
    {
        public Guid ItemId { get; set; }
        public string Language { get; set; }
        public int Version { get; set; }
        public Guid FieldID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Section { get; set; }
        public string Tooltip { get; set; }
        public string Content { get; set; }
    }
}
