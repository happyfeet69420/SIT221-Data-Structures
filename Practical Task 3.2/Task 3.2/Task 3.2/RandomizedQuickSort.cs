using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    class RandomizedQuickSort : ISorter
    {
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;
            QuickSort(sequence, comparer, 0, sequence.Length - 1);
        }
        private static void QuickSort<K>(K[] sequence, IComparer<K> comparer, int A, int B)
        {
            if (A >= B) //subarray is trivially sorted
                return;
            int left = A;
            int right = B - 1;
            K pivot = sequence[B];
            K temp; //temp object used for swapping
            while (left <= right)
            {
                //scan until reaching value equal or larger than pivot (or right marker)
                while (left <= right && comparer.Compare(sequence[left], pivot) < 0) left++;

                //scan until reaching value equal or smaller than pivot (or left marker)
                while (left <= right && comparer.Compare(sequence[right], pivot) > 0) right--;

                //swap values and shrink range
                if (left <= right)
                {
                    temp = sequence[left];
                    sequence[left] = sequence[right];
                    sequence[right] = temp;
                    left++;
                    right--;
                }
            }
            //put pivot into its final place (currently marked by left index)
            temp = sequence[left];
            sequence[left] = sequence[B];
            sequence[B] = temp;

            // recursive calls
            QuickSort(sequence, comparer, A, left - 1);
            QuickSort(sequence, comparer, left + 1, B);
        }
    }
}
//https://www.geeksforgeeks.org/quicksort-using-random-pivoting/
//https://stackoverflow.com/questions/40749544/randomized-quicksort-c-sharp
//Data Structures and Algorithms in Java, 6th Edition