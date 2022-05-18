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

        private string puthDirectory;

        private string puthLink;

        private Stack<string> hrefTags;

        private Stack<string> addidHref;

        public event Action<string> pageHandler;

        public CopySite(string link, string nameDirectory)
        {
            this.link = link;
            this.nameDirectory = nameDirectory;
            hrefTags = new Stack<string>();
            addidHref = new Stack<string>();
        }

        public void Copy()
        {
            hrefTags.Push(nameDirectory);

            while (hrefTags.Count > 0)
            {
                using (var handler = new HttpClientHandler())
                {
                    using (var client = new HttpClient(handler))
                    {
                        if (hrefTags.TryPop(out puthLink))
                        {
                            addidHref.Push(puthLink);

                            pageHandler(link + puthLink);

                            if (puthLink.IndexOf(nameDirectory) < 0)
                            {
                                puthDirectory = puthLink.TrimEnd(new char[] { '/' }).Replace("/", "\\").Insert(0, nameDirectory);
                            }
                            else
                            {
                                puthDirectory = puthLink;
                                puthLink = null;
                            }
                        }

                        if (!Directory.Exists(puthDirectory))
                            Directory.CreateDirectory(puthDirectory);

                        if (!File.Exists(puthDirectory + "\\index.html"))
                        {

                            FileStream file = null;

                            try
                            {
                                file = new FileStream($"{puthDirectory}\\index.html", FileMode.Create);
                            }
                            catch
                            {
                                Console.WriteLine($"Ошибка при выполнении {puthDirectory} ") ;
                                continue;
                            }
                            
                            var t = client.GetAsync(link + puthLink);

                            t.Result.Content.ReadAsStreamAsync().Result.CopyTo(file);

                            file.Close();
                        }
                    }
                }

                HtmlAgilityPack();
            }
            
        }

        public void HtmlAgilityPack()
        {
            HtmlDocument htmlSnippet = new HtmlDocument();

            var u = puthDirectory + "\\index.html";

            htmlSnippet.Load(puthDirectory + "\\index.html");

            var t = htmlSnippet.DocumentNode.SelectNodes("//a[@href]");

            if(t != null)
                foreach (HtmlNode link in t)
                {
                    HtmlAttribute att = link.Attributes["href"];
                    if (!hrefTags.Contains(att.Value))
                        if (!addidHref.Contains(att.Value))
                            if (att.Value.StartsWith("/"))
                                if(att.Value.IndexOf("?") == -1)
                                    if (att.Value.IndexOf("html") == -1)
                                        if (att.Value.IndexOf("php") == -1)
                                            if (att.Value.IndexOf("page") == -1)
                                                hrefTags.Push(att.Value);            
                }
        }
    }
}
