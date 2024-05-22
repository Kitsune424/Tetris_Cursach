using System.Collections;
using System.Xml.Linq;

namespace _08_bidirectional_linear_list
{
    public class LinkList<T> : IEnumerable<T>   // линейный список
    {
        #region Звено списка
        private class Node<T>
        {
            public T Data { get; set; }             // хранение значения
            public Node<T>? Next { get; set; }      // храненит следующий элемент списка
            public Node<T>? Previous { get; set; }  // хранение прошлого элемента списка

            public Node(T data)
            {
                Data = data;
                Next = null;
                Previous = null;
            }
        }
        #endregion

        #region Линейный список
        Node<T>? head;   // головной/первывй элемент
        Node<T> tail;   // последний/хвостовой элемент

        /// <summary>
        /// Очистка списка
        /// </summary>
        public void Clear()
        {
            head = null;
        }

        #region Ввод/вывод стандартный
        /// <summary>
        /// добавляет элемент в начало списка
        /// </summary>
        /// <param name="value">значние</param>
        public void AddFirst(T value)
        {
            Node<T> node = new Node<T>(value); 

            if (head == null)
            {
                head = node;
                tail = head;
            }
            else
            {
                node.Next = head;
                head.Previous = node;
                head = node;
            }
        }

        /// <summary>
        /// добавляет элемент в конец списка
        /// </summary>
        /// <param name="value">значение</param>
        public void AddLast(T value)
        {
            Node<T> node = new Node<T>(value);

            if (head == null)   // если список пуст
            {
                head = node;
                tail = head;
            }
            else                // если уже есть элементы
            {
                tail.Next = node;
                node.Previous = tail; 
                tail = tail.Next;
            }
        }

        /// <summary>
        /// Удаляет элемент из начал списка
        /// </summary>
        public void RemoveFirst()
        {
            if (head != null)
            {
                head = head.Next;
                if (head != null)
                    head.Previous = null;
                else
                    tail = null;
            }
        }

        /// <summary>
        /// Удаляет элемент из конца
        /// </summary>
        public void RemoveLast()
        {
            if (tail != null)
            {
                tail = tail.Previous;
                if (tail != null)
                    tail.Next = null;
                else
                    head = null;
            }
        }

        /// <summary>
        /// Вывод списка в консоль
        /// </summary>
        public void Print()
        {
            Node<T> current = head;

            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }

        public void ReversePrint()
        {
            Node<T> current = tail;

            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Previous;
            }
            Console.WriteLine();
        }
        #endregion

        #region Параметры
        /// <summary>
        /// Возвращение bool по результату есть ли элемент с значением
        /// </summary>
        /// <param name="value">искомое значение</param>
        /// <returns></returns>
        public bool Contains(T value)
        {
            Node<T> current = head;

            while (current != null)
            {
                if (current.Data.Equals(value)) // == не работает тут на объекты, беру Equals
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        /// <summary>
        /// Подсчет кол-ва элементов
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            int cnt = 0;
            Node<T> current = head; // звено отсчета

            while (current != null)
            {
                cnt++;
                current = current.Next;
            }
            return cnt;
        }

        /// <summary>
        /// Возвращает значение элемента конкретного номера
        /// </summary>
        /// <returns></returns>
        public T IndexValue(int index)
        {
            Node<T> current = head;
            int curIndex = 0;

            while (current != null)
            {
                if (curIndex == index)
                {
                    return current.Data;
                }

                current = current.Next;
                curIndex++;
            }

            throw new IndexOutOfRangeException("Индекс вышел за пределы доступных");
        }
        #endregion

        #region Ввод/вывод индексно ориентированный
        /// <summary>
        /// Добавление нового элемента перед index элементом
        /// </summary>
        /// <param name="index">номер/индекс элемента</param>
        /// <param name="value">значение</param>
        public void AddBefore(int index, T value)
        {
            if (index < 0 || index > Count())
                throw new IndexOutOfRangeException($"Выход за пределы значений");

            if (index == 0) // добавление перед первым элементом = добавление в начало
            {
                AddFirst(value);
                return;
            }

            Node<T> current = head; // звено отсчета
            for (int i = 0; i < index - 1; i++) // идем до нужного индекса, откатываем назад
            {
                current = current.Next;
            }
            Node<T> newElement = new Node<T>(value);
            newElement.Next = current.Next;
            current.Next = newElement;
        }

        /// <summary>
        /// добавление нового элемента после index элемента
        /// </summary>
        /// <param name="index">номер/индекс элемента</param>
        /// <param name="value">значние</param>
        public void AddAfter(int index, T value)
        {
            if (index < 0 || index >= Count())
                throw new IndexOutOfRangeException($"Выход за пределы значений");

            if (index == Count() - 1) // добавление после последнего = добавить в конец
            {
                AddLast(value);
                return;
            }

            Node<T> current = head; // звено отсчета
            for (int i = 0; i < index; i++) // шагаем ддо нужного индекса без отката шага назад
            {
                current = current.Next;
            }
            Node<T> newElement = new Node<T>(value);
            newElement.Next = current.Next;
            current.Next = newElement;
        }

        /// <summary>
        /// Удаление элемента перед index элементом
        /// </summary>
        /// <param name="index"></param>
        public void RemoveBefore(int index)
        {
            if (index <= 0 || head == null)
                return;

            Node<T> current = head;
            for (int i = 0; i < index - 1 && current != null; i++)
            {
                current = current.Next;
            }

            if (current == null || current.Previous == null)
            {
                return;
            }

            Node<T> nodeToRemove = current.Previous;
            current.Previous = nodeToRemove.Previous;
            if (nodeToRemove.Previous != null)
            {
                nodeToRemove.Previous.Next = current;
            }
            else
            {
                head = current;
            }
            nodeToRemove = null;
        }

        /// <summary>
        /// Удаление элемента после index элементом
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAfter(int index)
        {
            if (index < 0 || head == null)
                return;
            

            Node<T> current = head;
            for (int i = 0; i < index && current != null; i++)
            {
                current = current.Next;
            }

            if (current == null || current.Next == null)
            {
                return;
            }

            Node<T> nodeToRemove = current.Next;
            current.Next = nodeToRemove.Next;
            if (nodeToRemove.Next != null)
            {
                nodeToRemove.Next.Previous = current;
            }
            else
            {
                tail = current;
            }
            nodeToRemove = null;
        }
        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();

            Node<T> current = head;

            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
