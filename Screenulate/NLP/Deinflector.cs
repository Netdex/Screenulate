using System;
using System.Collections.Generic;
using System.Linq;
using Screenulate.Utility;

namespace Screenulate.NLP
{
    public class Deinflector
    {
        public class Reason : ClassEnumeration<Reason>
        {
            [Value] public static readonly Reason PolitePastNegative = new Reason("polite past negative");
            [Value] public static readonly Reason PoliteNegative = new Reason("polite negative");
            [Value] public static readonly Reason PoliteVolitional = new Reason("polite volitional");
            [Value] public static readonly Reason SuffixChau = new Reason("-chau");
            [Value] public static readonly Reason SuffixSugiru = new Reason("-sugiru");
            [Value] public static readonly Reason SuffixNasai = new Reason("-nasai");
            [Value] public static readonly Reason PolitePast = new Reason("polite past");
            [Value] public static readonly Reason SuffixTara = new Reason("-tara");
            [Value] public static readonly Reason Tari = new Reason("-tari");
            [Value] public static readonly Reason Causative = new Reason("causative");
            [Value] public static readonly Reason PotentialOrPassive = new Reason("potential or passive");
            [Value] public static readonly Reason SuffixSou = new Reason("-sou");
            [Value] public static readonly Reason SuffixTai = new Reason("-tai");
            [Value] public static readonly Reason Polite = new Reason("polite");
            [Value] public static readonly Reason Past = new Reason("past");
            [Value] public static readonly Reason Negative = new Reason("negative");
            [Value] public static readonly Reason Passive = new Reason("passive");
            [Value] public static readonly Reason SuffixBa = new Reason("-ba");
            [Value] public static readonly Reason Volitional = new Reason("volitional");
            [Value] public static readonly Reason Potential = new Reason("potential");
            [Value] public static readonly Reason PassiveOrCausative = new Reason("passive or causative");
            [Value] public static readonly Reason SuffixTe = new Reason("-te");
            [Value] public static readonly Reason SuffixZu = new Reason("-zu");
            [Value] public static readonly Reason Imperative = new Reason("imperative");
            [Value] public static readonly Reason MasuStem = new Reason("masu stem");
            [Value] public static readonly Reason Adverb = new Reason("adv");
            [Value] public static readonly Reason Noun = new Reason("noun");
            [Value] public static readonly Reason ImperativeNegative = new Reason("imperative negative");

            public string Verbose { get; }

            private Reason(string verbose)
            {
                Verbose = verbose;
            }

            public override string ToString() => Verbose;
        }

        public class Inflection
        {
            public string From { get; }
            public string To { get; }
            public int TypeMask { get; }
            public Reason Reason { get; }

            public Inflection(string from, string to, int typeMask, Reason reason)
            {
                From = from;
                To = to;
                TypeMask = typeMask;
                Reason = reason;
            }
        }

        public class DeinflectedString : IComparable<DeinflectedString>
        {
            public string Text { get; set; }
            public int TypeMask { get; set; } = 0xff;
            public bool Terminal { get; set; } = false;
            public List<Inflection> Inflections { get; }

            public DeinflectedString()
            {
                Inflections = new List<Inflection>();
            }

            public bool TryTransform(Inflection inflection, out DeinflectedString newValue)
            {
                if (Text.EndsWith(inflection.From) && (TypeMask & inflection.TypeMask) != 0)
                {
                    newValue = new DeinflectedString()
                    {
                        Text = Text.Substring(0, Text.Length - inflection.From.Length) + inflection.To,
                        TypeMask = inflection.TypeMask << 8
                    };
                    newValue.Inflections.AddRange(Inflections);
                    newValue.Inflections.Add(inflection);
                    return true;
                }

                newValue = null;
                return false;
            }

            public int CompareTo(DeinflectedString other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                return string.Compare(Text, other.Text, StringComparison.Ordinal);
            }
        }

