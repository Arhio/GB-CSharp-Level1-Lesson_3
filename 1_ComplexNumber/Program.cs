using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _1_ComplexNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            var ex = new Extension();
            var arrayNumForOnlyNum = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', };
            var q = new Questions();
            WriteLine("С# - Уровень 1. Задание 3.1");
            WriteLine("Кузнецов");
            WriteLine("1.а) Дописать структуру Complex, добавив метод вычитания комплексных чисел.Продемонстрировать работу структуры;" + Environment.NewLine +
                      "  б) Дописать класс Complex, добавив методы вычитания и произведения чисел. Проверить работу класса;");
            Write("Комплексное число n1");
            var complex1 = new Complex<double>(
                double.Parse(q.Question<double>("(X = ", arrayNumForOnlyNum)),
                double.Parse(q.Question<double>(", Y = ", arrayNumForOnlyNum)));
            Write(")\n\r");
            Write("Комплексное число n2");
            var complex2 = new Complex<double>(
                double.Parse(q.Question<double>("(X = ", arrayNumForOnlyNum)),
                double.Parse(q.Question<double>(", Y = ", arrayNumForOnlyNum)));
            Write(")\n\r");
            WriteLine("n1 + n2 = {0:F2}", (complex1 + complex2).ToString());
            WriteLine("n1 - n2 = {0:F2}", (complex1 - complex2).ToString());
            WriteLine("n1 * n2 = {0:F2}", (complex1 * complex2).ToString());
            WriteLine("n1 / n2 = {0:F2}", (complex1 / complex2).ToString());
            ex.Pause();



            //1.а) Дописать структуру Complex, добавив метод вычитания комплексных чисел.Продемонстрировать работу структуры;
            //  б) Дописать класс Complex, добавив методы вычитания и произведения чисел. Проверить работу класса;

            //2.а) С клавиатуры вводятся числа, пока не будет введен 0(каждое число в новой строке).Требуется подсчитать сумму всех нечетных положительных чисел. Сами числа и сумму вывести на экран, используя tryParse;
            //  б) Добавить обработку исключительных ситуаций на то, что могут быть введены некорректные данные.При возникновении ошибки вывести сообщение.Напишите соответствующую функцию;

            //3. * Описать класс дробей -рациональных чисел, являющихся отношением двух целых чисел.Предусмотреть методы сложения, вычитания, умножения и деления дробей. Написать программу, демонстрирующую все разработанные элементы класса. Достаточно решить 2 задачи.Все программы сделать в одном решении.
            //**Добавить проверку, чтобы знаменатель не равнялся 0.Выбрасывать исключение
            //ArgumentException("Знаменатель не может быть равен 0");
            //            Добавить упрощение дробей.
        }

        /// <summary>
        /// Класс запроса данных у пользователя
        /// </summary>
        class Questions
        {
            /// <summary>
            /// Перевести первый символ в заглавный
            /// </summary>
            /// <param name="text"></param>
            /// <returns></returns>
            public string FirstUpper(string text) => text.Substring(0, 1).ToUpper() + (text.Length > 1 ? text.Substring(1) : "");

            /// <summary>
            /// Запрос данных у пользователя
            /// </summary>
            /// <typeparam name="T">Тип выводимых значений</typeparam>
            /// <param name="text">Текст запроса значения у пользователя</param>
            /// <param name="arraySym">Массив допустимых вводимых символов пользователем</param>
            /// <returns></returns>
            public string Question<T>(string text, HashSet<char> arraySym)
            {
                Console.Write(text);
                var textAnswer = new StringBuilder();
                while (true)
                {
                    var symbol = Console.ReadKey(true);
                    if (arraySym.Contains(symbol.KeyChar))
                    {
                        textAnswer.Append(symbol.KeyChar.ToString());
                        Console.Write(symbol.KeyChar.ToString());
                    }

                    if (symbol.Key == ConsoleKey.Backspace && textAnswer.Length > 0)
                    {
                        textAnswer.Remove(textAnswer.Length - 1, 1);
                        Console.Write(symbol.KeyChar.ToString());
                        Console.Write(" ");
                        Console.Write(symbol.KeyChar.ToString());
                    }

                    if (typeof(T) == typeof(string))
                    {
                        if (symbol.Key == ConsoleKey.Enter && textAnswer.Length > 0)
                            break;
                    }
                    else
                        if (symbol.Key == ConsoleKey.Enter &&
                            double.TryParse(textAnswer.ToString()
                                .Replace(".", ","),
                                out var number))
                        break;
                }
                Console.Write("");
                return textAnswer.ToString();
            }
        }

        /// <summary>
        /// Класс расширения возможности консольного приложения
        /// </summary>
        public class Extension
        {
            /// <summary>
            /// Вывести текст на экран
            /// </summary>
            /// <param name="text">Текст выводимый на экран пользователя</param>
            /// <param name="x">Начальная позиция X для выводимого текста</param>
            /// <param name="y">Позиция Y (начиная от верха экрана) для выводимого текста</param>
            public void Print(string text, int x, int y)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(text);
            }

            /// <summary>
            /// Вывести текст на экран
            /// </summary>
            /// <param name="text">Текст выводимый на экран пользователя</param>
            /// <param name="position">Расположение на экране</param>
            /// <param name="y">Позиция Y (начиная от верха экрана) для выводимого текста</param>
            public void Print(string text, PositionForRow position, int y)
            {
                if (position == PositionForRow.Center)
                {
                    var n = (WindowWidth - text.Length) / 2;
                    if (n >= 0)
                        Console.SetCursorPosition(n, y);
                    else
                        Console.SetCursorPosition(0, y);
                    Console.Write(text);
                }

                if (position == PositionForRow.LeftEdge)
                {
                    Console.SetCursorPosition(0, y);
                    Console.Write(text);
                }

                if (position == PositionForRow.RightEdge)
                {
                    var n = (WindowWidth - text.Length);
                    if (n >= 0)
                        Console.SetCursorPosition(n, y);
                    else
                        Console.SetCursorPosition(0, y);
                    Console.Write(text);
                }
            }

            /// <summary>
            /// Пауза приложения
            /// </summary>
            /// <param name="millisec">Продолжительность паузы в миллисекундах</param>
            public void Pause(int millisec) => System.Threading.Thread.Sleep(millisec);
            /// <summary>
            /// Пауза приложения до нажатия любой клавиши пользователем
            /// </summary>
            public void Pause() => ReadKey(true);

        }

        /// <summary>
        /// Позиция на экране
        /// </summary>
        public enum PositionForRow
        {
            Center,
            LeftEdge,
            RightEdge
        }
    }

    /// <summary>
    /// Комплексное число
    /// </summary>
    /// <typeparam name="T">Тип данных комплексного числа (только числовые значения)</typeparam>
    struct Complex<T> where T: struct
    {
        private T a;
        private T b;

        public Complex(T a, T b)
        {
            this.a = a;
            this.b = b;
        }

        public T A { get => a; set => a = value; }
        public T B { get => b; set => b = value; }

        /// <summary>
        /// Преобразование в строковое выражение комплексное число
        /// </summary>
        /// <returns></returns>
        public override string ToString() => String.Format($"(A:{A}, B:{B})");

        /// <summary>
        /// Оператор сложения комплексных чисел
        /// </summary>
        /// <param name="complexA">Первое слагаемое комплексное число</param>
        /// <param name="complexB">Второе слагаемое комплексное число</param>
        /// <returns></returns>
        public static Complex<T> operator + (Complex<T> complexA, Complex<T> complexB) =>
            new Complex<T>((dynamic)complexA.a + (dynamic)complexB.a, (dynamic)complexA.b + (dynamic)complexB.b);

        /// <summary>
        /// Оператор разность комплексных чисел
        /// </summary>
        /// <param name="complexA">Уменьшаемое комплексное число</param>
        /// <param name="complexB">Вычитаемое комплексное число</param>
        /// <returns></returns>
        public static Complex<T> operator - (Complex<T> complexA, Complex<T> complexB) =>
            new Complex<T>((dynamic)complexA.a - (dynamic)complexB.a, (dynamic)complexA.b - (dynamic)complexB.b);

        /// <summary>
        /// Оператор умножение комплексных чисел
        /// </summary>
        /// <param name="complexA">Первый множитель комплексное число</param>
        /// <param name="complexB">Второй множитель комплексное число</param>
        /// <returns></returns>
        public static Complex<T> operator * (Complex<T> complexA, Complex<T> complexB) =>
            new Complex<T>((dynamic)complexA.a * (dynamic)complexB.a, (dynamic)complexA.b * (dynamic)complexB.b);

        /// <summary>
        /// Оператор деления комплексных чисел
        /// </summary>
        /// <param name="complexA">Делимое комплексное число</param>
        /// <param name="complexB">Делитель комплексное число</param>
        /// <returns></returns>
        public static Complex<T> operator / (Complex<T> complexA, Complex<T> complexB) =>
            new Complex<T>((dynamic)complexA.a / (dynamic)complexB.a, (dynamic)complexA.b / (dynamic)complexB.b);
    }
}
