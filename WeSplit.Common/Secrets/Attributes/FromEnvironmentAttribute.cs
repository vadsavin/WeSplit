namespace WeSplit.Common.Secrets.Attributes
{
    public class FromEnvironmentAttribute : BaseSourceAttribute
    {
        public string Name { get; }

        public FromEnvironmentAttribute(string name)
        {
            Name = name;
        }
    }
}
