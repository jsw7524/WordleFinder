using System;

namespace MyApp // Note: actual namespace depends on the project name.
{


    public class Wordle
    {
        public string Word { set; get; }
        public bool IsPossible { set; get; }
    }
    public class WordleHelper
    {
        int _n;
        public WordleHelper(int n)
        {
            _n = n;
        }
        public List<Wordle> GetWords(string[] data)
        {
            List<Wordle> result = new List<Wordle>();
            foreach (string w in data)
            {
                result.Add(new Wordle() { Word = w, IsPossible = true });
            }
            return result;
        }

        public Dictionary<int, Dictionary<char, List<Wordle>>> GetDict(List<Wordle> wordles)
        {
            Dictionary<int, Dictionary<char, List<Wordle>>> result = new Dictionary<int, Dictionary<char, List<Wordle>>>();
            for (int i = 0; i < _n; i++)
            {
                var dict = new Dictionary<char, List<Wordle>>();

                foreach (Wordle w in wordles)
                {
                    if (!dict.ContainsKey(w.Word[i]))
                    {
                        dict.Add(w.Word[i], new List<Wordle>());
                    }
                    dict[w.Word[i]].Add(w);

                }
                result.Add(i, dict);
            }
            return result;
        }


    }

    public class WordleFinderController
    {
        WordleHelper wh;
        WordleFinder wf;
        List<Wordle> words;
        List<char> possibleChars;
        Dictionary<int, Dictionary<char, List<Wordle>>> dict;
        public WordleFinderController(string[] data = null, int n = 5)
        {
            try
            {
                wh = new WordleHelper(n);
                wf = new WordleFinder(n);
                words = wh.GetWords(data);
                dict = wh.GetDict(words);
                possibleChars = new List<char>();
            }
            catch (Exception e)
            {
                int i = 1;
            }

        }

        public void FindAnswer(string guess, string respond)
        {

            for (int i = 0; i < respond.Length; i++)
            {
                char state = respond[i];
                switch (state)
                {
                    case 'b':
                        wf.SetImpossible(i, guess[i], dict);
                        break;
                    case 'g':
                        wf.SetPossible(i, guess[i], dict[i]);
                        possibleChars.Add(guess[i]);
                        break;
                    case 'o':
                        wf.SetNotHere(i, guess[i], dict[i]);
                        possibleChars.Add(guess[i]);
                        break;
                }
            }
            wf.Filter(words, possibleChars);
        }

        public List<string> ListPossibleWords()
        {
            return words.Where(w => w.IsPossible == true).Select(w => w.Word).ToList();
        }

    }


    public class WordleFinder
    {

        Random random = new Random(5);
        int _n;


        public WordleFinder(int n = 5)
        {
            _n = n;
        }

        public void SetPossible(int i, char posssibleChar, Dictionary<char, List<Wordle>> candidate)
        {
            foreach (char ch in candidate.Keys)
            {
                if (posssibleChar != ch)
                {
                    foreach (Wordle w in candidate[ch])
                    {
                        w.IsPossible = false;
                    }
                }
            }
        }

        public void SetNotHere(int i, char imposssibleChar, Dictionary<char, List<Wordle>> candidate)
        {
            foreach (Wordle w in candidate[imposssibleChar])
            {
                w.IsPossible = false;
            }

        }

        public void SetImpossible(int i, char imposssibleChar, Dictionary<int, Dictionary<char, List<Wordle>>> candidate)
        {

            foreach (int index in candidate.Keys)
            {
                Dictionary<char, List<Wordle>> tmp = candidate[index];
                foreach (Wordle w in tmp[imposssibleChar])
                {
                    w.IsPossible = false;
                }

            }
        }

        public void Filter(List<Wordle> candidate, List<char> mustHave)
        {
            foreach (char ch in mustHave)
            {
                foreach (Wordle w in candidate)
                {
                    if (!(w.Word.Contains(ch)))
                    {
                        w.IsPossible = false;
                    }
                }
            }
        }

        //public string PickWord()
        //{

        //    var candidate = _wordles.Where(w => w.IsPossible == true).ToList();
        //    return candidate[random.Next(candidate.Count)].Word;
        //}





    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //////notes
            //////acrid
            //int n = int.Parse(Console.ReadLine());
            //WordleHelper wh = new WordleHelper(n);
            //WordleFinder wf = new WordleFinder(n);
            //string guess;
            //string respond;
            //var words = wh.GetWords();
            //var dict = wh.GetDict(words);

            //List<char> possibleChars = new List<char>();
            //while (true)
            //{
            //    guess = Console.ReadLine().ToLower();
            //    respond = Console.ReadLine().ToLower();
            //    for (int i = 0; i < respond.Length; i++)
            //    {
            //        char state = respond[i];
            //        switch (state)
            //        {
            //            case 'b':
            //                wf.SetImpossible(i, guess[i], dict);
            //                break;
            //            case 'g':
            //                wf.SetPossible(i, guess[i], dict[i]);
            //                possibleChars.Add(guess[i]);
            //                break;
            //            case 'o':
            //                wf.SetNotHere(i, guess[i], dict[i]);
            //                possibleChars.Add(guess[i]);
            //                break;
            //        }
            //    }
            //    wf.Filter(words, possibleChars);
            //    Console.WriteLine("-------------------------------------------------");
            //    //foreach (var r in wf.ListPossibleWords(words))
            //    //{
            //    //    Console.WriteLine(r);
            //    //}

            //}
        }
    }
}