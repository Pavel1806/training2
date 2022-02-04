using System;

namespace ConvertStringToNumber
{
    public static class ConvertString
    {
        /// <summary>
        /// Метод для преобразования строки в целое число
        /// </summary>
        /// <param name="value">Строка, которая приходит для перевода в числовое значение</param> // TODO: "Слово" ИМХО неудачное имя, здесь как раз тот случай когда можно написать просто s или value
        /// <returns>Численное значение строки value</returns> // TODO: А где информация об исключениях?
        /// <FormatException>Ошибка если value не число</FormatException>
        /// <ArgumentNullException>Ошибка при value равном null</ArgumentNullException>
        /// <OverflowException>Ошибка при value не вмещающимся в int</OverflowException>
        /// <IndexOutOfRangeException>Ошибка при пустом значении value</IndexOutOfRangeException>
        public static int ToInt(string value)
        {
            if (value == null) // TODO: Как насчёт проверить ещё и на null?
                throw new ArgumentNullException("Значение переменной value равно null");


            if (value == "") // TODO: Как насчёт проверить ещё и на null?
                throw new IndexOutOfRangeException("Значение переменной value пусто"); // TODO: Текст в исключении не повредил бы. Почему IndexOutOfRangeException а не например ArgumentException

            var negativeNumber = false;
                //string valueWithoutMinus = value;

                if (value[0] == '-') // TODO: Мы уверены что первый элемент есть?
                {
                    negativeNumber = true;
                    value = value.Substring(1); // TODO: Почему бы не завести отдельную переменную? Подобные конструкции - ловушки
                                                // Я здесь так сделал потому что если не будет минуса, то чтение кода пойдет дальше и будет использована переменная value
                                                // Если меняю переменную, то надо вводить еще кусок кода для обработки переменной которая выйдет из этого if
                }

            int a;
            int[] arrayInt = new int[value.Length];

                for (int i = 0; i < value.Length; i++)
                {
                    char characterFromString = value[i];
                    a = characterFromString - '0';
                    if (a <= 9)
                    {
                        arrayInt[i] = a;
                    }
                    else
                    {
                        throw new FormatException("Введено не число"); // TODO: Текст в исключении не повредил бы.
                    }
                }

                if (value.Length > 10)
                    throw new OverflowException("Переменная не вмещает такое число");

                if (value.Length == 10 && arrayInt[0] > 2)
                    throw new OverflowException("Переменная не вмещает такое число");

                if (value.Length == 10 && arrayInt[0] == 2) 
                {
                    if (arrayInt[1] > 1)
                        throw new OverflowException("Переменная не вмещает такое число");

                    if (arrayInt[1] == 1)
                    {
                        if (arrayInt[2] > 4)
                            throw new OverflowException("Переменная не вмещает такое число");
                        if (arrayInt[2] == 4)
                        {
                            if (arrayInt[3] > 7)
                                throw new OverflowException("Переменная не вмещает такое число");
                            if (arrayInt[3] == 7)
                            {
                                if (arrayInt[4] > 4)
                                    throw new OverflowException("Переменная не вмещает такое число");
                                if (arrayInt[4] == 4)
                                {
                                    if (arrayInt[5] > 8)
                                        throw new OverflowException("Переменная не вмещает такое число");
                                    if (arrayInt[5] == 8)
                                    {
                                        if (arrayInt[6] > 3)
                                            throw new OverflowException("Переменная не вмещает такое число");
                                        if (arrayInt[6] == 3)
                                        {
                                            if (arrayInt[7] > 6)
                                                throw new OverflowException("Переменная не вмещает такое число");
                                            if (arrayInt[7] == 6)
                                            {
                                                if (arrayInt[8] > 4)
                                                    throw new OverflowException("Переменная не вмещает такое число");
                                                if (arrayInt[8] == 4)
                                                {
                                                    if (arrayInt[9] > 7)
                                                        throw new OverflowException("Переменная не вмещает такое число");
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
                    number += arrayInt[i] * (int)Math.Pow(10, arrayInt.Length - 1 - i);

                if (negativeNumber)
                    number = 0 - number;

                return number;
        }
    }
}
