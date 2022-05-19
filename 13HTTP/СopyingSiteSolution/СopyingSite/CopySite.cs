using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
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

        private Stack<string> jpeg;

        /// <summary>
        /// Событие для вывода ссылки обрабатываемой страницы
        /// </summary>
        public event Action<string> pageHandler;

        /// <summary>
        /// Конструктор для создания экземпляракласса
        /// </summary>
        /// <param name="link">ссылка на сайт</param>
        /// <param name="nameDirectory">название папки, в которую будет идти сохранение </param>
        public CopySite(string link, string nameDirectory)
        {
            this.link = link;
            this.nameDirectory = nameDirectory;
            hrefTags = new Stack<string>();
            addidHref = new Stack<string>();
            jpeg = new Stack<string>();
        }

        /// <summary>
        /// Создает папку в корне и копирует html-документ страницы в папку
        /// </summary>
        public void Copy()
        {
            hrefTags.Push(nameDirectory);

            while (hrefTags.Count > 0)
            {
                string pathLink = null;

                string tyu = null;

                if (hrefTags.TryPop(out pathLink))
                {
                    addidHref.Push(pathLink);

                    if (pathLink.IndexOf(nameDirectory) < 0)
                    {
                        pathDirectory = pathLink.TrimEnd(new char[] { '/' }).Replace("/", "\\").Insert(0, nameDirectory);
                    }
                    else
                    {
                        pathDirectory = pathLink;
                        pathLink = null;
                    }

                    tyu = link + pathLink;

                    pageHandler(tyu);
                }

                if (!Directory.Exists(pathDirectory))
                    Directory.CreateDirectory(pathDirectory);

                string pathFile = $"{pathDirectory}\\index.html";

                HtmlAgilityPack(tyu);

                CopyFile(pathFile, tyu);

                CopyLinkJpeg(tyu);

                if(jpeg.Count >= 1)
                {
                    while (jpeg.Count > 0)
                    {
                        string imgFile = $"{pathDirectory}\\{jpeg.Count}.jpeg";
                        CopyFile(imgFile, jpeg.Pop());
                    }
                }
              }
            
        }


        //void CopyJpeg()
        //{
        //    using (var handler = new HttpClientHandler())
        //    {
        //        using (var client = new HttpClient(handler))
        //        {
        //            client.BaseAddress = new Uri(link);

        //            if (!File.Exists(pathDirectory + "\\index.html"))
        //            {

        //                FileStream file = null;

        //                try
        //                {
        //                    file = new FileStream($"{pathDirectory}\\index.html", FileMode.Create);
        //                }
        //                catch
        //                {
        //                    Console.WriteLine($"Ошибка при выполнении {pathDirectory} ");
        //                }

        //                var t = client.GetAsync(pathLink);

        //                t.Result.Content.ReadAsStreamAsync().Result.CopyTo(file);

        //                file.Close();
        //            }
        //        }
        //    }
        //}

        void CopyFile(string pathFile, string pathLink)
        {
            using (var handler = new HttpClientHandler())
            {
                using (var client = new HttpClient(handler))
                {
                    //client.BaseAddress = new Uri(link);

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

                        var t = client.GetAsync(pathLink);

                        t.Result.Content.ReadAsStreamAsync().Result.CopyTo(file);

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
                foreach (HtmlNode link in t)
                {
                    HtmlAttribute att = link.Attributes["href"];
                    if (!hrefTags.Contains(att.Value))
                        if (!addidHref.Contains(att.Value))
                            if (att.Value.StartsWith("/"))
                                if(att.Value.IndexOf("?") == -1)
                                    if (att.Value.IndexOf("php") == -1)
                                            hrefTags.Push(att.Value);            
                }
        }

        void CopyLinkJpeg(string puthLink)
        {
            HtmlWeb htmlSnippet = new HtmlWeb();

            var u = htmlSnippet.Load(puthLink);

            var t = u.DocumentNode.SelectNodes("//img");

            if (t != null)
                foreach (HtmlNode link in t)
                {
                    HtmlAttribute src = link.Attributes["src"];

                    if (src != null)
                    {
                        Console.WriteLine($"src - {src.Value}");

                        if (src.Value.IndexOf("jpeg") != -1)
                            jpeg.Push(src.Value);
                    }

                    HtmlAttribute dataSrc = link.Attributes["data-src"];

                    if (dataSrc != null)
                    {
                        Console.WriteLine($"data-src - {dataSrc.Value}");

                        if (dataSrc.Value.IndexOf("jpeg") != -1)
                            jpeg.Push(dataSrc.Value);
                    }

                }
        }
    }
}
