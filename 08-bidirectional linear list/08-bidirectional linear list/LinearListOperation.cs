using System;

namespace _08_bidirectional_linear_list
{
    internal class Operation
    {
        #region Input
        /// <summary>
        /// Заполнение списка
        /// </summary>
        /// <param name="N"></param>
        public static LinkList<int> Fill(int N)
        {
            LinkList<int> tmp = new LinkList<int>();

            Random rnd = new Random();
            for (int i = 0; i < N; i++)
                tmp.AddFirst(rnd.Next(0, 10));
            return tmp;
        }
        #endregion

        #region Task 11
        public static void FromTailToHead(LinkList<int> list)
        {
            int size = list.Count();
            int value = list.IndexValue(size-1);

            list.AddFirst(value);
            list.RemoveLast();
        }
        #endregion

        #region Task 23
        public static int MaxElement(LinkList<int> list)
        {
            int listCnt = list.Count();
            int max = 0;

            for (int i = 0; i < listCnt; i++)
            {
                if (max < list.IndexValue(i))
                {
                    max = list.IndexValue(i);
                }
            }

            return max;
        }
        #endregion 
    }
}
