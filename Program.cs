using System;
using SimpleClassLibrary;

namespace Entrant
{
    class Program
    {
        static void Main(string[] args)
        {

            Entrants[] entrants = ReadEntrantArray();
            
            bool checkExitMenu = false;
            while (!checkExitMenu)
            {
                Console.Write("\n\n1.Переглянути масив\n" +
                    "2.Сортувати за спаданням конкурсного балу студентів\n" +
                    "3.Сортувати за прізвищем\n" +
                    "4.Отримати дані про найвищий та найнижчий конкурсний бал студентів\n" +
                    "5.Вихід\n" +
                    "Ваш вибір: ");
                try
                {
                    int n = Int32.Parse(Console.ReadLine());
                    switch (n)
                    {
                        case 1:
                            Console.WriteLine("\n");
                            PrintEntrants(entrants);
                            break;
                        case 2:
                            Console.WriteLine("\nМасив відсортовано");
                            SortEntrantsByPoints(ref entrants);
                            break;
                        case 3:
                            Console.WriteLine("\nМасив відсортовано"); 
                            SortEntrantsByName(ref entrants);
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
        static Entrants[] ReadEntrantArray()
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

            Entrants[] arrEntrant = new Entrants[n];
            Console.WriteLine("Введіть дані про студентів");
            for (int i = 0; i <= n-1; i++)
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
                int number = 0;
                while (!loopExit)
                {
                    try
                    {
                        number = Int32.Parse(Console.ReadLine());
                        if(number > 0)
                            loopExit = true;
                        else Console.WriteLine("Введіть число більше нуля!");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Введено некоректне значення! Повторіть спробу.");
                    }
                }
                zno = new ZNO[number];
                string Subject;
                int Points = 0;
                for (int j = 0; j < number; j++)
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
                    zno[j] = new ZNO(Subject, Points);
                }
                arrEntrant[i] = new Entrants(Name, CoursePoints, AvgPoints, zno);
            }
            return arrEntrant;
        }
        static void PrintEntrant(Entrants entrant)
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
        static void PrintEntrants(Entrants[] entrants)
        {
            foreach (Entrants entrant in entrants)
            {
                PrintEntrant(entrant);
            }
        }
        static void GetEntrantsInfo(Entrants[] entrants, out double maxBal, out double minBal)
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
        static void SortEntrantsByPoints(ref Entrants[] entrants)
        {
            Array.Sort(entrants, SortInfoByPoints);
        }
        static int SortInfoByPoints(Entrants a, Entrants b)
        {
            if (a.GetCompMark() > b.GetCompMark())
                return 1;
            if (a.GetCompMark() < b.GetCompMark())
                return -1;
            return 0;
        }
        static void SortEntrantsByName(ref Entrants[] entrants)
        {
            Array.Sort(entrants, SortInfoByName);
        }
        static int SortInfoByName(Entrants a, Entrants b)
        {
            var part1 = a.Name;
            var part2 = b.Name;
            var compareResult = part1.CompareTo(part2);
            if (compareResult == 0)
                return b.GetCompMark().CompareTo(b.GetCompMark());
            return compareResult;
        }

    }
 
}
