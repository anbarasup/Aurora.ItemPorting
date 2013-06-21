using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aurora.ItemPorting.Client.Data.Entities;

namespace Aurora.ItemPorting.Client.Comparer
{
    public static class CompareExtensions
    {
        public static bool CompareTemplate(this Template sourceTemplate
            , Template targetTemplate)
        {
            if (sourceTemplate.Id == targetTemplate.Id                 
                && sourceTemplate.Sections.CompareSections(targetTemplate.Sections))
            {

            }
            return false;
        }

        public static bool CompareSections(this IEnumerable<TemplateSection> sourceSections
            , IEnumerable<TemplateSection> targetSections)
        {
            if (sourceSections.Count() == targetSections.Count())
            {
                
            }
            return false;
        }
    }
}
