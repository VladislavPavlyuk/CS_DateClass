using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CS_DateClass
{
    public class Program
    {

        static void Main(string[] args)
        {

            Date date1 = new Date();

            date1 = DateInput();

            Print(date1);

            Date date2 = new Date();

            date2 = DateInput();

            Print(date2);

            Date date3 = new Date();          

            int difference = date3.DiffDate(date1, date2); // метод, возвращающий разницу между двумя датами в днях;
            date3.ConvertDaysToDate(difference);

            Console.Write("\nDifference of the two dates is " + difference + " days.");
            Console.Write("\n that is " + date3);
            Console.ReadKey();

            Date date4 = date3;

            Console.Write("\nВведите количество не (+/- увеличить/уменьшить) : ");

            int dd = Int32.Parse(Console.ReadLine());

            date4.AddDays(dd);          // метод изменения даты на заданное количество дней;

            int days = date4.ConvertDateToDays();

            Print(date4);



            void Print(Date date)           //  метод вывода даты на экран.
            {
                Console.WriteLine(date.ToString() + " " + date.Day_Of_Week(date));

                Console.ReadKey();
            }

            Date DateInput()
            {
                Console.Write("\nВведите дату (ДД/ММ/ГГГГ) : ");

                string strDate = Console.ReadLine();

                if (strDate ==  "") { strDate = "22/01/1976"; }

                string[] subs = strDate.Split(' ', '/');

                int d = 0, m = 0, y = 0, i = 0;

                int[] dateArray = { d, m, y };

                foreach (var sub in subs)
                {
                    //Console.WriteLine($"Substring: {sub}");

                    dateArray[i] = Int32.Parse(sub);

                    i++;
                }
                d = dateArray[0];
                m = dateArray[1];
                y = dateArray[2];

                Date date = new Date(d, m, y);

                return date;
            }

        }


        public class Date
        {

            /// Количество дней (от 0 до 30)
            private int day;

            /// Количество месяцев (от 0 до 11)
            private int month;

            /// Количество лет (не меньше 0)
            private int year;

            /// Массив, в котором хранится кол-во дней в каждом месяце
            // readonly - значение поля нельзя менять в методах класса 
            private static readonly int[] months = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            /// Конструктор без параметров
            public Date()
            {
                day = 0;
                month = 0;
                year = 0;
            }

            /// Конструктор с параметрами
            public Date(int d, int m, int y)
            {
                SetYear(y);
                SetMonth(m);
                SetDay(d);
            }

            /// Деструктор
            ~Date() { }

            /// Задать день 
            public void SetDay(int d)
            {
                // проверка предусловия
                if ((d < 0) || (d > 30)) throw new ArgumentException("Invalid argument: day");
                else day = d;
            }

            /// Задать месяц
            public void SetMonth(int m)
            {
                // проверка предусловия
                if ((m < 0) || (m > 11)) throw new ArgumentException("Invalid argument: month");

                if (day < months[m])
                {
                    month = m;
                }
                else // если в поле day дней больше, чем в текущем месяце
                {
                    month = m + 1;
                    day -= months[m];
                }
            }

            /// Задать год
            public void SetYear(int y)
            {
                // проверка предусловия
                if (y < 0) throw new ArgumentException("Invalid argument: year");
                year = y;
            }

            /// Перевод даты в дни 
            public int ConvertDateToDays()
            {
                int days = 0;
                for (int i = 0; i < month; i++)
                {
                    days += months[i];
                }
                days += day + year * 365;
                return days;
            }

            /// Преобразовать дни в дату 
            public void ConvertDaysToDate(int d)
            {
                int y = d / 365;
                SetYear(y);
                int x = d % 365;
                int m = 0;
                for (int i = 0; i < 12; i++)
                {
                    if (x < months[i])
                    {
                        SetMonth(m);
                        SetDay(x);
                        break;
                    }
                    x -= months[i];
                    m++;
                }
            }

            /// Задать дату
            public void SetDate(int d, int m, int y)
            {
                SetDay(d);
                SetMonth(m);
                SetYear(y);
            }

            /// Получить дни
            public int GetDay() { return day; }

            /// Получить месяцы
            public int GetMonth() { return month; }

            /// Получить годы
            public int GetYear() { return year; }

            /// Получить день недели
            public string Day_Of_Week(Date date)
            {                           
                int weekDayNum = 4; //Wednesday

                Date defaultDate = new Date(03, 05, 2023);

                int day1 = date.ConvertDateToDays();

                int day2 = defaultDate.ConvertDateToDays(); //Wednesday 4

                if (day2 < day1)
                {
                    for (int i = day2; i <= day1; i++)
                    {
                        weekDayNum++;

                        if (weekDayNum == 7) weekDayNum = 0;
                    }
                } else
                {
                    for (int i = day2; i > day1; i--)
                    {                    
                        weekDayNum--;

                        if (weekDayNum == -1) weekDayNum = 6;
                    }
                }

                string weekDay = Enum.GetName(typeof(WeekDays), weekDayNum);

                return weekDay;
            }
            enum WeekDays{ Saturday, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday };

            /// Сложить даты
            public int SumDates(Date _d1,Date _d2)
            {
                int d1 = _d1.ConvertDateToDays();
                int d2 = _d2.ConvertDateToDays();
                d2 += d1;
                this.ConvertDaysToDate(d2);

                return d2;
            }
            public void AddDate(Date _d)
            {
                int d1 = this.ConvertDateToDays();
                int d2 = _d.ConvertDateToDays();
                d2 += d1;
                this.ConvertDaysToDate(d2);
            }

            /// Вычесть из даты 1 дату 2
            public int DiffDate(Date _d1, Date _d2)
            {
                int d1 = _d1.ConvertDateToDays();
                int d2 = _d2.ConvertDateToDays();
                if (d1 >= d2) 
                {
                    d1 -= d2;
                    //this.ConvertDaysToDate(d1);
                    return d1;
                }
                else
                    d2 -= d1;
                //this.ConvertDaysToDate(d2);
                return d2;

            }
            public void SubDate(Date d)
            {
                int d1 = this.ConvertDateToDays();
                int d2 = d.ConvertDateToDays();
                if (d1 < d2) throw new ArgumentException("The first date must be bigger than the second");
                d1 -= d2;
                this.ConvertDaysToDate(d1);
            }

            /// Добавить дни
            public void AddDays(int d)
            {
                int d1 = this.ConvertDateToDays();

                if (d >= 0) 
                {                    
                    d1 += d;

                    this.ConvertDaysToDate(d1);
                }
                else
                {
                    d *= (-1);

                    d1 += d;

                    this.ConvertDaysToDate(d1);
                }
            }

            /// Вычесть дни
            public void SubDay(int d)
            {
                if (d < 0) throw new ArgumentException("Invalid argument: seconds");
                int d1 = this.ConvertDateToDays();
                d1 -= d;

                this.ConvertDaysToDate(d1);
            }

            /// Добавить месяцы
            public void AddMonth(int m)
            {
                if (m < 0) throw new ArgumentException("Invalid argument: month");

                if (m + month >= 12)
                {
                    year += +m / 12;
                    SetDate(day, month + m - 12, year + 1);

                }
                else
                    SetDate(day, month + m, year);
            }

            /// Вычесть месяцы
            public void SubMonth(int m)
            {
                if (m < 0) throw new ArgumentException("Invalid argument: month");

                if (month - m <= 0)
                {
                    year -= m / 12;
                    SetDate(day, month - m + 12, year - 1);

                }
                else
                    SetDate(day, month - m, year);
            }

            /// Добавить годы
            public void AddYear(int y)
            {
                if (y < 0) throw new ArgumentException("Invalid argument: month");
                SetDate(day, month, year + y);
            }

            /// Вычесть годы
            public void SubYear(int y)
            {
                if (y < 0) throw new ArgumentException("Invalid argument: month");
                if (year - y < 0) throw new ArgumentException("Invalid argument: year < 0");
                SetDate(day, month, year - y);
            }

            override public string ToString()
            {
                return Convert.ToString(day) + " days " + Convert.ToString(month) + " months " + Convert.ToString(year) + " years";
            }
        }
    }
}
