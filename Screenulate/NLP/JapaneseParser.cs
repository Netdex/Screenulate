using System.Collections.Generic;
using System.Linq;
using KTrie;
using Wacton.Desu.Japanese;
using Wacton.Desu.Kanji;
using Wacton.Desu.Names;

namespace Screenulate.NLP
{
    class JapaneseParser
    {
        private JapaneseDictionary JpDict { get; set; }
        private NameDictionary NameDict { get; set; }
        private KanjiDictionary KanjiDict { get; set; }

        private StringTrie<List<(Wacton.Desu.Japanese.IKanji, IJapaneseEntry)>> JpKanjiTrie { get; set; }
        private StringTrie<List<(Wacton.Desu.Japanese.IReading, IJapaneseEntry)>> JpReadingTrie { get; set; }

        public bool IsLoaded { get; private set; }

        public JapaneseParser()
        {
        }

        public void Load()
        {
            JpDict = new JapaneseDictionary();
            JpKanjiTrie = new StringTrie<List<(Wacton.Desu.Japanese.IKanji, IJapaneseEntry)>>();
            JpReadingTrie = new StringTrie<List<(Wacton.Desu.Japanese.IReading, IJapaneseEntry)>>();

            // For each kanji entry of a japanese entry, insert its (text,entry) pair into the trie.
            foreach (var jpEntry in JpDict.GetEntries())
            {
                foreach (var kanjiEntry in jpEntry.Kanjis)
                {
                    bool existsInTrie = JpKanjiTrie.TryGetValue(kanjiEntry.Text, out var entries);
                    if (!existsInTrie)
                    {
                        entries = new List<(Wacton.Desu.Japanese.IKanji, IJapaneseEntry)>();
                        JpKanjiTrie.Add(kanjiEntry.Text, entries);
                    }

                    entries.Add((kanjiEntry, jpEntry));
                }

                foreach (var readingEntry in jpEntry.Readings)
                {
                    bool existsInTrie = JpReadingTrie.TryGetValue(readingEntry.Text, out var entries);
                    if (!existsInTrie)
                    {
                        entries = new List<(Wacton.Desu.Japanese.IReading, IJapaneseEntry)>();
                        JpReadingTrie.Add(readingEntry.Text, entries);
                    }

                    entries.Add((readingEntry, jpEntry));
                }
            }

            IsLoaded = true;

            //NameDict = new NameDictionary();
            //KanjiDict = new KanjiDictionary();
        }

        public AnnotatedString Annotate(string text)
        {
            AnnotatedString annotatedString = new AnnotatedString(text);
            for (int i = 0; i < text.Length; ++i)
            {
                annotatedString.JapaneseTokens[i] = new List<AnnotatedString.JapaneseToken>();
                for (int j = 1; j < text.Length - i; ++j)
                {
                    string substr = text.Substring(i, j);
                    if (JpKanjiTrie.TryGetValue(substr, out var kanjiEntries))
                    {
                        annotatedString.JapaneseTokens[i].AddRange(kanjiEntries.Select(
                            x => new AnnotatedString.JapaneseToken(i, j,
                                x.Item2, x.Item1)));
                    }

                    if (JpReadingTrie.TryGetValue(substr, out var readingEntries))
                    {
                        annotatedString.JapaneseTokens[i].AddRange(readingEntries.Select(
                            x => new AnnotatedString.JapaneseToken(i, j,
                                x.Item2, x.Item1)));
                    }
                }

                annotatedString.JapaneseTokens[i].Sort();
            }

            return annotatedString;
        }
    }
}