namespace AutotestingTrainingSandboxProject
{
    internal class LocalizedText
    {
        public Language Language { get; private set; }
        public string Text { get; private set; }

        public LocalizedText(Language language, string text)
        {
            Language = language;
            Text = text;
        }

        public override string ToString()
        {
            return $"{Language}: {Text}";
        }
    }
}
