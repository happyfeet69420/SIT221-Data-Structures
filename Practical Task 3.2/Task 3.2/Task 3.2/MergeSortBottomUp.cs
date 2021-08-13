using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    class MergeSortBottomUp : ISorter
    {
        public MergeSortBottomUp()
        {

        }
        public void Sort<K>(K[] sequence, IComparer<K> comparer) where K : IComparable<K>
        {
            if (comparer == null) comparer = Comparer<K>.Default;
            mergeSortBottomUp(sequence, comparer);
        }
        public static void merge<K>(K[] in1, K[] out1, IComparer<K> comparer, int start, int inc)
        {
            int end1 = Math.Min(start + inc, in1.Length);
            int end2 = Math.Min(start + 2 * inc, in1.Length);
            int x = start;
            int y = start + inc;
            int z = start;

            while (x < end1 && y < end2)
            {
                if (comparer.Compare(in1[x], in1[y]) < 0)
                {
                    out1[z++] = in1[x++];
                }
                else
                {
                    out1[z++] = in1[y++];
                }
            }
            if (x < end1)
            {
                Array.Copy(in1, x, out1, z, end1 - x);
            }
            else if (y < end2)
            {
                Array.Copy(in1, y, out1, z, end2 - y);
            }
        }
        public static void mergeSortBottomUp<K>(K[] orig, IComparer<K> comparer)
        {
            int n = orig.Length;
            K[] src = orig;
            K[] dest = new K[n]; //make a new temp array
            K[] temp; //reference used only for swapping

            for (int i = 1; i < n; i *= 2)
            {
                for (int j = 0; j < n; j += 2*i)
                {
                    merge(src, dest, comparer, j, i);

                }
                temp = src;
                src = dest;
                dest = temp;

            }
            if  (orig != src)
            {
                Array.Copy(src, 0, orig, 0, n);
            }
        }
    }
}
//Data Structures and Algorithms in Java, 6th Edition