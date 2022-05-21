using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            int menu = 1;
            List<TestDepartament> ListDepartament = new List<TestDepartament>();
            ListDepartament = TestDepartament.ReadFile(@"указываем путь к файлу\TestTaskDepartament.csv");

            List<TestEmployee> ListEmployee = new List<TestEmployee>();
            ListEmployee = TestEmployee.ReadFile(@"указываем путь к файлу\TestTaskEmployee.csv");

            do
            {
                Console.Clear();
                menu = MainMenu(ListEmployee, ListDepartament);
            }
            while (menu != 1);
        }

        /// <summary>
        /// Главное меню
        /// </summary>
        public static int MainMenu(List<TestEmployee> ListEmployee, List<TestDepartament> ListDepartament)
        {
            int task = 0;
            Console.WriteLine("Введите номер задачи:" +
               "\n1 - Посчитать суммарную зарплату в разрезе департаментов." +
               "\n2 - Департамент, в котором у сотрудника зарплата максимальна" +
               "\n3 - Зарплаты руководителей департаментов (по убыванию)");
            while (true) 
            { 
                try
                    {
                        task = Convert.ToInt32(Console.ReadLine());
                    break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Проверьте правильность ввода!");
                    } 
            }
            
            switch (task)
            {
                case 1:
                    Task1(ListEmployee);
                    break;

                case 2:
                    Task2(ListEmployee, ListDepartament);
                    break;

                case 3:
                    Task3(ListEmployee);
                    break;
            }
            Console.WriteLine("1 - Выйти из программы" +
                "\n2 - Выйти в главное меню");
            int menu = 0;
            while (true)
            {
                try
                {
                    menu = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Проверьте правильность ввода!");
                }
            }
            return menu;
        }

        /// <summary>
        /// Выполнение первой задачи
        /// </summary>
        public static void Task1(List<TestEmployee> ListEmployee)
        {
            int taskRuc = 0;
            Console.Clear();
            Console.WriteLine("Посчитать: " +
                        "\n1 - С руководителями" +
                        "\n2 - Без руководителей");
            while (true)
            {
                try
                {
                    taskRuc = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Проверьте правильность ввода!");
                }
            }
            int summ = 0;
            for (int i = 0; i <= ListEmployee.Count - 1; i++)
            {
                switch (taskRuc)
                {
                    case 1:
                        if (ListEmployee[i].C_Cheif_id != "") summ = summ + ListEmployee[i].E_Salary;
                    break;
                    case 2:
                        if (ListEmployee[i].C_Cheif_id == "") summ = summ + ListEmployee[i].E_Salary;
                    break;
                }
            }
            Console.Clear();
            Console.WriteLine("Сумма: " + summ + "");
        }
        /// <summary>
        /// Выполнение второй задачи
        /// </summary>
        public static void Task2(List<TestEmployee> ListEmployee, List<TestDepartament> ListDepartament)
        {
            Console.Clear();
            int summ = 0;
            int departament_id = 0;
            string departament_name = "";
            for (int i = 0; i <= ListEmployee.Count - 1; i++)
            {
                if (ListEmployee[i].E_Salary > summ)
                {
                    summ = ListEmployee[i].E_Salary;
                    departament_id = ListEmployee[i].B_Departament_id;
                }
            }
            for (int i = 0; i <= ListDepartament.Count - 1; i++)
            {
                if (ListDepartament[i].A_ID == departament_id) departament_name = ListDepartament[i].B_Departament;
            }
            Console.WriteLine("Департамент: " + departament_name + ", ЗП:"+ summ + "");
        }
        /// <summary>
        /// Выполнение третьей задачи
        /// </summary>
        public static void Task3(List<TestEmployee> ListEmployee)
        {
            Console.Clear();
            int summ = 0;
            List<int> salary = new List<int>();
            for (int i = 0; i <= ListEmployee.Count - 1; i++)
            {
                 if (ListEmployee[i].F_Cheif_Or_No == 1) salary.Add(ListEmployee[i].E_Salary);
                
            }
            var selectedList = salary.OrderByDescending(u => u);
            foreach(int lst in selectedList)
            Console.WriteLine(""+ lst + "");   
        }
        /// <summary>
        /// Данные по департаментам
        /// </summary>
        public class TestDepartament
        {
            public int A_ID { get; set; }
            public string B_Departament { get; set; }

            public void piece(string line)
            {
                string[] parts = line.Split(';');  //Разделитель в CVS файле.
                A_ID = Convert.ToInt32(parts[0]);
                B_Departament = parts[1];

            }
            public static List<TestDepartament> ReadFile(string filename)
            {
                List<TestDepartament> res = new List<TestDepartament>();
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        TestDepartament p = new TestDepartament();
                        p.piece(line);
                        res.Add(p);
                    }
                }

                return res;
            }
        }
        /// <summary>
        /// Данные по работникам
        /// </summary>
        public class TestEmployee
        {
            public int A_ID { get; set; }
            public int B_Departament_id { get; set; }
            public string C_Cheif_id { get; set; }
            public string D_Name { get; set; }
            public int E_Salary { get; set; }
            public int F_Cheif_Or_No { get; set; }

            public void Str(string line)
            {
                string[] parts = line.Split(';');

                A_ID = Convert.ToInt32(parts[0]);
                B_Departament_id = Convert.ToInt32(parts[1]);
                C_Cheif_id = parts[2];
                D_Name = parts[3];
                E_Salary = Convert.ToInt32(parts[4]);
                F_Cheif_Or_No = Convert.ToInt32(parts[5]);

            }
            public static List<TestEmployee> ReadFile(string filename)
            {
                List<TestEmployee> res = new List<TestEmployee>();
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        TestEmployee p = new TestEmployee();
                        p.Str(line);
                        res.Add(p);

                    }
                }

                return res;
            }
        }
    }
}
