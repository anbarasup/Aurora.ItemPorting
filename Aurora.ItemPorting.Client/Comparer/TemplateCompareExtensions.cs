using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aurora.ItemPorting.Client.Data.Entities;

namespace Aurora.ItemPorting.Client.Comparer
{
    /// <summary>
    /// Contains list of extension function used to compare template
    /// </summary>
    public static class TemplateCompareExtensions
    {
        public static bool CompareTemplate(this Template sourceTemplate
            , Template targetTemplate)
        {
            if (sourceTemplate.Id == targetTemplate.Id                 
                && sourceTemplate.Sections.CompareSections(targetTemplate.Sections))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Extension function used to compare source sections against target sections
        /// </summary>
        /// <param name="sourceSections">List of Source Sections</param>
        /// <param name="targetSections">List of Target Sections</param>
        /// <returns>Returns true if there is a match else false</returns>
        public static bool CompareSections(this IEnumerable<TemplateSection> sourceSections
            , IEnumerable<TemplateSection> targetSections)
        {
            if (sourceSections.Count() == targetSections.Count())
            {
                foreach (TemplateSection sourceSection in sourceSections)
                {
                    TemplateSection targetSection = null;
                    if ((targetSection = sourceSection.IsTemplateSectionAvailableInTarget(targetSections)) == null)
                        return false;
                    else
                    {
                        if (sourceSection.Fields.CompareFields(targetSection.Fields))
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Function used to compare whether the source 
        /// and target fields are same
        /// </summary>
        /// <param name="sourceFields"></param>
        /// <param name="targetFields"></param>
        /// <returns></returns>
        public static bool CompareFields(this IEnumerable<ItemField> sourceFields
            , IEnumerable<ItemField> targetFields)
        {

            if (sourceFields.Count() == targetFields.Count())
            {
                foreach (ItemField sourceField in sourceFields)
                {
                    if (!sourceField.IsFieldIsAvailableInTarget(targetFields))
                        return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Function used to check whether the source field is available in 
        /// list of target fields
        /// </summary>
        /// <param name="sourceField">Source Field</param>
        /// <param name="targetFields">List of Target Fields</param>
        /// <returns>return true is available else false</returns>
        private static bool IsFieldIsAvailableInTarget(this ItemField sourceField
            , IEnumerable<ItemField> targetFields)
        {
            foreach (ItemField targetField in targetFields)
            {
                if (sourceField.FieldID == targetField.FieldID
                    && sourceField.Name.Trim().ToLower() == targetField.Name.Trim().ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Function used to check whether sourcesection is available in the 
        /// list of target section
        /// </summary>
        /// <param name="sourceSection">Source Section</param>
        /// <param name="targetSections">List of Target Sections</param>
        /// <returns>Returns the matched target section or null if not available in the target list</returns>
        private static TemplateSection IsTemplateSectionAvailableInTarget(this TemplateSection sourceSection, IEnumerable<TemplateSection> targetSections)
        {
            foreach (TemplateSection targetSection in targetSections)
            {
                if (sourceSection.Id == targetSection.Id
                    && sourceSection.Name.Trim().ToLower() == targetSection.Name.Trim().ToLower())
                {
                    return targetSection;
                }
            }

            return null;
        }
    }
}
