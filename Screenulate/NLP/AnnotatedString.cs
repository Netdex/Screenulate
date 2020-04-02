using System;
using System.Collections.Generic;
using Wacton.Desu.Enums;
using Wacton.Desu.Japanese;

namespace Screenulate.NLP
{
    public class AnnotatedString
    {
        public abstract class Token
        {
            public int Offset { get; }
            public int Length { get; }


            public Lazy<int> DisplayPriority { get; }

            protected Token(int offset, int length)
            {
                Offset = offset;
                Length = length;
                DisplayPriority = new Lazy<int>(CalculatePriority);
            }

            protected abstract int CalculatePriority();
        }

        public class JapaneseToken : Token, IComparable<JapaneseToken>
        {
            public enum Source
            {
                Kanji,
                Reading
            }

            private Source TokenSource { get; }

            public IJapaneseEntry JapaneseEntry { get; }

            public IKanji SourceKanji { get; }
            public IReading SourceReading { get; }

            public JapaneseToken(int offset, int length, IJapaneseEntry japaneseEntry, IKanji sourceKanji) : base(
                offset, length)
            {
                JapaneseEntry = japaneseEntry;
                SourceKanji = sourceKanji;
                TokenSource = Source.Kanji;
            }

            public JapaneseToken(int offset, int length, IJapaneseEntry japaneseEntry, IReading sourceReading) : base(
                offset, length)
            {
                JapaneseEntry = japaneseEntry;
                SourceReading = sourceReading;
                TokenSource = Source.Reading;
            }

            protected override int CalculatePriority()
            {
                int lengthScore = 1000 * Length;

                IEnumerable<Priority> priorities;
                switch (TokenSource)
                {
                    case Source.Kanji:
                        priorities = SourceKanji.Priorities;
                        break;
                    case Source.Reading:
                        priorities = SourceReading.Priorities;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                int priorityScore = 0;
                foreach (var priority in priorities)
                {
                    if (priority.Value >= Priority.SpecialCommon1.Value &&
                        priority.Value <= Priority.SpecialCommon2.Value)
                    {
                        priorityScore += 1000 - 100 * (priority.Value - Priority.SpecialCommon1.Value);
                    }
                    else if (priority.Value >= Priority.Frequency1.Value &&
                             priority.Value <= Priority.Frequency48.Value)
                    {
                        priorityScore += 100 - 5 * (priority.Value - Priority.Frequency1.Value);
                    }
                    else if (priority.Value >= Priority.Newspaper1.Value &&
                             priority.Value <= Priority.Newspaper2.Value)
                    {
                        priorityScore += 100 - 50 * (priority.Value - Priority.Newspaper1.Value);
                    }
                    else if (priority.Value >= Priority.Ichimango1.Value &&
                             priority.Value <= Priority.Ichimango2.Value)
                    {
                        priorityScore += 50 - 10 * (priority.Value - Priority.Ichimango1.Value);
                    }
                    else if (priority.Value >= Priority.Loanword1.Value && priority.Value <= Priority.Loanword2.Value)
                    {
                        priorityScore += 100 - 10 * (priority.Value - Priority.Ichimango1.Value);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }

                return lengthScore + priorityScore;
            }

            public int CompareTo(JapaneseToken other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                return -DisplayPriority.Value.CompareTo(other.DisplayPriority.Value);
            }

            public override string ToString()
            {
                switch (TokenSource)
                {
                    case Source.Reading:
                        return $"{SourceReading} => {JapaneseEntry}";
                    case Source.Kanji:
                        return $"{SourceKanji} => {JapaneseEntry}";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public string Text { get; }
        public SortedDictionary<int, List<JapaneseToken>> JapaneseTokens { get; }

        public AnnotatedString(string text)
        {
            Text = text;
            JapaneseTokens = new SortedDictionary<int, List<JapaneseToken>>();
        }
    }
}