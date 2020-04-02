namespace Screenulate.Tesseract
{
    class VnTextTransform : ITextTransform
    {
        public string Transform(string text)
        {
            return RemoveQuotes(RemoveNewline(text));
        }

        private static string RemoveQuotes(string text)
        {
            char[] quotes = {'「', '」', '"', '\''};
            return text.Trim(quotes).Trim();
        }

        private static string RemoveNewline(string text)
        {
            return text
                .Replace("\r", "")
                .Replace("\n", "")
                .Replace("\t", "")
                .Trim();
        }
    }
}
