using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Xml;

namespace CSharpLAB2Class
{
    /// <summary>
    /// Конструктор абстрактного класса function
    /// </summary>

    abstract class function
    {
        protected abstract double func(double x);
        public double count(double x)
        {
            //Trace.WriteLine(func(x));
            return func(x);
        }
    }
    /// <summary>
    /// Конструктор класса line
    /// Производный класс
    /// </summary>
    /// <param name="a">коэффициент а</param>
    /// <param name="b">коэффициент b
    /// readonly при объявлении поля 
    /// предотвращает изменения поля после инициализации
    /// </param>
    /// <param name="func">метод,который возвращает данную посчитанную функцию</param>
    class line : function
    {
        public readonly double a;
        public readonly double b;

        public line(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        //переопределение получения значения функции
        protected override double func(double x)
        {
            double f = a * x + b;
            Trace.WriteLine("line:" + f);
            return f;
        }
    }
    /// <summary>
    /// Конструктор класса Quadratic
    /// </summary>
    /// <param name="a">коэффициент а</param>
    /// <param name="b">коэффициент b</param>
    /// <param name="c">коэффициент c</param>
    /// <param name="func">метод,который переопределяет функцию</param>

    class Quadratic : function
    {
        public readonly double a;
        public readonly double b;
        public readonly double c;

        public Quadratic(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        protected override double func(double x)
        {
            double f = a * x * x + b * x + c;
            Trace.WriteLine("quadratic:" + f);
            return f;
        }
    }
    /// <summary>
    /// Конструктор класса cube
    /// </summary>  
    /// <param name="a">коэффициент а</param>
    /// <param name="func">метод,который переопределяет функцию</param>

    class cube : function
    {
        public readonly double a;

        public cube(double a)
        {
            this.a = a;
        }

        protected override double func(double x)
        {
            double f = a * x * x * x;
            Trace.WriteLine("cube:" + f);
            return f;
        }
    }

    class program
    {
        /// <summary>
        /// Данный метод читает характеристики функции из файла
        /// </summary>
        /// <param name="splittered">Это массив,в котором хранятся разделенные данные</param>
        /// <returns>Возвращает посчитанную функцию</returns>
        /// <remarks>Значение и характеристики функций берутся из файла input.txt,
        /// поэтому разделяем строку на массив данных (добавляем потом их как коэффициенты функции)
        /// </remarks>

        private static function[] Figures()
        {
            function[] func = new function[3];
            try
            {
                string s;
                StreamReader f = new StreamReader("input.txt");
                while ((s = f.ReadLine()) != null)
                {
                    string[] splittered = s.Split(',');
                    if (splittered[0] == "line") func[0] = new line(Convert.ToDouble(splittered[1]), Convert.ToDouble(splittered[2]));
                    if (splittered[0] == "quadratic") func[1] = new Quadratic(Convert.ToDouble(splittered[1]), Convert.ToDouble(splittered[2]), Convert.ToDouble(splittered[3]));
                    if (splittered[0] == "cube") func[2] = new cube(Convert.ToDouble(splittered[1]));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return func;

        }
        /// <summary>
        /// Точка входа для приложения
        /// </summary>
        /// <param name="args">Список аргументов командой строки</param>
        /// <param name="х">Значение х(заданная точка для функции)</param>

        static void Main(string[] args)
        {
            function[] func = Figures();

            double x = 2;
            foreach (function f in func)
                Console.WriteLine("значение функции {0} для x = {1} равно {2}", f, x, f.count(x));

            Console.ReadLine();
        }
        /// <summary>
        /// Сериализация в файл output.txt
        /// </summary>
        /// <param name="f">Лист функций</param>
        public static void Ser(List<function> f)
        {
            Type[] types = new Type[] { typeof(line), typeof(Quadratic), typeof(cube) };
            var serializer = new XmlSerializer(typeof(function), types);
            using (StreamWriter streamWriter = new StreamWriter("output.txt"))
            {

                foreach (var item in f)
                {
                    serializer.Serialize(streamWriter, item);
                }
            }
            
        }

    }
}

