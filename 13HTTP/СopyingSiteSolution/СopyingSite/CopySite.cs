using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace СopyingSite
{
    public class CopySite
    {
        private string link;

        private string nameDirectory;

        private string pathDirectory;

        private Stack<string> hrefTags;

        private Stack<string> addidHref;

        private List<string> jpeg;

        private List<string> extensions;

        /// <summary>
        /// Событие для вывода ссылки обрабатываемой страницы
        /// </summary>
        public event Action<string> pageHandler;

        /// <summary>
        /// Конструктор для создания экземпляракласса
        /// </summary>
        /// <param name="link">ссылка на сайт</param>
        /// <param name="nameDirectory">название папки, в которую будет идти сохранение </param>
        public CopySite(string link, string nameDirectory, List<string> extensions)
        {
            this.link = link;
            this.nameDirectory = nameDirectory;
            this.extensions = extensions;
            hrefTags = new Stack<string>();
            addidHref = new Stack<string>();
            jpeg = new List<string>();
        }

        /// <summary>
        /// Создает папку в корне и копирует html-документ страницы в папку
        /// </summary>
        public void Copy()
        {
            string domain = RemovingDomainFromLink();

            hrefTags.Push(nameDirectory);

            while (hrefTags.Count > 0)
            {
                string pathLink = null;

                string url = null;

                if (hrefTags.TryPop(out pathLink))
                {
                    addidHref.Push(pathLink);

                    if (pathLink.IndexOf(nameDirectory) < 0)
                    {
                        pathDirectory = pathLink.TrimEnd(new char[] { '/' }).Replace(".html","").Replace("/", "\\").Insert(0, nameDirectory);
                        url = domain + pathLink;
                    }
                    else
                    {
                        pathDirectory = pathLink;
                        url = link;
                    }

                    pageHandler(url);
                }

                if (!Directory.Exists(pathDirectory))
                    Directory.CreateDirectory(pathDirectory);

                string pathFile = $"{pathDirectory}\\index.html";

                HtmlAgilityPack(url);

                CopyFile(pathFile, url);

                //CopyLinkPicture(url);

                CopyLinkPictureHtml(pathFile);

                if (jpeg.Count >= 1)
                {
                    foreach (var it in extensions)
                    {
                        List<string> i = null;
                        i = jpeg.Where(x => x.IndexOf(it) > -1).ToList();

                        foreach (var item in i)
                        {
                            string link = null;

                            if (!item.StartsWith("http") && item.StartsWith('/'))
                            {
                                link = item.Insert(0, domain);
                            }
                            else if(!item.StartsWith("http") && !item.StartsWith('/'))
                            {
                                jpeg.Remove(item);
                                continue;
                            }
                            else
                            {
                                link = item;
                            }

                            string imgFile = $"{pathDirectory}\\{jpeg.Count}{it}";

                            jpeg.Remove(item);

                            CopyFile(imgFile, link);
                        }
                    }
                }
            } 
        }

        /// <summary>
        /// Выделение домена из ссылки, указанной в данных
        /// </summary>
        /// <returns>домен</returns>
        string RemovingDomainFromLink()
        {
            int i = 0;
            int k = 0;
            string domain = null;

            var quantity = link.Select(x => x.Equals('/')).Where(x=>x == true);

            for (var j = 0; j < link.Count(); j++)
            {
                if (link[j] == '/')
                    ++i;
                if (i == 3)
                {
                    k = j;
                    break;
                }    
            }

            domain = link;

            if (quantity.Count() > 2)
                domain = link.Substring(0, k);

            return domain;
        }

        /// <summary>
        /// Копирование Html-страницы сайта в файл в папке
        /// </summary>
        /// <param name="pathFile"></param>
        /// <param name="pathLink"></param>
        void CopyFile(string pathFile, string pathLink)
        {
            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    if (!File.Exists(pathFile))
                    {

                        FileStream file = null;

                        try
                        {
                            file = new FileStream(pathFile, FileMode.Create);
                        }
                        catch
                        {
                            Console.WriteLine($"Ошибка при выполнении {pathDirectory} ");
                        }

                        client.GetAsync(pathLink).Result.Content.ReadAsStreamAsync().Result.CopyTo(file);

                        file.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Копирование всех ссылок с обрабатываемой страницы
        /// </summary>
        void HtmlAgilityPack(string puthLink)
        {
            HtmlWeb htmlSnippet = new HtmlWeb();

            var u = htmlSnippet.Load(puthLink);

            var t = u.DocumentNode.SelectNodes("//a[@href]");

            if (t != null)
            {
                var e = t.Select(s => s.Attributes["href"])
                .Where(x => x.Value.StartsWith("/"))
                .Where(x => x.Value.IndexOf("?") == -1)
                .Where(x => x.Value.IndexOf("php") == -1);

                foreach (var link in e)
                {
                    //HtmlAttribute att = link.Attributes["href"];
                    if (!hrefTags.Contains(link.Value))
                        if (!addidHref.Contains(link.Value))
                            hrefTags.Push(link.Value);
                }
            }    
        }

        /// <summary>
        /// Копирование всех ссылок с картинками с Html страницы
        /// </summary>
        /// <param name="puthLink">Ссылка на обрабатываемую страницу</param>
        void CopyLinkPicture(string puthLink)
        {
            HtmlWeb htmlSnippet = new HtmlWeb();

            var u = htmlSnippet.Load(puthLink);

            foreach(var item in extensions)
            {
                var t = u.DocumentNode.Descendants().Select(e => e.Attributes.Select(x => x.Value).Where(c => c.IndexOf(item) > -1)); 

                foreach (var tag in t)
                {
                    foreach (var it in tag)
                    {
                        jpeg.Add(it);
                    }
                }
            }
            var listWithoutDuplicates = jpeg.Distinct().ToList();

            jpeg = listWithoutDuplicates;

        }
        /// <summary>
        /// Копирование всех ссылок с картинками с Html страницы, которая находится в папке
        /// </summary>
        /// <param name="puthDirectory"></param>
        void CopyLinkPictureHtml(string puthDirectory)
        {
            HtmlDocument htmlSnippet = new HtmlDocument();

            htmlSnippet.Load(puthDirectory);

            foreach (var item in extensions)
            {
                var t = htmlSnippet.DocumentNode.Descendants().Select(e => e.Attributes.Select(x => x.Value).Where(c => c.IndexOf(item) > -1)); 

                foreach (var tag in t)
                {
                    foreach (var it in tag)
                    {
                        jpeg.Add(it);
                    }
                }
            }
            var listWithoutDuplicates = jpeg.Distinct().ToList();

            jpeg = listWithoutDuplicates;

        }
    }
}
