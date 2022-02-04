using ConvertStringToNumber;
using System;

namespace ErrorHandlingModule
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите слово");
            var wordConsole = Console.ReadLine();

            try
            {
                Console.WriteLine(wordConsole[0]);
            }
            catch(Exception e)
            {

                Console.WriteLine("Вы ничего не ввели!");

                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("Введите число до 2147483647 или до -2147483647");
                string numb = Console.ReadLine();
                int number = ConvertString.ToInt(numb); // TODO: Почему бы не запросить у пользователя число?
                Console.WriteLine(number);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Введено пусто значение null");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Введено пустое значение");
            }
            catch (FormatException)
            {
                Console.WriteLine("Введено не число");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Введено очень большое число");
            }
            catch (Exception e)
            {
                //Мне кажется этот блок в любом случае нужен. Но сообщение отсюда надо вывести в лог. А потребителю общее сообщение
                Console.WriteLine("Что-то прошло не так! Попробуйте еще раз");
            }
        }
    }
}
