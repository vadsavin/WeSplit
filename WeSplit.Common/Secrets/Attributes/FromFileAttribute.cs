namespace WeSplit.Common.Secrets.Attributes
{
    public class FromFileAttribute : BaseSourceAttribute
    {
        public string FilePath { get; }

        public FromFileAttribute(string filePath)
        {
            FilePath = filePath;
        }
    }
}
