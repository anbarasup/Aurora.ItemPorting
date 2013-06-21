using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aurora.ItemPorting.Client.Data.Entities;

namespace Aurora.ItemPorting.Client.Data
{
    public class OldWebService : WebService
    {
        public OldWebService()
            :base()
        {

        }
        public OldWebService(Connection connection)
            :base(connection)
        {

        }

        public override IEnumerable<Template> GetTemplates()
        {
            IEnumerable<Template> templates = base.GetTemplates().ToList();

            foreach (Template template in templates)
            {
                IEnumerable<Item> sections = this.GetChildern(template.Id);
                template.Sections = (from section in sections
                                    select new TemplateSection()
                                    {
                                        Id = section.Id
                                        , 
                                        Name = section.Name
                                    }).ToList();

                foreach (TemplateSection section in template.Sections)
                {
                    IEnumerable<Item> fields = this.GetChildern(section.Id);
                    section.Fields = (from field in fields
                                        select new ItemField()
                                        {
                                            FieldID = field.Id
                                            ,
                                            Name = field.Name
                                        }).ToList();
                }
            }

            return templates;
        }
    }
}
