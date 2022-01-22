using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocumentHierarchy
{

    class FileSystemVisitor
    {

        string Puth { get; set; }
        string Word { get; set; }
        List<string> vs { get; set; }

        public FileSystemVisitor(string puth, string word)
        {
            Puth = puth;
            Word = word;
            vs = new List<string>();
        }
        public void StartSearch()
        {
            vs.Add("Поиск начался");
        }
        public void EndSearch()
        {
            vs.Add("Поиск закончился");
            //Console.WriteLine("Осталась последняя директория");
        }


        public List<string> ListDocuments()
        {
            Stack<string> stack = new Stack<string>();

            MyEvent myEvent = new MyEvent();
            myEvent.myEvent += StartSearch;
            myEvent.InvokeEvent();

            vs.Add(Puth);

            stack.Push(Puth);


            while (stack.Count > 0)
            {

                string puth = stack.Pop();

                var directories = Directory.EnumerateDirectories(puth);
                var directories1 = Directory.EnumerateDirectories(puth, Word);

                var file = Directory.EnumerateFiles(puth, Word);



                if (directories1 != null)
                {
                    foreach (var item in directories1)
                    {
                        vs.Add(item);

                    }
                }
                if (directories != null)
                {
                    foreach (var item in directories)
                    {

                        stack.Push(item);
                    }
                }

                if (file != null)
                {
                    foreach (var item in file)
                    {
                        vs.Add(item);
                    }
                }
                if (stack.Count == 0)
                {
                    MyEvent myEvent1 = new MyEvent();
                    myEvent1.myEvent += EndSearch;
                    myEvent1.InvokeEvent();
                }
            }
            return vs;
        }

        public IEnumerable Tree()
        {

            foreach (var item in ListDocuments())
            {
                yield return item;
            }
        }


    }
}

