namespace AutotestingTrainingSandboxProject
{
    internal class LocalizedText
    {
        public Language Language { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"{Language}: {Text}";
        }
    }
}
