using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Console;

namespace _2_SumAllOddPositiVealues
{
    class Program
    {
        static void Main(string[] args)
        {
            var q = new Questions();
            var ex = new Extension();
            var arrayNumForOnlyNum = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', 'а', 'a' };
            WriteLine("С# - Уровень 1. Задание 3.2");
            WriteLine("Кузнецов");
            WriteLine("С клавиатуры вводятся числа, пока не будет введен 0. Подсчитать сумму всех нечетных положительных чисел.");
            WriteLine("2.а) С клавиатуры вводятся числа, пока не будет введен 0(каждое число в новой строке).Требуется подсчитать сумму всех нечетных положительных чисел. Сами числа и сумму вывести на экран, используя tryParse;" + Environment.NewLine +
                      "  б) Добавить обработку исключительных ситуаций на то, что могут быть введены некорректные данные.При возникновении ошибки вывести сообщение.Напишите соответствующую функцию; " + Environment.NewLine +
                      "Для проверки на обработку недопустимых значений оспользуйте символ 'a'.");

            var value = 0;
            var summ = 0;
            var text = new StringBuilder();

            do
            {
                var b = int.TryParse(q.Question<string>("Введите число: ", arrayNumForOnlyNum), out value);
                Write(Environment.NewLine);

                if (!b)
                {
                    MessageBox.Show("Вы ввели не числовое значение!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    value = 2;
                }
                else                
                    if (value > 0 && value % 2 != 0)
                    {
                        summ += value;
                        if (text.Length == 0)
                            text.Append(value);
                        else
                            text.Append(" + " + value);
                    }
            }
            while (value != 0);

            if (text.Length > 0)
                ex.Print($"{text.ToString()} = {summ}", PositionForRow.Center, WindowHeight / 2);
            else
                ex.Print("Ни одного не введено значения!", PositionForRow.Center, Console.CursorTop + 1);

            ex.Pause();
        }

        /// <summary>
        /// Класс запроса данных у пользователя
        /// </summary>
        class Questions
        {
            /// <summary>
            /// Перевести первый символ в заглавный
            /// </summary>
            /// <param name="text">Корректируемый текст</param>
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

    
}
