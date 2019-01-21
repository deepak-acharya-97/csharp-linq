using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace play_with_linq
{
    public static class Extensions
    {
        //refer https://stackoverflow.com/questions/9750078/get-value-from-anonymous-type
        public static void PrintAnonymousObject<T>(this IEnumerable<T> objects)
        {
            foreach(var obj in objects)
            {
                var members = obj.GetType().GetProperties();
                foreach(var member in members)
                {
                    var property=member.ToString().Split(" ")[1];
                    Console.Write(property+" = "+obj.GetType().GetProperty(property).GetValue(obj)+"\t");
                }
                Console.WriteLine();
            }
        }

        public static IEnumerable<T> InterleaveSequenceWith<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstIteration = first.GetEnumerator();
            var secondIteration = second.GetEnumerator();

            while(firstIteration.MoveNext() && secondIteration.MoveNext())
            {
                yield return firstIteration.Current;
                yield return secondIteration.Current;
            }
        }

        public static bool SequenceEquals<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstIteration = first.GetEnumerator();
            var secondIteration = second.GetEnumerator();

            while(firstIteration.MoveNext() && secondIteration.MoveNext())
            {
                if(!firstIteration.Current.Equals(secondIteration.Current))
                {
                    return false;
                }
            }
            return true;
        }

        public static IEnumerable<T> LogQuery<T>(this IEnumerable<T> sequence, string tag)
        {
            using(var writer = File.AppendText("debug.log"))
            {
                writer.WriteLine($"Excecuting Query {tag}");
            }
            return sequence;
        }
    }
}