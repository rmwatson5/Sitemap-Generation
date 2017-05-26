# Sitemap Generation

[![N|Solid](https://marketplace.sitecore.net/img/modules/large/icon_administration.png)](https://marketplace.sitecore.net/Modules/S/Sitemap_Generation.aspx?sc_lang=en)

The following .dlls need to be added to a Libraries folder in the root of your repository in order to build

  - Sitecore.Analytics.dll
  - Sitecore.ContentSearch.Linq.dll
  - Sitecore.Kernel.dll
  - Sitecore.Mvc.Analytics.dll
  - Sitecore.Mvc.dll
  - System.Web.Mvc.dll

# About!

Generating sitemaps for Sitecore can easily get complicated when it comes to including special pages or if you are working in a multi site environment. While many developers will struggle to create a sitemap.xml file and drop it in the web root while scratching their heads trying to figure out a way to include their sitemap for their other sites. 

This module takes a different approach. Instead of creating a physical sitemap.xml file, this creates a sitemap page within Sitecore. It will look at the site root of the site the page lives under and calls generates a url of each page.

This module also supports wildcard pages. Simple reference the item bucket you have the pages you want to include and the urls will generate through the sitecore LinkManager.

If you have any other pages that are not included under the site root and are not for wildcards, simply reference those pages directly on the custom sitemap page.

This sitemap rendering is also fully cachable under Sitecore and will return in xml format every time.

If you need to add any modifications to the sitemap module, simply override the assembly, I made most my methods virtual. 

Future Enhancements:

Later I will create an update to clear the cache of the sitemap rendering whenever a page gets updated, added, or removed. Also, it will notify your google, bing, yahoo or any other search providers that your sitemap has updated. Stay tuned!

# Included in installation
Sitecore Items:
1. Sitemap Page Template (/sitecore/templates/SEO/Sitemap Page)
2. Sitemap Settings Template (/sitecore/templates/SEO/Sitemap Settings)
3. Sitemap Rendering (/sitecore/layout/Renderings/SEO/Sitemap)
4. Sitemap Layout (/sitecore/layout/Layouts/SitemapLayout)

Files:
1. SBR.Sitemap.dll (\Website\bin\SBR.Sitemap.dll)
2. SitemapLayout.cshtml (\Website\Views\Layouts\SitemapLayout.cshtml)
3. Sitemap.cshtml (\Website\Views\Sitemap\Sitemap.cshtml)

# Instructions for Installation

A wildcard page template will be created under the path /sitecore/templates/SEO/Sitemap Page. The presentation details of the standard values of this page have been configured with a layout that just has a placeholder of "body." There is only one rendering - Sitemap. The rendering is located under /sitecore/layout/Renderings/SEO/Sitemap. The caching is turned on by default for this rendering.

The Sitemap page template has two fields: "Extra Sitemap Pages," and "Item Buckets For Wildcard." The Extra Sitemap Pages field is a multi list with search used for if you need to include any pages in your sitemap that are not under your site root. The Item Buckets For Wildcard field is a multi list with search to include any item buckets that you may use for wildcards. The sitemap processor will call the sitecore Link Manager so if you set up your url generation for wildcards in a custom Link Provider, the urls will be generated as expected. 

BASIC SETUP:
1. Right click under your start path and select "Insert from template"
2. Select Sitemap Page and name the page Sitemap
3. Publish

SETUP WITH EXTRA PAGES:
1. Follow the basic setup procedures
2. On your sitemap page, select any extra pages you want to have included in your sitemap under Extra Sitemap Pages
3. For wildcard pages, select the item bucket the pages you want to include are under.

NOTE: You must make sure you have each of your pages (not including pages you select under Extra Sitemap Pages) inherit from a Sitemap Settings template located under /sitecore/templates/SEO/Sitemap Settings

** Need to make enhancements specific to your site? **
The controller I created for this Sitemap is completely overridable from writing how the pages are retreived to creating an empty virtual method to retreive any extra pages that I have not thought of.
