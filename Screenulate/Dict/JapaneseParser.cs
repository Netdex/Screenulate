using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTrie;
using Wacton.Desu.Japanese;
using Wacton.Desu.Kanji;
using Wacton.Desu.Names;
using Reading = Wacton.Desu.Japanese.Reading;

namespace Screenulate.Dict
{
    class JapaneseParser
    {
        private JapaneseDictionary JpDict { get; set; }
        private NameDictionary NameDict { get; set; }
        private KanjiDictionary KanjiDict { get; set; }

        private StringTrie<HashSet<IJapaneseEntry>> JpTrie { get; set; }

        public event EventHandler LoadCompleted;

        public JapaneseParser()
        {
        }

        public void Load()
        {
            JpDict = new JapaneseDictionary();
            JpTrie = new StringTrie<HashSet<IJapaneseEntry>>();

            // For each kanji/reading entry of a japanese entry, insert its (text,entry) pair into the trie.
            foreach (var jpEntry in JpDict.GetEntries())
            {
                foreach (var kanjiEntry in jpEntry.Kanjis)
                {
                    bool existsInTrie = JpTrie.TryGetValue(kanjiEntry.Text, out var entries);
                    if (!existsInTrie)
                    {
                        entries = new HashSet<IJapaneseEntry>();
                        JpTrie.Add(kanjiEntry.Text, entries);
                    }

                    entries.Add(jpEntry);
                }

                foreach (var readingEntry in jpEntry.Readings)
                {
                    bool existsInTrie = JpTrie.TryGetValue(readingEntry.Text, out var entries);
                    if (!existsInTrie)
                    {
                        entries = new HashSet<IJapaneseEntry>();
                        JpTrie.Add(readingEntry.Text, entries);
                    }
                    entries.Add(jpEntry);
                }
            }

            //NameDict = new NameDictionary();
            //KanjiDict = new KanjiDictionary();
            LoadCompleted?.Invoke(this, null);
        }
    }
}