namespace SBR.Sitemap.Helpers
{
    using System;
    using System.Linq;

    using Sitecore.ContentSearch;
    using Sitecore.Data.Items;
    using Sitecore.Data.Managers;

    public static class SitemapHelpers
    {
        private const string IndexNameTemplate = "sitecore_{0}_index";

        private const string SitemapPageTemplateIdValue = "{6E147EAC-3DEF-4906-BA42-77F17B1A7CF4}";
        public static Guid SitemapPageTemplateId = new Guid(SitemapPageTemplateIdValue);

        private const string SitemapSettingsTemplateIdValue = "{3F733E57-B9BF-459D-AC53-2C8380A96671}";
        public static Guid SitemapSettingsTemplateId = new Guid(SitemapSettingsTemplateIdValue);

        public static bool HasTemplate(this Item item, Guid templateId)
        {
            if (item.TemplateID.Guid.Equals(templateId))
            {
                return true;
            }

            var template = TemplateManager.GetTemplate(item);
            if (template == null)
            {
                return false;
            }

            var baseTemplates = template.GetBaseTemplates();
            var allIds = baseTemplates.Select(bt => bt.ID);
            return allIds.Any(bt => bt.Guid.Equals(templateId));
        }

        public static ISearchIndex GetSearchIndex(Item item)
        {
            var indexable = new SitecoreIndexableItem(item);
            var index = ContentSearchManager.GetIndex(indexable);
            if (index?.Name == null)
            {
                var indexName = string.Format(IndexNameTemplate, item.Database.Name);
                index = ContentSearchManager.GetIndex(indexName);
            }

            return index;
        }
    }
}
