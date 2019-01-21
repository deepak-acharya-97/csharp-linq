using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace play_with_linq
{
    class Program
    {
        private static IEnumerable<string> Suits() {
            yield return "clubs";
            yield return "diamonds";
            yield return "hearts";
            yield return "spades";
        }

        private static IEnumerable<string> Ranks()
        {
            yield return "two";
            yield return "three";
            yield return "four";
            yield return "five";
            yield return "six";
            yield return "seven";
            yield return "eight";
            yield return "nine";
            yield return "ten";
            yield return "jack";
            yield return "queen";
            yield return "king";
            yield return "ace";
        }

        private static int ShufflesToReachInitStage(IEnumerable<object> shuffle)
        {
            var Times=0;

            IEnumerable<object> Shuffle=shuffle;

            do
            {
                Shuffle = Shuffle.Take(26).LogQuery("Top Half").InterleaveSequenceWith(Shuffle.Skip(26).LogQuery("Bottom Half")).LogQuery("Shuffle");
                // swapping left with right argument for extension method
                //Shuffle = Shuffle.Skip(26).LogQuery("Bottom Half").InterleaveSequenceWith(Shuffle.Take(26).LogQuery("Top Half")).LogQuery("Shuffle");
                Times+=1;
                Console.WriteLine(Times);
            } while(!Shuffle.SequenceEquals(shuffle));
            return Times;
        }

        private static void WriteToLogFile(string message)
        {
            using(var writer = File.AppendText("debug.log"))
            {
                writer.WriteLine(message);
            }
        }
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");

            var startingDeck = (from s in Suits().LogQuery("Suit Generation")
                                    from r in Ranks().LogQuery("Rank Generation")
                                        select new {
                                            Suit = s,
                                            Rank = r
                                        }).LogQuery("Starting Deck");
            //var startingDeckUsingMethods = Suits().SelectMany(suit => Ranks().Select(rank => new {Suit=suit, Rank=rank}));
            //startingDeckUsingMethods.PrintAnonymousObject();

            //Order Manipulation

            WriteToLogFile("I hope Nothing Will Be There Before This");
            var top=startingDeck.Take(26).LogQuery("Taking Top 26 Cards");
            var bottom=startingDeck.Skip(26).LogQuery("Taking Bottom 26 Cards");

            var shuffled = top.InterleaveSequenceWith(bottom);

            shuffled.PrintAnonymousObject();

            Console.WriteLine("Number Of Shuffles Needed In Order To Reach Initial Stage = "+ShufflesToReachInitStage(shuffled));
        }
    }
}
