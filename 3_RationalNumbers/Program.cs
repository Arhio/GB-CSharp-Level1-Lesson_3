using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace _3_RationalNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var ex = new Extension();
            var arrayNumForOnlyNum = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', };
            var q = new Questions();
            WriteLine("С# - Уровень 1. Задание 3.3");
            WriteLine("Кузнецов");
            WriteLine("*  Описать класс дробей — рациональных чисел, являющихся отношением двух целых чисел. Предусмотреть методы сложения, вычитания, умножения и деления дробей. Написать программу, демонстрирующую все разработанные элементы класса." + Environment.NewLine +
                      "*  Добавить свойства типа int для доступа к числителю и знаменателю;" + Environment.NewLine +
                      "*  Добавить свойство типа double только на чтение, чтобы получить десятичную дробь числа;" + Environment.NewLine +
                      "** Добавить проверку, чтобы знаменатель не равнялся 0. Выбрасывать исключение ArgumentException(\"Знаменатель не может быть равен 0\");" + Environment.NewLine +
                      "***Добавить упрощение дробей.");
            Write("Запишите первую дробь:");
            var x = new Rational(int.Parse(q.Question<int>(" ", arrayNumForOnlyNum)),
                                 int.Parse(q.Question<int>("/", arrayNumForOnlyNum)));
            Write("\n\rЗапишите вторую дробь:");
            var y = new Rational(int.Parse(q.Question<int>(" ", arrayNumForOnlyNum)),
                                 int.Parse(q.Question<int>("/", arrayNumForOnlyNum)));
            Write("\n\r");
            var t = new Rational(1, 1);
            WriteLine($"Сложение:  {x.Numerator}/{x.Denominator} + {y.Numerator}/{y.Denominator} = {(x + y)} = {(int)(x + y)} = {(float)(x + y)} = {(double)(x + y)}");
            WriteLine($"Вычитание: {x.Numerator}/{x.Denominator} - {y.Numerator}/{y.Denominator} = {(x - y)} = {(int)(x - y)} = {(float)(x - y)} = {(double)(x - y)}");
            WriteLine($"Умножение: {x.Numerator}/{x.Denominator} * {y.Numerator}/{y.Denominator} = {(x * y)} = {(int)(x * y)} = {(float)(x * y)} = {(double)(x * y)}");
            WriteLine($"Деление:   {x.Numerator}/{x.Denominator} / {y.Numerator}/{y.Denominator} = {(x / y)} = {(int)(x / y)} = {(float)(x / y)} = {(double)(x / y)}");
            //WriteLine($"Вычитание целого числа: {(x + y)} - {(int)(x + y)} = {(x + y) - (int)(x + y)} = {(int)((x + y) - (int)(x + y))} = {(float)((x + y) - (int)(x + y))} = {(double)((x + y) - (int)(x + y))}");
            WriteLine($"Создание типа рациональное число из int: {new Rational(2)}");
            WriteLine($"Создание типа рациональное число из double: {new Rational(3.45)}");
            WriteLine($"Преобразования из int в рациональное число: {(Rational)10}");
            WriteLine($"Преобразования из int неявно в рациональное число: {(t = 10) + 0.5}");
            WriteLine($"Преобразования из double в рациональное число: {(Rational)5.55}");
            WriteLine($"Преобразования из double неявно в рациональное число: {(t = 5.55) + 0.5}");
            WriteLine($"Проверка на 0 в знаменателе {x}: {Rational.CheckRational(x)}");
            WriteLine($"Проверка на 0 в знаменателе {y}, {x}: {Rational.CheckRational(y, x)}");
            WriteLine($"Получить число double из {x}: {x.DecimalNumber}");

            ReadLine();

            new Rational(3, 0); // Проверка на вывод ошибки при 0 в знаминателе 
        }
    }
    
    /// <summary>
    /// Рациональное число
    /// </summary>
    public class Rational
    {
        private int numerator;
        private int denominator;
        private bool isNan = false;

        /// <summary>
        /// Числитель
        /// </summary>
        public int Numerator
        {
            get { return numerator; }
            set { numerator = value; }
        }

        /// <summary>
        /// Знаменатель
        /// </summary>
        public int Denominator
        {
            get { return denominator; }
            set
            {
                if (!CheckNumberForNull(Numerator, value))                
                    this.IsNan = true;
                if (value == 0)
                    throw new System.ArgumentException("Знаменатель не может быть равен 0");

                double Check_value = (double)value / Numerator;
                if (value > 0 && Numerator > 0) Check_value = Math.Abs(Check_value);
                if (value > 0 && Numerator < 0) Check_value = (Math.Abs(Check_value));
                if (value < 0 && Numerator > 0)
                {
                    Check_value = Math.Abs(Check_value);
                    Numerator = -1 * Numerator;
                }
                if (value < 0 && Numerator < 0)
                {
                    Check_value = Math.Abs(Check_value);
                    Numerator = Math.Abs(Numerator);
                }

                double Check_value_Denominator = Math.Truncate(Check_value);
                if (Check_value == Check_value_Denominator)
                {
                    if (value != Math.Abs(Check_value_Denominator))
                    {
                        if (Numerator != 0 && value != 0)
                        {
                            double Check_value_Numerator = Numerator / (Math.Abs(value) / Check_value_Denominator);
                            Numerator = (int)Check_value_Numerator;
                            denominator = (int)Check_value_Denominator;
                        }
                    }
                    else
                    {
                        denominator = value;
                    }                  
                }
                else              
                    denominator = value;                
            }
        }

        /// <summary>
        /// Недопустимое рациональное число
        /// </summary>
        public bool IsNan { get => isNan; private set => isNan = value; }

        /// <summary>
        /// 
        /// </summary>
        public double DecimalNumber { get => (double)Numerator / Denominator; }// Зачем если можно через implicit/explicit?? 

        /// <summary>
        /// Инициализирует новый класс рациональных чисел
        /// </summary>
        /// <param name="numerator"></param>
        /// <param name="denominator"></param>
        public Rational(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        /// <summary>
        /// Инициализирует новый класс рациональных чисел
        /// </summary>
        /// <param name="x"></param>
        public Rational(int x)
        {
            Numerator = x;
            Denominator = 1;
        }

        /// <summary>
        /// Инициализирует новый класс рациональных чисел
        /// </summary>
        /// <param name="x"></param>
        public Rational(double x)
        {
            var r = ConvertToRational(x);
            Numerator = r.numerator;
            Denominator = r.denominator;
        }

        /// <summary>
        /// Инициализирует новый класс рациональных чисел
        /// </summary>
        /// <param name="x"></param>
        public Rational(float x)
        {
            var r = ConvertToRational((double)x);
            Numerator = r.numerator;
            Denominator = r.denominator;
        }

        /// <summary>
        /// Преобразовать рациональное число в строку
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Numerator < Denominator)
                return $"{Numerator}/{Denominator}";
            else if (Denominator == 0)
                return $"{Numerator}/{Denominator}";
            else if (Numerator % Denominator != 0)
                return $"{Numerator / Denominator} {Numerator - (Denominator * (Numerator / Denominator))}/{Denominator}";
            else
                return $"{Numerator / Denominator}";
        }

        /// <summary>
        /// Оператор сложения
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Rational operator +(Rational x, Rational y) =>
            CheckRational(x, y, ((double)x.Numerator / (double)x.Denominator) +
                                ((double)y.Numerator / (double)y.Denominator));
        /// <summary>
        /// Оператор вычитания
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Rational operator -(Rational x, Rational y) =>
            CheckRational(x, y, ((double)x.Numerator / (double)x.Denominator) -
                                ((double)y.Numerator / (double)y.Denominator));

        /// <summary>
        /// Оператор умножения
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Rational operator *(Rational x, Rational y) =>
            CheckRational(x, y, ((double)x.Numerator / (double)x.Denominator) *
                                ((double)y.Numerator / (double)y.Denominator));
        /// <summary>
        /// Оператор деления
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Rational operator /(Rational x, Rational y) =>        
            CheckRational(x, y, ((double)x.Numerator / (double)x.Denominator) /
                                ((double)y.Numerator / (double)y.Denominator));

        /// <summary>
        /// Преобразование неявное рациональнго числа в int 
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator int(Rational x) => x.Numerator / x.Denominator;

        /// <summary>
        /// Преобразование неявное рациональнго числа в double 
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator double(Rational x) => (double)x.Numerator / (double)x.Denominator;

        /// <summary>
        /// Преобразование неявное int в рациональное число 
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator Rational(int x) => new Rational(x, 1);

        /// <summary>
        /// Преобразование явное рационального числа во float
        /// </summary>
        /// <param name="x"></param>
        public static explicit operator float(Rational x) => (float)x.Numerator / (float)x.Denominator;

        /// <summary>
        /// Преобразование неявное в рациональное число
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator Rational(double x) => ConvertToRational(x);

        /// <summary>
        /// Преобразование неявное в рациональное число
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator Rational(float x) => ConvertToRational((float)x);
        
        /// <summary>
        /// Конвертация в рациональное число
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>       
        private static Rational ConvertToRational(double x)
        {
            double Num = 0;
            int Denomi = 0;
            double Check = 1;
            x = Math.Round(x, 7);
            for (int i = 1; Num != Check; ++i)
            {
                Denomi = i;
                Num = x * Denomi;
                Check = Math.Truncate(Num);
            }

            return new Rational((int)Num, Denomi);
        }

        /// <summary>
        /// Проверка на нулевые рациональные значения
        /// </summary>
        /// <param name="x">Первое рациональное число</param>
        /// <param name="y">Второе рациональное число</param>
        /// <returns></returns>
        public static bool CheckRational(Rational x, Rational y) =>        
            (CheckNumberForNull(x.Numerator, y.Numerator)  || 
            (x.Numerator != 0 && y.Numerator != 0 && x.Denominator != 0 && y.Denominator != 0));

        /// <summary>
        /// Проверка на нулевые рациональное значение
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool CheckRational(Rational x) =>
            (CheckNumberForNull(x.Numerator) ||
            (x.Numerator != 0  && x.Denominator != 0));

        /// <summary>
        /// Проверка операции на нулевые рациональные значения с конвертацией в рациональное число с плавающей точкой
        /// </summary>
        /// <param name="x">Первое рациональное число</param>
        /// <param name="y">Второе рациональное число</param>
        /// <param name="z">Число с плавающей точкой</param>
        /// <returns></returns>
        private static Rational CheckRational(Rational x, Rational y, double z)
        {
            if (CheckNumberForNull(x.Numerator, y.Numerator) ||
                (x.Numerator != 0 && y.Numerator != 0 && x.Denominator != 0 && y.Denominator != 0))
                return ConvertToRational(z);
            else
            {
                x.IsNan = true;
                throw new System.ArgumentException("Знаменатель не может быть равен 0");
                return x;
            }
        }

        /// <summary>
        /// Является ли оба из значений нулевым
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool CheckNumberForNull(int x, int y) => (x == 0 && y == 0) ? true : false;

        /// <summary>
        /// Является ли оба из значений нулевым
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static bool CheckNumberForNull(int x) => (x == 0) ? true : false;
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
