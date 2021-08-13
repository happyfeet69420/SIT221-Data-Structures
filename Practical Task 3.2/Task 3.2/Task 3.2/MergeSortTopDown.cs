using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    class MergeSortTopDown : ISorter
    {
        private void MergeSort<K>(K[] sequence, IComparer<K> comparer)
        {
            //array is trivially sorted
            int n = sequence.Length;
            int leftSide;
            int rightSide;
            if (n < 2) return; //divide

            if (n % 2 == 0)
            {
                leftSide = n / 2;
                rightSide = n / 2;
            }
            else
            {
                leftSide = n / 2 + 1;
                rightSide = n / 2;
            }

            K[] left = new K[leftSide]; //genric array
            K[] right = new K[rightSide]; //genric array

            Array.Copy(sequence, 0, left, 0, leftSide); //copy of first half
            Array.Copy(sequence, leftSide, right, 0, rightSide); //copy of second half

            //conquer (with recursion)
            MergeSort(left, comparer); //sort copy of first half
            MergeSort(right, comparer); //sort copy of second half

            merge(left, right, sequence, comparer); //merge sorted halves back into original
        }

        private void merge<K>(K[] sequence1, K[] sequence2, K[] sequence, IComparer<K> comparer)
        {
            int i = 0, j = 0;

            while (i + j < sequence.Length)
            {
                if (j == sequence2.Length || (i < sequence1.Length && comparer.Compare(sequence1[i], sequence2[j]) < 0))
                {
                    sequence[i + j] = sequence1[i++]; //copy ith element if sequence and increment i
                }
                else
                {
                    sequence[i + j] = sequence2[j++]; //copy jth element of sequence2 and increment j
                }
            }
        }
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;
            MergeSort(sequence, comparer);
        }
    }
}
//Data Structures and Algorithms in Java, 6th Edition
