namespace AutotestingTrainingSandboxProject
{
    public class LocalizedText
    {
        public Language Language { get; }
        public string Text { get; }

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
