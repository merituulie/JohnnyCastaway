using System;
using System.Text;

namespace MonoGame.Graph
{
    class PriorityQueue<T> where T : System.IComparable<T>
    {
        public static int DEFAULT_CAPACITY = 100;
        public int size;   // Number of elements in heap
        public T[] array;  // The heap array

        public PriorityQueue()
        {
            size = 0;
            array = new T[DEFAULT_CAPACITY];
        }

        public void Add(T x)
        {
            if (size + 1 == array.Length)
                DoubleArray();

            // Percolate up 
            int hole = ++size;
            array[0] = x;

            while (x.CompareTo(array[hole / 2]) < 0)
            {
                array[hole] = array[hole / 2];
                hole /= 2;
            }

            array[hole] = x;
        }

        public T Remove()
        {
            if (size == 0)
                throw new Exception();

            T minItem = array[1];
            array[1] = array[size--];
            PercolateDown(1);

            return minItem;
        }

        public void Clear() => size = 0;

        public int Size() => size;

        public void AddFreely(T x)
        {
            if (size + 1 == array.Length)
                DoubleArray();

            array[++size] = x;
        }

        public void BuildHeap()
        {
            for (int i = size / 2; i > 0; i--)
                PercolateDown(i);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < size + 1; i++)
            {
                sb.Append($"{array[i]}");

                if (i != size)
                    sb.Append(" ");
            }
            return sb.ToString();
        }

        // Double the size of the array
        private void DoubleArray()
        {
            T[] tmpArray = new T[array.Length * 2];

            for (int i = 0; i < array.Length; i++)
                tmpArray[i] = array[i];

            array = tmpArray;
        }

        // Internal method to percolate down in the heap
        private void PercolateDown(int hole)
        {
            int child;
            T tmp = array[hole];

            for (; hole * 2 <= size; hole = child)
            {
                child = hole * 2;

                if (child != size && array[child + 1].CompareTo(array[child]) < 0)
                    child++;

                if (array[child].CompareTo(tmp) < 0)
                    array[hole] = array[child];
                else
                    break;
            }

            array[hole] = tmp;
        }
    }
}
