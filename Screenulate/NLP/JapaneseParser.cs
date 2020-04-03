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
                annotatedString.JapaneseTokens[i] = new List<JapaneseToken>();
                var jpTokens = annotatedString.JapaneseTokens[i];
                for (int j = 1; j <= text.Length - i; ++j)
                {
                    string substr = text.Substring(i, j);
                    if (TryTokenize(substr, out var tokens))
                        jpTokens.AddRange(tokens);
                    foreach (var deinflectedString in Deinflector.Deinflect(substr))
                    {
                        if (TryTokenize(substr, deinflectedString, out var deinflectTokens))
                            jpTokens.AddRange(deinflectTokens);
                    }
                }

                annotatedString.JapaneseTokens[i].Sort();
            }

            return annotatedString;
        }

        public bool TryTokenize(string text, out IEnumerable<JapaneseToken> tokens)
        {
            if (JpKanjiTrie.TryGetValue(text, out var kanjiEntries))
            {
                tokens = kanjiEntries.Select(x => new JapaneseToken(text, x.Item2, x.Item1));
                return true;
            }

            if (JpReadingTrie.TryGetValue(text, out var readingEntries))
            {
                tokens = readingEntries.Select(x => new JapaneseToken(text, x.Item2, x.Item1));
                return true;
            }

            tokens = Enumerable.Empty<JapaneseToken>();
            return false;
        }

        public bool TryTokenize(string source, Deinflector.DeinflectedString deinflected,
            out IEnumerable<JapaneseToken> tokens)
        {
            if (JpKanjiTrie.TryGetValue(deinflected.Text, out var kanjiEntries))
            {
                tokens = kanjiEntries.Select(x => 
                    new JapaneseToken(source, x.Item2, x.Item1, deinflected.Inflections));
                return true;
            }

            if (JpReadingTrie.TryGetValue(deinflected.Text, out var readingEntries))
            {
                tokens = readingEntries.Select(
                    x => 
                        new JapaneseToken(source, x.Item2, x.Item1, deinflected.Inflections));
                return true;
            }

            tokens = Enumerable.Empty<JapaneseToken>();
            return false;
        }
    }
}