using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager
{
    class Manager
    {

        //метод ввода и проверки порядковых номеров от 1 до maxValue
        private static int EnterNumber(int maxValue)
        {
            int index;
            bool indexTry;
            string pos;
            do
            {
                Console.WriteLine("Enter int number between {0} and {1}", 1, maxValue);
                pos = Console.ReadLine();
                indexTry = Int32.TryParse(pos, out index);
            }
            while (indexTry == false || index < 0 || index > maxValue);
            return index;

        }
        //метод ввода и проверки корректной даты
        private static DateTime EnterDate()
        {
            DateTime dateD = DateTime.MinValue;
            bool dayError;
            do
            {
                Console.WriteLine("Enter the real date in format dd.mm.yyyy or 'cancel'");
                string day = Console.ReadLine();
                if (day == "cancel") break;
                else dayError = DateTime.TryParse(day, out dateD);
            }
            while (dayError == false);
            //Console.WriteLine(Convert.ToString(dateD));
            return dateD;
        }
        //Метод добавления нового элемента
        public static void Add(List<Plans> tasks)
        {
            Console.WriteLine("Create Task. Enter the task");
            string taskString = Console.ReadLine();
            DateTime dateD = EnterDate();

            //создаем задачу на основе введенных данных

            Plans task = new Plans() { task = taskString, date = dateD };

            //вставляем задачу в нужное место в отсортированную коллекцию
            //добавление элемента по индексу в заранее отстортированный список будет происходить быстрее, чем добавление в конец, а потом сортировка       
           
            if (dateD < tasks[tasks.Count-1].date)
            {
                foreach (var item in tasks)
                {
                    if (dateD < item.date)
                    {
                        tasks.Insert(tasks.IndexOf(item), task);
                        Console.WriteLine("New Task is created. Position {0}", tasks.IndexOf(item));
                        break;
                    }
                }
            }
            else tasks.Add(task);
            Console.WriteLine("New Task is created. Last position");
        }
        //метод выводит на экран список задач
        public static void ShowTasks(List<Plans> plans)
        {
            //DateTime today = DateTime.Today;
            int pos;
            foreach (var item in plans)
            {
                string status = item.Status();
                pos = plans.IndexOf(item) + 1;
                Console.WriteLine("{0}-{1}-{2}---{3}", pos, item.date.ToShortDateString(), status, item.task);
            }
            Console.WriteLine(new string('-', 45));
        }

        //сортировака коллекции
        public static void SortPlans(List<Plans> plans)
        {
            plans.Sort(new Plans().CompareDates);
        }
        //удаление задачи
        public static void DeleteTask(List<Plans> plans)
        {
            Console.WriteLine("Delete Task");
            int length = plans.Count;
            int index = EnterNumber(length);
            plans.RemoveAt(index - 1);
            Console.WriteLine("Task {0} Deleted", index);
        }
        //Изменение задачи
        public static void ChangeTask(List<Plans> plans)
        {
            Console.WriteLine("Change Task");
            int length = plans.Count;
            int index = EnterNumber(length) - 1;
            Console.WriteLine("Change Task. For choosing enter '1' for date, '2 for task or '3' for status");
            Console.WriteLine("1-date---{0}", plans[index].date.ToShortDateString());
            Console.WriteLine("2-task---{0}", plans[index].task);
            string status = plans[index].Status();
            Console.WriteLine("3-status-{0}", status);
            int pos = EnterNumber(3);
            string change;
            switch (pos)
            {
                case 1:
                    var temp = plans[index].date;
                    plans[index].date = EnterDate();

                    if (plans[index].date != temp)
                    {
                        SortPlans(plans);
                    }
                    Console.WriteLine(plans[index].date);
                    break;
                case 2:
                    Console.WriteLine("Enter new Task");
                    string taskString = Console.ReadLine();
                    plans[index].task = taskString;
                    break;

                case 3:
                    Console.WriteLine("Enter 'done' if the Task is finished, other answer means 'have not done'");
                    change = Console.ReadLine();
                    plans[index].done = (change == "done") ? true : false;
                    break;
            }
            Console.WriteLine("Changes have done");


        }
    }

}

