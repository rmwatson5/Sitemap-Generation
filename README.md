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
