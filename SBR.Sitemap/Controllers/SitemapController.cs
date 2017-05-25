namespace SBR.Sitemap.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Models;

    using Helpers;

    using Sitecore.ContentSearch.Linq;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    public class SitemapController : Controller
    {
        protected const string DiscludeFromSitemapFieldName = "Disclude From Sitemap";
        protected const string ExtraPagesFieldName = "Extra Sitemap Pages";
        protected const string ItemBucketsForWildcardFieldName = "Item Buckets For Wildcard";

        public ActionResult Sitemap()
        {
            var model = new SitemapRenderingModel
            {
                Pages = new List<Item>()
            };

            Assert.IsTrue(
                Sitecore.Context.Item.HasTemplate(SitemapHelpers.SitemapPageTemplateId),
                "Sitemap rendering must be on a sitemap page");

            var siteRoot = this.GetSiteRoot();
            if (siteRoot.HasTemplate(SitemapHelpers.SitemapSettingsTemplateId))
            {
                model.Pages.Add(siteRoot);
            }

            var childPages = this.GetChildPages(siteRoot);
            var extraPages = this.GetExtraPages();
            var bucketPages = this.GetItemsFromBuckets();
            var extended = this.GetExtendedPages();

            model.Pages.AddRange(childPages);
            model.Pages.AddRange(extraPages);
            model.Pages.AddRange(bucketPages);
            model.Pages.AddRange(extended);

            return this.View(model);
        }

        /// <summary>
        /// Gets the child pages.
        /// </summary>
        /// <param name="siteRoot">The site root.</param>
        /// <returns></returns>
        protected virtual List<Item> GetChildPages(Item siteRoot)
        {
            if (siteRoot.HasChildren)
            {
                var children = siteRoot.Axes.GetDescendants().AsEnumerable();
                children = children.Where(child => child.HasTemplate(SitemapHelpers.SitemapSettingsTemplateId));
                children = children.Where(child => this.DiscludeFromSitemap(child) == false);
                return children.ToList();
            }

            return new List<Item>();
        }

        /// <summary>
        /// Gets the extra pages.
        /// </summary>
        /// <returns></returns>
        protected virtual List<Item> GetExtraPages()
        {
            var extraPages = new List<Item>();
            Sitecore.Data.Fields.MultilistField extraPagesField = Sitecore.Context.Item.Fields[ExtraPagesFieldName];
            if (extraPagesField.TargetIDs != null)
            {
                extraPages.AddRange(extraPagesField.TargetIDs.Select(targetID => Sitecore.Context.Database.GetItem(targetID)));
            }

            return extraPages;
        }

        /// <summary>
        /// Gets the items from all buckets included.
        /// </summary>
        /// <returns></returns>
        protected virtual List<Item> GetItemsFromBuckets()
        {
            var bucketedItems = new List<Item>();
            Sitecore.Data.Fields.MultilistField bucketField = Sitecore.Context.Item.Fields[ItemBucketsForWildcardFieldName];
            if (bucketField.TargetIDs != null)
            {
                var items = bucketField.TargetIDs.SelectMany(this.GetBucketedItems);
                bucketedItems = items.ToList();
            }

            return bucketedItems;
        }

        /// <summary>
        /// Gets the items included in a bucket.
        /// </summary>
        /// <param name="bucketId">The bucket identifier.</param>
        /// <returns></returns>
        protected virtual List<Item> GetBucketedItems(ID bucketId)
        {
            var searchResults = new List<Item>();
            var bucket = Sitecore.Context.Database.GetItem(bucketId);
            var index = SitemapHelpers.GetSearchIndex(bucket);
            var parentPath = bucket.Paths.FullPath.ToLowerInvariant();

            using (var searchContext = index.CreateSearchContext())
            {
                var itemQuery = searchContext.GetQueryable<SitemapSearchResult>()
                    .Filter(item => item.Path.Contains(parentPath))
                    .Filter(ssr => !ssr.DiscludeFromSitemap);

                searchResults = itemQuery.Select(iq => iq.GetItem()).ToList();
            }

            return searchResults;
        }

        /// <summary>
        /// This method is left open ended in the case where the developer will
        /// need to override this and make any modifications specific to their 
        /// site.
        /// </summary>
        /// <returns></returns>
        protected virtual List<Item> GetExtendedPages()
        {
            return new List<Item>();
        }

        /// <summary>
        /// Gets the site root.
        /// </summary>
        /// <returns></returns>
        protected Item GetSiteRoot()
        {
            Assert.IsNotNull(Sitecore.Context.Site, "The site context is not present. The sitemap did not render");
            var item = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.RootPath);
            return item;
        }

        /// <summary>
        /// Check to see if the template is discluded from the sitemap.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        protected bool DiscludeFromSitemap(Item page)
        {
            Sitecore.Data.Fields.CheckboxField disclude = page.Fields[DiscludeFromSitemapFieldName];
            return disclude.Checked;
        }
    }
}
