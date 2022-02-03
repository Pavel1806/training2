using System;

namespace ConvertStringToNumber
{
    public static class ConvertString
    {
        /// <summary>
        /// Метод для преобразования строки в целое число
        /// </summary>
        /// <param name="wordConvert">Строка, которая приходит для перевода в числовое значение</param> // TODO: "Слово" ИМХО неудачное имя, здесь как раз тот случай когда можно написать просто s или value
        /// <returns>Численное значение строки wordConvert</returns> // TODO: А где информация об исключениях?
        public static int ToInt(string wordConvert)
        {

            if (wordConvert == "") // TODO: Как насчёт проверить ещё и на null?
            {
                throw new IndexOutOfRangeException(); // TODO: Текст в исключении не повредил бы. Почему IndexOutOfRangeException а не например ArgumentException
            }
            
            var negativeNumber = false;

            if (wordConvert[0] == '-') // TODO: Мы уверены что первый элемент есть?
            {
                negativeNumber = true;
                wordConvert = wordConvert.Substring(1); // TODO: Почему бы не завести отдельную переменную? Подобные конструкции - ловушки.
            }

            int a;
            int[] arrayInt = new int[wordConvert.Length];

            try
            {
                for (int i = 0; i < wordConvert.Length; i++)
                {
                    char characterFromString = wordConvert[i];
                    a = characterFromString - '0';
                    if (a <= 9)
                    {
                        arrayInt[i] = a;
                    }
                    else
                    {
                        throw new FormatException(); // TODO: Текст в исключении не повредил бы.
                    }
                }

                if (wordConvert.Length > 10)
                {
                    throw new OverflowException();
                }
                else // TODO: else избыточен, здесь и ниже
                {
                    if (wordConvert.Length == 10 && arrayInt[0] > 2)
                    {
                        throw new OverflowException();
                    }
                    else if (wordConvert.Length == 10 && arrayInt[0] == 2) 
                    {
                        if (arrayInt[1] > 1)
                        {
                            throw new OverflowException();
                        }
                        else if (arrayInt[1] == 1)
                        {
                            if (arrayInt[2] > 4)
                            {
                                throw new OverflowException();
                            }
                            else if (arrayInt[2] == 4)
                            {
                                if (arrayInt[3] > 7)
                                {
                                    throw new OverflowException();
                                }
                                else if (arrayInt[3] == 7)
                                {
                                    if (arrayInt[4] > 4)
                                    {
                                        throw new OverflowException();
                                    }
                                    else if (arrayInt[4] == 4)
                                    {
                                        if (arrayInt[5] > 8)
                                        {
                                            throw new OverflowException();
                                        }
                                        else if (arrayInt[5] == 8)
                                        {
                                            if (arrayInt[6] > 3)
                                            {
                                                throw new OverflowException();
                                            }
                                            else if (arrayInt[6] == 3)
                                            {
                                                if (arrayInt[7] > 6)
                                                {
                                                    throw new OverflowException();
                                                }
                                                else if (arrayInt[7] == 6)
                                                {
                                                    if (arrayInt[8] > 4)
                                                    {
                                                        throw new OverflowException();
                                                    }
                                                    else if (arrayInt[8] == 4)
                                                    {
                                                        if (arrayInt[9] > 7)
                                                        {
                                                            throw new OverflowException();
                                                        }
                                                        else if (arrayInt[9] == 7)
                                                        {
                                                            // TODO: ?
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                int number = 0;

                for (int i = 0; i < arrayInt.Length; i++)
                {
                    number += arrayInt[i] * (int)Math.Pow(10, arrayInt.Length - 1 - i);
                }

                if (negativeNumber)
                {
                    number = 0 - number;
                }

                return number;
            }
            catch(Exception)
            {
                throw; // TODO: Зачем это?
            }
        }
    }
}
