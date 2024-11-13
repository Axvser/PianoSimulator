using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoSimulator
{
    public static class CollectionService
    {
        public static void RemoveSubList<T>(this List<T> source, List<T> subList)
        {
            int index = source.IndexOfSubList(subList);
            while (index != -1)
            {
                source.RemoveRange(index, subList.Count);
                index = source.IndexOfSubList(subList);
            }
        }
        public static int IndexOfSubList<T>(this List<T> source, List<T> subList)
        {
            for (int i = 0; i <= source.Count - subList.Count; i++)
            {
                if (source.GetRange(i, subList.Count).SequenceEqual(subList))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
