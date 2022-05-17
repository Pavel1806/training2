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

        private string nameDirectory;

        private string puth;

        private Stack<string> hrefTags;

        private Stack<string> addidHref;

        public CopySite(string link, string nameDirectory)
        {
            this.link = link;
            this.nameDirectory = nameDirectory;
            hrefTags = new Stack<string>();
            addidHref = new Stack<string>();
        }

        public void Copy()
        {
            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    if(hrefTags.TryPop(out puth))
                    {
                        addidHref.Push(puth);

                        puth = puth.TrimEnd(new char[] { '/' }).Replace("/", "\\");
                    }
                        
                    if (!Directory.Exists(nameDirectory + puth))
                        Directory.CreateDirectory(nameDirectory + puth);

                    if (!File.Exists(nameDirectory + puth + "\\index.html"))
                    {
                        var file = new FileStream($"{nameDirectory}{puth}\\index.html", FileMode.Create);

                        var t = client.GetAsync(link);

                        t.Result.Content.ReadAsStreamAsync().Result.CopyTo(file);

                        file.Close();
                    }
                }
            }
            
            HtmlAgilityPack();
        }

        public void HtmlAgilityPack()
        {
            HtmlDocument htmlSnippet = new HtmlDocument();

            var u = nameDirectory + puth + "\\index.html";

            htmlSnippet.Load(nameDirectory + puth + "\\index.html");

            var t = htmlSnippet.DocumentNode.SelectNodes("//a[@href]");

            foreach (HtmlNode link in t)
            {
                HtmlAttribute att = link.Attributes["href"];
                if (!hrefTags.Contains(att.Value))
                    if (!addidHref.Contains(att.Value))
                        if (att.Value.StartsWith("/"))
                            if(att.Value.IndexOf("?") == -1)
                                if (att.Value.IndexOf("html") == -1)
                                    if (att.Value.IndexOf("php") == -1)
                                        hrefTags.Push(att.Value);            
            }

            Copy();
        }
    }
}
