using System;

namespace Entrant
{
    class Program
    {
        static void Main(string[] args)
        {

            Entrant[] entrants = ReadEntrantArray();
            Console.WriteLine("\n\n1.Переглянути масив\n" +
                    "2.Сортувати за спаданням конкурсного балу студентів\n" +
                    "3.Сортувати за прізвищем\n" +
                    "4.Отримати дані про найвищий та найнижчий конкурсний бал студентів\n" +
                    "5.Вихід\n" +
                    "Ваш вибір: ");
            bool checkExitMenu = false;
            while (!checkExitMenu)
            {
                try
                {
                    int n = Int32.Parse(Console.ReadLine());
                    switch (n)
                    {
                        case 1:
                            PrintEntrants(entrants);
                            break;
                        case 2:
                            SortEntrantsByPoints(ref entrants);
                            break;
                        case 3:SortEntrantsByName(ref entrants);
                            break;
                        case 4:
                            GetEntrantsInfo(entrants, out double maxBal, out double minBal);
                            Console.WriteLine("\nНайбільший конкурсний бал студента - " + maxBal +
                                "\nНайменший конкурсний бал студента - " + minBal);
                            break;
                        case 5:
                            checkExitMenu = true;
                            break;
                        default:
                            Console.WriteLine("Некоректний ввід, повторіть спробу!");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Некоректний ввід, повторіть спробу!");
                }
            }
        }
        static Entrant[] ReadEntrantArray()
        {
            string Name;
            int CoursePoints = 0,
                AvgPoints = 0;
            ZNO[] zno;

            Console.Write("Введіть кількість студентів: ");
            bool loopExit = false;
            int n = 0;
            while(!loopExit)
            {
                try
                {
                    n = Int32.Parse(Console.ReadLine());
                    if(n>0) 
                        loopExit = true;
                    else Console.WriteLine("Введіть число більше нуля!");
                }
                catch (Exception)
                {
                    Console.WriteLine("Введено некоректне значення! Повторіть спробу.");
                    
                }
            }

            Entrant[] arrEntrant = new Entrant[n];
            Console.WriteLine("Введіть дані про студентів");
            for (int i = 0; i < n; i++)
            {
                Console.Write("Ім'я: ");
                Name = Console.ReadLine();
                Console.Write("Бали за курси: ");
                loopExit = false;
                while (!loopExit)
                {
                    try
                    {
                        CoursePoints = Int32.Parse(Console.ReadLine());
                        loopExit = true;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Введено некоректне значення! Повторіть спробу.");
                    }
                }
                Console.Write("Бал атестату: ");
                loopExit = false;
                while (!loopExit)
                {
                    try
                    {
                        AvgPoints = Int32.Parse(Console.ReadLine());
                        loopExit = true;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Введено некоректне значення! Повторіть спробу.");
                    }
                }
                Console.Write("Кількість ЗНО: ");
                loopExit = false;
                while (!loopExit)
                {
                    try
                    {
                        n = Int32.Parse(Console.ReadLine());
                        if(n>0)
                            loopExit = true;
                        else Console.WriteLine("Введіть число більше нуля!");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Введено некоректне значення! Повторіть спробу.");
                    }
                }
                zno = new ZNO[n];
                string Subject;
                int Points = 0;
                for (int j = 0; j < n; j++)
                {
                    
                    Console.Write("Предмет: ");
                    Subject = Console.ReadLine();
                    Console.Write("Бал: ");
                    loopExit = false;
                    while (!loopExit)
                    {
                        try
                        {
                            Points = Int32.Parse(Console.ReadLine());
                            loopExit = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Введено некоректне значення! Повторіть спробу.");
                        }
                    }
                    zno[i] = new ZNO(Subject, Points);
                }
                arrEntrant[i] = new Entrant(Name, CoursePoints, AvgPoints, zno);
            }
            return arrEntrant;
        }
        static void PrintEntrant(Entrant entrant)
        {
            Console.WriteLine($"ID: {entrant.IdNum}");
            Console.WriteLine($"Ім'я: {entrant.Name}");
            Console.WriteLine($"Бали за підготовчі курси: {entrant.CoursePoints}");
            Console.WriteLine($"Бал атестату: {entrant.AvgPoints}");
            Console.WriteLine("ЗНО");
            foreach (ZNO zno in entrant.ZNOResult)
            {
                Console.WriteLine($"{zno.Subject} : {zno.Points}");
            }

        }
        static void PrintEntrants(Entrant[] entrants)
        {
            foreach (Entrant entrant in entrants)
            {
                PrintEntrant(entrant);
            }
        }
        static void GetEntrantsInfo(Entrant[] entrants, out double maxBal, out double minBal)
        {
            maxBal = minBal = entrants[0].GetCompMark();
            foreach (var entrant in entrants)
            {
                if (entrant.GetCompMark() > maxBal)
                    maxBal = entrant.GetCompMark();
                if (entrant.GetCompMark() < minBal)
                    minBal = entrant.GetCompMark();
            }
        }
        static void SortEntrantsByPoints(ref Entrant[] entrants)
        {
            Array.Sort(entrants, SortInfoByPoints);
        }
        static int SortInfoByPoints(Entrant a, Entrant b)
        {
            if (a.GetCompMark() > b.GetCompMark())
                return 1;
            if (a.GetCompMark() < b.GetCompMark())
                return -1;
            return 0;
        }
        static void SortEntrantsByName(ref Entrant[] entrants)
        {
            Array.Sort(entrants, SortInfoByName);
        }
        static int SortInfoByName(Entrant a, Entrant b)
        {
            var part1 = a.Name;
            var part2 = b.Name;
            var compareResult = part1.CompareTo(part2);
            if (compareResult == 0)
                return b.GetCompMark().CompareTo(b.GetCompMark());
            return compareResult;
        }

    }
    class Entrant
    {
        private string _Name;
        private Guid _IdNum;
        private int _CoursePoints;
        private int _AvgPoints;
        private ZNO []_ZNOResult;
        public string Name { get { return _Name; } set { _Name = value; } }
        public Guid IdNum { get { return _IdNum; } set { _IdNum = value; } }
        public int CoursePoints { get { return _CoursePoints; } set { _CoursePoints = value; } }
        public int AvgPoints { get { return _AvgPoints; } set { _AvgPoints = value; } }
        public ZNO []ZNOResult { get { return _ZNOResult; } set { _ZNOResult = value; } }
        public Entrant(string Name, int CoursePoints, int AvgPoints, ZNO []ZNOResult)
        {
            _Name = Name;
            _IdNum = new Guid();
            _CoursePoints = CoursePoints;
            _AvgPoints = AvgPoints;
            _ZNOResult = new ZNO[ZNOResult.Length];
            for (int i = 0; i < ZNOResult.Length; i++)
            {
                _ZNOResult[i] = new ZNO(ZNOResult[i]);
            }
        }
        public double GetCompMark()
        {
            if(_ZNOResult.Length > 2)
            {
                double bal;
                bal = CoursePoints * 0.05 + AvgPoints * 0.1;

                bal += ZNOResult[0].Points * 0.25;
                bal += ZNOResult[1].Points * 0.4;
                bal += ZNOResult[2].Points * 0.2;
                return bal;

            }
            else return 0;
        }
        public string GetBestSubject()
        {
            if (_ZNOResult.Length > 1)
            {
                int SubjectValue = _ZNOResult[0].Points;
                string SubjectName = _ZNOResult[0].Subject;
                for (int i = 1; i < _ZNOResult.Length; i++)
                {
                    if(_ZNOResult[i].Points > SubjectValue)
                    {
                        SubjectName = _ZNOResult[i].Subject;
                        SubjectValue = _ZNOResult[i].Points;
                    }
                }
                return SubjectName;
            }
            else return _ZNOResult[0].Subject;
        }
        public string GetWorstSubject()
        {
            if (_ZNOResult.Length > 1)
            {
                int SubjectValue = _ZNOResult[0].Points;
                string SubjectName = _ZNOResult[0].Subject;
                for (int i = 1; i < _ZNOResult.Length; i++)
                {
                    if (_ZNOResult[i].Points < SubjectValue)
                    {
                        SubjectName = _ZNOResult[i].Subject;
                        SubjectValue = _ZNOResult[i].Points;
                    }
                }
                return SubjectName;
            }
            else return _ZNOResult[0].Subject;
        }

    }
    class ZNO
    {
        private string _Subject;
        private int _Points;
        public string Subject { get { return _Subject; } set { _Subject = value; } }
        public int Points { get { return _Points; } set { _Points = value; } }
        public ZNO(string Subject, int Points)
        {
            _Subject = Subject;
            _Points = Points;
        }
        public ZNO(ZNO znoResult)
        {
            _Subject = znoResult.Subject;
            _Points = znoResult.Points;
        }
    }
}
