namespace SBR.Sitemap.Helpers
{
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.SearchTypes;
    public class SitemapSearchResult : SearchResultItem
    {
        [IndexField("disclude_from_sitemap")]
        public bool DiscludeFromSitemap { get; set; }
    }
}
