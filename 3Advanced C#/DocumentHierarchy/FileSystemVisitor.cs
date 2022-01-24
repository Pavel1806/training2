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
        Stack<string> stack { get; set; }

        public FileSystemVisitor(string puth, string word)
        {
            Puth = puth;
            Word = word;
            vs = new List<string>();
        }
        public FileSystemVisitor(string puth)
        {
            Puth = puth;
            vs = new List<string>();
            stack = new Stack<string>();
        }
        public void StartSearch()
        {
            
            Console.WriteLine("Поиск начался");
        }
        public void EndSearch()
        {
            
            Console.WriteLine("Поиск закончился");
        }
        public void StoppedSearch()
        {
            
            Console.WriteLine("Поиск прервался");
        }


        public List<string> ListDocuments(int stop)
        {
            
            MyEvent myEvent = new MyEvent();
            myEvent.myEvent += StartSearch;
            myEvent.InvokeEvent();

            int i = 1;

            if(vs.Count == 0)
               vs.Add(Puth);

            if (stack.Count == 0)
                stack.Push(Puth);

            while (stack.Count > 0)
            {
                if (i != stop)
                {
                    string puth = stack.Pop();

                    var directories = Directory.EnumerateDirectories(puth);

                    var file = Directory.EnumerateFiles(puth);

                    if (directories != null)
                    {
                        foreach (var item in directories)
                        {

                            vs.Add(item);
                            stack.Push(item);
                            Console.WriteLine(item);

                        }
                    }
                    if (file != null)
                    {
                        foreach (var item in file)
                        {
                            vs.Add(item);
                            Console.WriteLine(item);
                        }
                    }
                    if (stack.Count == 0)
                    {
                        MyEvent myEvent1 = new MyEvent();
                        myEvent1.myEvent += EndSearch;
                        myEvent1.InvokeEvent();
                    }
                }
                else
                {
                    MyEvent myEvent1 = new MyEvent();
                    myEvent1.myEvent += StoppedSearch;
                    myEvent1.InvokeEvent();
                    break;
                }
                
                i++;
                
            }
            return vs;
        }

        public void DeleteFiles(string searchWord, string expansion)
        {
            List<string> del = new List<string>();
            foreach(var item in vs)
            {
                int indexOfChar = item.IndexOf(searchWord);

                if (indexOfChar != -1)
                {
                    if(item.EndsWith(expansion))
                    {
                        del.Add(item);
                    }                   
                }               
            }
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Список папок и файлов подходящих для удаления");
           for(int i=0; i<del.Count; i++)
            {
                Console.WriteLine($"{i+1}.{del[i]}");
            }
            Console.WriteLine("Введите номер удаляемого элемента");
            int number = Convert.ToInt32(Console.ReadLine());

            string file = del[number-1];

            foreach(var item in vs)
            {
                if(item.IndexOf(file) == 0)
                {
                    vs.Remove(item);
                    break;
                }
            }
        }

        //public IEnumerable<string> Tree()
        //{

        //    foreach (var item in ListDocuments())
        //    {
        //        yield return item;
        //    }
        //}
    }
}