        public static IEnumerable<DeinflectedString> Deinflect(string text)
        {
            SortedSet<DeinflectedString> intermediates = new SortedSet<DeinflectedString>();
            intermediates.Add(new DeinflectedString() {Text = text});

            bool changed = true;
            while (changed)
            {
                changed = false;
                List<DeinflectedString> pendingStrings = new List<DeinflectedString>();
                foreach (var deinflected in intermediates.Where(deinflected => !deinflected.Terminal))
                {
                    foreach (var inflection in RuleTable.Value)
                    {
                        if (inflection.From.Length > deinflected.Text.Length)
                            break;
                        bool result = deinflected.TryTransform(inflection, out var newValue);
                        changed |= result;
                        if (result)
                            pendingStrings.Add(newValue);
                    }

                    deinflected.Terminal = true;
                }

                intermediates.UnionWith(pendingStrings);
            }

            return intermediates;
        }

        // Deinflect Rules 20081220-0509 | by Jonathan Zarate | http://www.polarcloud.com
        // It is very important that these entries are sorted in ascending length of From.
        private static readonly Lazy<Inflection[]> RuleTable = new Lazy<Inflection[]>(() => new[]
        {
            new Inflection("ろ", "る", 384, Reason.Values[23]),
            new Inflection("れ", "れる", 384, Reason.Values[24]),
            new Inflection("れ", "る", 640, Reason.Values[23]),
            new Inflection("り", "る", 640, Reason.Values[24]),
            new Inflection("り", "りる", 384, Reason.Values[24]),
            new Inflection("よ", "る", 384, Reason.Values[23]),
            new Inflection("め", "める", 384, Reason.Values[24]),
            new Inflection("め", "む", 640, Reason.Values[23]),
            new Inflection("み", "む", 640, Reason.Values[24]),
            new Inflection("み", "みる", 384, Reason.Values[24]),
            new Inflection("べ", "べる", 384, Reason.Values[24]),
            new Inflection("べ", "ぶ", 640, Reason.Values[23]),
            new Inflection("へ", "へる", 384, Reason.Values[24]),
            new Inflection("び", "ぶ", 640, Reason.Values[24]),
            new Inflection("び", "びる", 384, Reason.Values[24]),
            new Inflection("ひ", "ひる", 384, Reason.Values[24]),
            new Inflection("ね", "ねる", 384, Reason.Values[24]),
            new Inflection("ね", "ぬ", 640, Reason.Values[23]),
            new Inflection("に", "ぬ", 640, Reason.Values[24]),
            new Inflection("に", "にる", 384, Reason.Values[24]),
            new Inflection("な", "", 7040, Reason.Values[27]),
            new Inflection("で", "でる", 384, Reason.Values[24]),
            new Inflection("て", "る", 2432, Reason.Values[21]),
            new Inflection("て", "てる", 384, Reason.Values[24]),
            new Inflection("て", "つ", 640, Reason.Values[23]),
            new Inflection("ち", "つ", 640, Reason.Values[24]),
            new Inflection("ち", "ちる", 384, Reason.Values[24]),
            new Inflection("た", "る", 2432, Reason.Values[14]),
            new Inflection("ぜ", "ぜる", 384, Reason.Values[24]),
            new Inflection("せ", "せる", 384, Reason.Values[24]),
            new Inflection("せ", "す", 640, Reason.Values[23]),
            new Inflection("ず", "る", 2432, Reason.Values[22]),
            new Inflection("じ", "じる", 384, Reason.Values[24]),
            new Inflection("し", "す", 640, Reason.Values[24]),
            new Inflection("さ", "い", 1152, Reason.Values[26]),
            new Inflection("げ", "げる", 384, Reason.Values[24]),
            new Inflection("げ", "ぐ", 640, Reason.Values[23]),
            new Inflection("け", "ける", 384, Reason.Values[24]),
            new Inflection("け", "く", 640, Reason.Values[23]),
            new Inflection("く", "い", 1152, Reason.Values[25]),
            new Inflection("ぎ", "ぐ", 640, Reason.Values[24]),
            new Inflection("ぎ", "ぎる", 384, Reason.Values[24]),
            new Inflection("き", "く", 640, Reason.Values[24]),
            new Inflection("き", "きる", 384, Reason.Values[24]),
            new Inflection("え", "える", 384, Reason.Values[24]),
            new Inflection("え", "う", 640, Reason.Values[23]),
            new Inflection("い", "る", 2176, Reason.Values[23]),
            new Inflection("い", "う", 640, Reason.Values[24]),
            new Inflection("い", "いる", 384, Reason.Values[24]),
            new Inflection("んで", "む", 640, Reason.Values[21]),
            new Inflection("んで", "ぶ", 640, Reason.Values[21]),
            new Inflection("んで", "ぬ", 640, Reason.Values[21]),
            new Inflection("んだ", "む", 640, Reason.Values[14]),
            new Inflection("んだ", "ぶ", 640, Reason.Values[14]),
            new Inflection("んだ", "ぬ", 640, Reason.Values[14]),
            new Inflection("わず", "う", 640, Reason.Values[22]),
            new Inflection("ろう", "る", 640, Reason.Values[18]),
            new Inflection("れる", "る", 2817, Reason.Values[19]),
            new Inflection("れば", "る", 7040, Reason.Values[17]),
            new Inflection("らず", "る", 640, Reason.Values[22]),
            new Inflection("よう", "る", 2432, Reason.Values[18]),
            new Inflection("もう", "む", 640, Reason.Values[18]),
            new Inflection("める", "む", 513, Reason.Values[19]),
            new Inflection("めば", "む", 640, Reason.Values[17]),
            new Inflection("まず", "む", 640, Reason.Values[22]),
            new Inflection("ます", "る", 2432, Reason.Values[13]),
            new Inflection("ぼう", "ぶ", 640, Reason.Values[18]),
            new Inflection("べる", "ぶ", 513, Reason.Values[19]),
            new Inflection("べば", "ぶ", 640, Reason.Values[17]),
            new Inflection("ばず", "ぶ", 640, Reason.Values[22]),
            new Inflection("のう", "ぬ", 640, Reason.Values[18]),
            new Inflection("ねる", "ぬ", 513, Reason.Values[19]),
            new Inflection("ねば", "ぬ", 640, Reason.Values[17]),
            new Inflection("なず", "ぬ", 640, Reason.Values[22]),
            new Inflection("ない", "る", 2308, Reason.Values[15]),
            new Inflection("とう", "つ", 640, Reason.Values[18]),
            new Inflection("てる", "つ", 513, Reason.Values[19]),
            new Inflection("てば", "つ", 640, Reason.Values[17]),
            new Inflection("って", "る", 640, Reason.Values[21]),
            new Inflection("って", "つ", 640, Reason.Values[21]),
            new Inflection("って", "く", 640, Reason.Values[21]),
            new Inflection("って", "う", 640, Reason.Values[21]),
            new Inflection("った", "る", 640, Reason.Values[14]),
            new Inflection("った", "つ", 640, Reason.Values[14]),
            new Inflection("った", "く", 640, Reason.Values[14]),
            new Inflection("った", "う", 640, Reason.Values[14]),
            new Inflection("たり", "る", 2432, Reason.Values[8]),
            new Inflection("たら", "る", 2432, Reason.Values[7]),
            new Inflection("たず", "つ", 640, Reason.Values[22]),
            new Inflection("たい", "る", 2308, Reason.Values[12]),
            new Inflection("そう", "る", 2432, Reason.Values[11]),
            new Inflection("そう", "す", 640, Reason.Values[18]),
            new Inflection("そう", "い", 1152, Reason.Values[11]),
            new Inflection("せる", "す", 513, Reason.Values[19]),
            new Inflection("せよ", "する", 4224, Reason.Values[23]),
            new Inflection("せば", "す", 640, Reason.Values[17]),
            new Inflection("せず", "する", 4224, Reason.Values[22]),
            new Inflection("しろ", "する", 4224, Reason.Values[23]),
            new Inflection("して", "する", 4224, Reason.Values[21]),
            new Inflection("して", "す", 640, Reason.Values[21]),
            new Inflection("した", "する", 4224, Reason.Values[14]),
            new Inflection("した", "す", 640, Reason.Values[14]),
            new Inflection("さず", "す", 640, Reason.Values[22]),
            new Inflection("こず", "くる", 2176, Reason.Values[22]),
            new Inflection("ごう", "ぐ", 640, Reason.Values[18]),
            new Inflection("こう", "く", 640, Reason.Values[18]),
            new Inflection("こい", "くる", 2176, Reason.Values[23]),
            new Inflection("げる", "ぐ", 513, Reason.Values[19]),
            new Inflection("ける", "く", 513, Reason.Values[19]),
            new Inflection("げば", "ぐ", 640, Reason.Values[17]),
            new Inflection("けば", "く", 640, Reason.Values[17]),
            new Inflection("くて", "い", 1152, Reason.Values[21]),
            new Inflection("きて", "くる", 2176, Reason.Values[21]),
            new Inflection("きた", "くる", 2176, Reason.Values[14]),
            new Inflection("がず", "ぐ", 640, Reason.Values[22]),
            new Inflection("かず", "く", 640, Reason.Values[22]),
            new Inflection("おう", "う", 640, Reason.Values[18]),
            new Inflection("える", "う", 513, Reason.Values[19]),
            new Inflection("えば", "う", 640, Reason.Values[17]),
            new Inflection("いで", "ぐ", 640, Reason.Values[21]),
            new Inflection("いて", "く", 640, Reason.Values[21]),
            new Inflection("いだ", "ぐ", 640, Reason.Values[14]),
            new Inflection("いた", "く", 640, Reason.Values[14]),
            new Inflection("んだり", "む", 640, Reason.Values[8]),
            new Inflection("んだり", "ぶ", 640, Reason.Values[8]),
            new Inflection("んだり", "ぬ", 640, Reason.Values[8]),
            new Inflection("んだら", "む", 640, Reason.Values[7]),
            new Inflection("んだら", "ぶ", 640, Reason.Values[7]),
            new Inflection("んだら", "ぬ", 640, Reason.Values[7]),
            new Inflection("われる", "う", 513, Reason.Values[16]),
            new Inflection("わない", "う", 516, Reason.Values[15]),
            new Inflection("わせる", "う", 513, Reason.Values[9]),
            new Inflection("ります", "る", 640, Reason.Values[13]),
            new Inflection("りたい", "る", 516, Reason.Values[12]),
            new Inflection("りそう", "る", 640, Reason.Values[11]),
            new Inflection("られる", "る", 2817, Reason.Values[10]),
            new Inflection("らない", "る", 516, Reason.Values[15]),
            new Inflection("らせる", "る", 513, Reason.Values[9]),
            new Inflection("みます", "む", 640, Reason.Values[13]),
            new Inflection("みたい", "む", 516, Reason.Values[12]),
            new Inflection("みそう", "む", 640, Reason.Values[11]),
            new Inflection("まれる", "む", 513, Reason.Values[16]),
            new Inflection("まない", "む", 516, Reason.Values[15]),
            new Inflection("ません", "る", 2432, Reason.Values[1]),
            new Inflection("ませる", "む", 513, Reason.Values[9]),
            new Inflection("ました", "る", 2432, Reason.Values[6]),
            new Inflection("びます", "ぶ", 640, Reason.Values[13]),
            new Inflection("びたい", "ぶ", 516, Reason.Values[12]),
            new Inflection("びそう", "ぶ", 640, Reason.Values[11]),
            new Inflection("ばれる", "ぶ", 513, Reason.Values[16]),
            new Inflection("ばない", "ぶ", 516, Reason.Values[15]),
            new Inflection("ばせる", "ぶ", 513, Reason.Values[9]),
            new Inflection("にます", "ぬ", 640, Reason.Values[13]),
            new Inflection("にたい", "ぬ", 516, Reason.Values[12]),
            new Inflection("にそう", "ぬ", 640, Reason.Values[11]),
            new Inflection("なれる", "ぬ", 513, Reason.Values[16]),
            new Inflection("なない", "ぬ", 516, Reason.Values[15]),
            new Inflection("なせる", "ぬ", 513, Reason.Values[9]),
            new Inflection("なさい", "る", 2432, Reason.Values[5]),
            new Inflection("ったり", "る", 640, Reason.Values[8]),
            new Inflection("ったり", "つ", 640, Reason.Values[8]),
            new Inflection("ったり", "う", 640, Reason.Values[8]),
            new Inflection("ったら", "る", 640, Reason.Values[7]),
            new Inflection("ったら", "つ", 640, Reason.Values[7]),
            new Inflection("ったら", "う", 640, Reason.Values[7]),
            new Inflection("ちゃう", "る", 2306, Reason.Values[3]),
            new Inflection("ちます", "つ", 640, Reason.Values[13]),
            new Inflection("ちたい", "つ", 516, Reason.Values[12]),
            new Inflection("ちそう", "つ", 640, Reason.Values[11]),
            new Inflection("たれる", "つ", 513, Reason.Values[16]),
            new Inflection("たない", "つ", 516, Reason.Values[15]),
            new Inflection("たせる", "つ", 513, Reason.Values[9]),
            new Inflection("すぎる", "る", 2305, Reason.Values[4]),
            new Inflection("すぎる", "い", 1025, Reason.Values[4]),
            new Inflection("しよう", "する", 4224, Reason.Values[18]),
            new Inflection("します", "する", 4224, Reason.Values[13]),
            new Inflection("します", "す", 640, Reason.Values[13]),
            new Inflection("しない", "する", 4100, Reason.Values[15]),
            new Inflection("したり", "する", 4224, Reason.Values[8]),
            new Inflection("したり", "す", 640, Reason.Values[8]),
            new Inflection("したら", "する", 4224, Reason.Values[7]),
            new Inflection("したら", "す", 640, Reason.Values[7]),
            new Inflection("したい", "する", 4100, Reason.Values[12]),
            new Inflection("したい", "す", 516, Reason.Values[12]),
            new Inflection("しそう", "する", 4224, Reason.Values[11]),
            new Inflection("しそう", "す", 640, Reason.Values[11]),
            new Inflection("される", "する", 4097, Reason.Values[16]),
            new Inflection("される", "す", 513, Reason.Values[20]),
            new Inflection("さない", "す", 516, Reason.Values[15]),
            new Inflection("させる", "る", 2305, Reason.Values[9]),
            new Inflection("させる", "する", 4097, Reason.Values[9]),
            new Inflection("これる", "くる", 2049, Reason.Values[19]),
            new Inflection("こよう", "くる", 2176, Reason.Values[18]),
            new Inflection("こない", "くる", 2052, Reason.Values[15]),
            new Inflection("ければ", "い", 1152, Reason.Values[17]),
            new Inflection("くない", "い", 1028, Reason.Values[15]),
            new Inflection("ぎます", "ぐ", 640, Reason.Values[13]),
            new Inflection("きます", "くる", 2176, Reason.Values[13]),
            new Inflection("きます", "く", 640, Reason.Values[13]),
            new Inflection("きたり", "くる", 2176, Reason.Values[8]),
            new Inflection("きたら", "くる", 2176, Reason.Values[7]),
            new Inflection("ぎたい", "ぐ", 516, Reason.Values[12]),
            new Inflection("きたい", "くる", 2052, Reason.Values[12]),
            new Inflection("きたい", "く", 516, Reason.Values[12]),
            new Inflection("ぎそう", "ぐ", 640, Reason.Values[11]),
            new Inflection("きそう", "くる", 2176, Reason.Values[11]),
            new Inflection("きそう", "く", 640, Reason.Values[11]),
            new Inflection("がれる", "ぐ", 513, Reason.Values[16]),
            new Inflection("かれる", "く", 513, Reason.Values[16]),
            new Inflection("がない", "ぐ", 516, Reason.Values[15]),
            new Inflection("かない", "く", 516, Reason.Values[15]),
            new Inflection("かった", "い", 1152, Reason.Values[14]),
            new Inflection("がせる", "ぐ", 513, Reason.Values[9]),
            new Inflection("かせる", "く", 513, Reason.Values[9]),
            new Inflection("います", "う", 640, Reason.Values[13]),
            new Inflection("いだり", "ぐ", 640, Reason.Values[8]),
            new Inflection("いたり", "く", 640, Reason.Values[8]),
            new Inflection("いだら", "ぐ", 640, Reason.Values[7]),
            new Inflection("いたら", "く", 640, Reason.Values[7]),
            new Inflection("いたい", "う", 516, Reason.Values[12]),
            new Inflection("いそう", "う", 640, Reason.Values[11]),
            new Inflection("んじゃう", "む", 514, Reason.Values[3]),
            new Inflection("んじゃう", "ぶ", 514, Reason.Values[3]),
            new Inflection("んじゃう", "ぬ", 514, Reason.Values[3]),
            new Inflection("りません", "る", 640, Reason.Values[1]),
            new Inflection("りました", "る", 640, Reason.Values[6]),
            new Inflection("りなさい", "る", 640, Reason.Values[5]),
            new Inflection("りすぎる", "る", 513, Reason.Values[4]),
            new Inflection("みません", "む", 640, Reason.Values[1]),
            new Inflection("みました", "む", 640, Reason.Values[6]),
            new Inflection("みなさい", "む", 640, Reason.Values[5]),
            new Inflection("みすぎる", "む", 513, Reason.Values[4]),
            new Inflection("ましょう", "る", 2432, Reason.Values[2]),
            new Inflection("びません", "ぶ", 640, Reason.Values[1]),
            new Inflection("びました", "ぶ", 640, Reason.Values[6]),
            new Inflection("びなさい", "ぶ", 640, Reason.Values[5]),
            new Inflection("びすぎる", "ぶ", 513, Reason.Values[4]),
            new Inflection("にません", "ぬ", 640, Reason.Values[1]),
            new Inflection("にました", "ぬ", 640, Reason.Values[6]),
            new Inflection("になさい", "ぬ", 640, Reason.Values[5]),
            new Inflection("にすぎる", "ぬ", 513, Reason.Values[4]),
            new Inflection("っちゃう", "る", 514, Reason.Values[3]),
            new Inflection("っちゃう", "つ", 514, Reason.Values[3]),
            new Inflection("っちゃう", "く", 514, Reason.Values[3]),
            new Inflection("っちゃう", "う", 514, Reason.Values[3]),
            new Inflection("ちません", "つ", 640, Reason.Values[1]),
            new Inflection("ちました", "つ", 640, Reason.Values[6]),
            new Inflection("ちなさい", "つ", 640, Reason.Values[5]),
            new Inflection("ちすぎる", "つ", 513, Reason.Values[4]),
            new Inflection("しません", "する", 4224, Reason.Values[1]),
            new Inflection("しません", "す", 640, Reason.Values[1]),
            new Inflection("しました", "する", 4224, Reason.Values[6]),
            new Inflection("しました", "す", 640, Reason.Values[6]),
            new Inflection("しなさい", "する", 4224, Reason.Values[5]),
            new Inflection("しなさい", "す", 640, Reason.Values[5]),
            new Inflection("しちゃう", "する", 4098, Reason.Values[3]),
            new Inflection("しちゃう", "す", 514, Reason.Values[3]),
            new Inflection("しすぎる", "する", 4097, Reason.Values[4]),
            new Inflection("しすぎる", "す", 513, Reason.Values[4]),
            new Inflection("こられる", "くる", 2049, Reason.Values[10]),
            new Inflection("こさせる", "くる", 2049, Reason.Values[9]),
            new Inflection("ぎません", "ぐ", 640, Reason.Values[1]),
            new Inflection("きません", "くる", 2176, Reason.Values[1]),
            new Inflection("きません", "く", 640, Reason.Values[1]),
            new Inflection("ぎました", "ぐ", 640, Reason.Values[6]),
            new Inflection("きました", "くる", 2176, Reason.Values[6]),
            new Inflection("きました", "く", 640, Reason.Values[6]),
            new Inflection("ぎなさい", "ぐ", 640, Reason.Values[5]),
            new Inflection("きなさい", "くる", 2176, Reason.Values[5]),
            new Inflection("きなさい", "く", 640, Reason.Values[5]),
            new Inflection("きちゃう", "くる", 2050, Reason.Values[3]),
            new Inflection("ぎすぎる", "ぐ", 513, Reason.Values[4]),
            new Inflection("きすぎる", "くる", 2049, Reason.Values[4]),
            new Inflection("きすぎる", "く", 513, Reason.Values[4]),
            new Inflection("かったり", "い", 1152, Reason.Values[8]),
            new Inflection("かったら", "い", 1152, Reason.Values[7]),
            new Inflection("いません", "う", 640, Reason.Values[1]),
            new Inflection("いました", "う", 640, Reason.Values[6]),
            new Inflection("いなさい", "う", 640, Reason.Values[5]),
            new Inflection("いちゃう", "く", 514, Reason.Values[3]),
            new Inflection("いすぎる", "う", 513, Reason.Values[4]),
            new Inflection("いじゃう", "ぐ", 514, Reason.Values[3]),
            new Inflection("りましょう", "る", 640, Reason.Values[2]),
            new Inflection("みましょう", "む", 640, Reason.Values[2]),
            new Inflection("びましょう", "ぶ", 640, Reason.Values[2]),
            new Inflection("にましょう", "ぬ", 640, Reason.Values[2]),
            new Inflection("ちましょう", "つ", 640, Reason.Values[2]),
            new Inflection("しましょう", "する", 4224, Reason.Values[2]),
            new Inflection("しましょう", "す", 640, Reason.Values[2]),
            new Inflection("ぎましょう", "ぐ", 640, Reason.Values[2]),
            new Inflection("きましょう", "くる", 2176, Reason.Values[2]),
            new Inflection("きましょう", "く", 640, Reason.Values[2]),
            new Inflection("いましょう", "う", 640, Reason.Values[2]),
            new Inflection("ませんでした", "る", 2432, Reason.Values[0]),
            new Inflection("くありません", "い", 1152, Reason.Values[1]),
            new Inflection("りませんでした", "る", 640, Reason.Values[0]),
            new Inflection("みませんでした", "む", 640, Reason.Values[0]),
            new Inflection("びませんでした", "ぶ", 640, Reason.Values[0]),
            new Inflection("にませんでした", "ぬ", 640, Reason.Values[0]),
            new Inflection("ちませんでした", "つ", 640, Reason.Values[0]),
            new Inflection("しませんでした", "する", 4224, Reason.Values[0]),
            new Inflection("しませんでした", "す", 640, Reason.Values[0]),
            new Inflection("ぎませんでした", "ぐ", 640, Reason.Values[0]),
            new Inflection("きませんでした", "くる", 2176, Reason.Values[0]),
            new Inflection("きませんでした", "く", 640, Reason.Values[0]),
            new Inflection("いませんでした", "う", 640, Reason.Values[0]),
            new Inflection("くありませんでした", "い", 1152, Reason.Values[0]),
        });
    }
}