namespace SBR.Sitemap.Pipelines.Response.RenderRendering
{
    using Helpers;

    using Sitecore.Mvc.Pipelines.Response.RenderRendering;
    public class SitemapItemProcessor : ExecuteRenderer
    {
        public override void Process(RenderRenderingArgs args)
        {
            if (args.PageContext.Item.HasTemplate(SitemapHelpers.SitemapPageTemplateId))
            {
                System.Web.HttpContext.Current.Response.ContentType = "text/xml";
            }
        }
    }
}
