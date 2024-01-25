using System.Diagnostics;
using System.Reflection;

namespace Exam13
{
    internal class Work
    {
        private string text;
        private List<string> listText;
        private LinkedList<string> listLinkedText;

        private int topCount;
        public Work()
        {
            topCount = 10;

            GetText();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            AddTextToList(text, true);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            AddTextToLinkedList(text, true);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            GetTopWordsFromList(topCount);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            GetTopWordsFromLinkedList(topCount);

            Console.ForegroundColor = ConsoleColor.White;
        }

        

        private void GetText()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames()
              .Single(str => str.EndsWith(".txt"));
            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            using StreamReader reader = new StreamReader(stream);
            text = reader.ReadToEnd().Trim();
        }

        private string[] GetArrayFromText(string text)
        {
            var delimiter = new[] {" ", "\r", "\n"};

            return text.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
        }
        private string GetNoPunctuationText()
        {
            return new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
        }
        private void AddTextToList(string text, bool getTime)
        {
            var mas = GetArrayFromText(text);

            listText = new List<string>();
            var stopWatch = Stopwatch.StartNew();
            foreach (var str in mas)
            {
                listText.Add(str);
            }
            if (getTime)
            {
                Console.WriteLine($"Вставка в List {stopWatch.Elapsed.TotalMilliseconds} мс");
            }
        }
        private void AddTextToLinkedList(string text, bool gettime)
        {
            var mas = GetArrayFromText(text);

            listLinkedText = new LinkedList<string>();
            var stopWatch = Stopwatch.StartNew();
            foreach(var str in mas)
            {
                listLinkedText.AddFirst(str);    
            }
            if (gettime)
            {
                Console.WriteLine($"Вставка в LinkedList {stopWatch.Elapsed.TotalMilliseconds} мс");
            }
        }
        private void GetTopWordsFromList(int count)
        {
            var noPunctuationText = GetNoPunctuationText();

            AddTextToList(noPunctuationText, false);
            var stopWatch = Stopwatch.StartNew();
            var groupingList = listText.GroupBy(a=>a).Select(group => new 
            {
                word = group.Key,
                count = group.Count()
            }).OrderByDescending(a=>a.count).Take(count).ToList();

            Console.WriteLine();
            Console.WriteLine($"Группировка List {stopWatch.Elapsed.TotalMilliseconds} мс");
            Console.WriteLine();

            foreach (var group in groupingList)
            {
                Console.WriteLine($"{group.word} - {group.count}");
            }
        }
        private void GetTopWordsFromLinkedList(int count)
        {
            var noPunctuationText = GetNoPunctuationText();

            AddTextToLinkedList(noPunctuationText, false);
            var stopWatch = Stopwatch.StartNew();
            var groupingList = listLinkedText.GroupBy(a => a).Select(group => new
            {
                word = group.Key,
                count = group.Count()
            }).OrderByDescending(a => a.count).Take(count).ToList();

            Console.WriteLine();
            Console.WriteLine($"Группировка LinkedList {stopWatch.Elapsed.TotalMilliseconds} мс");
            Console.WriteLine();

            foreach (var group in groupingList)
            {
                Console.WriteLine($"{group.word} - {group.count}");
            }
        }
    }
}
