using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace СopyingSite
{
    public class CopySite
    {
        private string link;

        public CopySite(string link)
        {
            this.link = link;
        }

        public void Copy()
        {
            using (var handler = new HttpClientHandler())
            using(var client = new HttpClient(handler))
            {
                var file = new FileStream("image.html", FileMode.Create);

                var t = client.GetAsync(link);

                var y = t.Result.Content.ReadAsStreamAsync();

                y.Result.CopyTo(file);  
            }
        }

        public IEnumerable<string> HtmlAgilityPack()
        {
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.Load("D:\\VisualStudio\\repos\\training\\13HTTP\\СopyingSiteSolution\\SiteFolder\\bin\\Debug\\netcoreapp3.1\\image.html");

            List<string> hrefTags = new List<string>();

            var t = htmlSnippet.DocumentNode.SelectNodes("//a[@href]");

            foreach (HtmlNode link in t)
            {
                HtmlAttribute att = link.Attributes["href"];
                hrefTags.Add(att.Value);
            }

            return hrefTags;
        }
    }
}
