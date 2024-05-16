using System;

namespace _08_bidirectional_linear_list
{
    class Program
    {
        private static void Main(string[] args)
        {
            LinkList<int> linklist = new LinkList<int>();
            ConsoleKeyInfo K;
            do
            {
                #region  Interface
                Console.Clear(); //очистка экрана перед выводом меню
                Console.WriteLine("-----Задания по классу-----");
                Console.WriteLine("1.Создать линейный список");
                Console.WriteLine("2.Задание 11");
                Console.WriteLine("3.Задание 27");
                Console.WriteLine("4.Вывести список");
                Console.WriteLine("Esc. Выход из программы");
                Console.WriteLine("-----Тест методов класса-----");
                Console.WriteLine("5.Добавить элемент в начало и конец списка");
                Console.WriteLine("6.Поиск элемента по значению");
                Console.WriteLine("7.Поиск элемента по индексу");
                Console.WriteLine("8.Добавление элемента перед заданным и после заданного");
                Console.WriteLine("9.Удаление элементов в начале и в конце списка");
                Console.WriteLine("0.Удаление элементов вокруг заданного");
                #endregion
                K = Console.ReadKey(); //считывание кода вводимой клавиши
                switch (K.Key)
                {
                    #region Tasks
                    // создание списка
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:// если нажата клавиша с цифрой 1
                        {
                            if (linklist.Count() == 0)
                            {
                                Console.WriteLine("\nЗадайте кол-во элементов списка");
                                int n = 0;
                                bool n_t = int.TryParse(Console.ReadLine(), out n);

                                if (n_t)
                                {
                                    if (n <= 0)
                                        Console.WriteLine("список не может быть нулевым или отрецательным");
                                    else
                                        linklist = Operation.Fill(n);
                                }
                                else
                                    Console.WriteLine("Недопустимое значение");
                            }
                            else
                            {
                                linklist.Clear();
                                Console.WriteLine("\nПрошлый список был очищен"
                                    + "\nЗадайте кол-во элементов списка");
                                int n = 0;
                                bool n_t = int.TryParse(Console.ReadLine(), out n);

                                if (n_t)
                                {
                                    if (n <= 0)
                                        Console.WriteLine("Список не может быть нулевым или отрецательным");
                                    else
                                        linklist = Operation.Fill(n);
                                }
                                else
                                    Console.WriteLine("Недопустимое значение");
                            }
                            break;
                        }

                    // задание 1
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:// если нажата клавиша с цифрой 2
                        {
                            Console.WriteLine("\nЭлемент с конца добавляем в начало");
                            linklist.Print();
                            Operation.FromTailToHead(linklist);
                            linklist.Print();
                            break;
                        }

                    // задание 2
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:// если нажата клавиша с цифрой 3
                        {
                            if (linklist.Count() != 0)
                            {
                                int max = Operation.MaxElement(linklist);
                                Console.WriteLine($"\nМаксимальный элемент списка: {max}");
                            }
                            else
                                Console.WriteLine("\nСписок пуст");

                            break;
                        }

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:// если нажата клавиша с цифрой 4
                        {
                            Console.WriteLine("");
                            Console.WriteLine("В прямом порядке:");
                            linklist.Print();
                            Console.WriteLine("");
                            Console.WriteLine("В обратном порядке:");
                            linklist.ReversePrint();
                            break;
                        }
                    #endregion

                    #region Class element
                    case ConsoleKey.D5:  // Добавить в конец, добавить в начало
                    case ConsoleKey.NumPad5:
                        {
                            Console.WriteLine("\nДобавление 1000 в начало и конец списка");
                            linklist.AddLast(1000);
                            linklist.AddFirst(1000);
                            break;
                        }

                    case ConsoleKey.D6: // поиск по значению
                    case ConsoleKey.NumPad6:
                        {
                            Console.WriteLine("\nВведите число, которое хотите найти в списке");
                            int n = 0;
                            bool n_t = int.TryParse(Console.ReadLine(), out n);

                            if (n_t)
                            {
                                if (linklist.Contains(n))
                                {
                                    Console.WriteLine($"число найдено");
                                    linklist.Print();
                                }
                                else
                                {
                                    Console.WriteLine($"число не найдено");
                                    linklist.Print();
                                }
                            }
                            else
                                Console.WriteLine("Недопустимое значение");

                            break;
                        }

                    case ConsoleKey.D7: // поиск по индекску
                    case ConsoleKey.NumPad7:
                        {
                            Console.WriteLine("\nВведите номер элемента, чтобы получить его значение");
                            int n = 0;
                            bool n_t = int.TryParse(Console.ReadLine(), out n);

                            if (n_t)
                            {
                                if (n >= 0 && n <= linklist.Count())
                                {
                                    Console.WriteLine($"Элемент на позиции {n} имеет значение {linklist.IndexValue(n)}");
                                }
                                else
                                    throw new IndexOutOfRangeException();
                            }
                            else
                                Console.WriteLine("Недопустимое значение");
                            break;
                        }

                    case ConsoleKey.D8: // добавить перед заданным добавить после заданного
                    case ConsoleKey.NumPad8:
                        {
                            Console.WriteLine("\nЗадайте индекс элемента перед которым и после которого будут добавлены 999");
                            int n = 0;
                            bool n_t = int.TryParse(Console.ReadLine(), out n);

                            if (n_t)
                            {
                                if (n >= 0 && n <= linklist.Count())
                                {
                                    linklist.AddBefore(n, 999);
                                    linklist.AddAfter(n + 1, 999);
                                    linklist.Print();
                                }
                                else
                                    throw new IndexOutOfRangeException();
                            }
                            else
                                Console.WriteLine("Недопустимое значение");
                            break;
                        }

                    case ConsoleKey.D9: // удаляет элемент из начала списка и с его конца
                    case ConsoleKey.NumPad9:
                        {
                            linklist.Print();
                            linklist.RemoveFirst();
                            linklist.RemoveLast();
                            linklist.Print();
                            break;
                        }

                    case ConsoleKey.D0: // Удаляет элемент перед заданным числом и после него
                    case ConsoleKey.NumPad0:
                        {
                            Console.WriteLine("\nЗадайте индекс элемента вокург которого будут удалены элементы");
                            int n = 0;
                            bool n_t = int.TryParse(Console.ReadLine(), out n);

                            if (n_t)
                            {
                                if (n >= 0 && n <= linklist.Count())
                                {
                                    linklist.Print();
                                    linklist.RemoveBefore(n);
                                    linklist.Print();
                                    System.Threading.Thread.Sleep(1000);
                                    linklist.RemoveAfter(n);
                                    linklist.Print();
                                }
                                else
                                    throw new IndexOutOfRangeException();
                            }
                            else
                                Console.WriteLine("Недопустимое значение");
                            break;
                        }
                    #endregion

                    default: break;
                }
                // приостанавливаем выполнение текущего потока на заданное число времени. Время измеряется в миллисекундах
                System.Threading.Thread.Sleep(3000); // 3сек.
            }
            while (K.Key != ConsoleKey.Escape);// цикл заканчивается, если нажата клавиша Esc
        }
    }
}