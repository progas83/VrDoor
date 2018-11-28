namespace FileDataOperations
{
    public class XmlConfigurator<T> where T:class, new()
    {
        private static readonly string defaultFileName = "configuration.xml";

        private string fileName;

        private XmlSerializator<T> xmlSerializator;
        public XmlConfigurator() : this(defaultFileName)
        {

        }
        public XmlConfigurator(string fileName)
        {
            this.fileName = fileName;
            this.xmlSerializator = new XmlSerializator<T>();
        }

        public T ReadConfiguration()
        {
            return this.xmlSerializator.Deserialise(fileName);
        }

        public void SaveConfiguratio(T configuration)
        {
            LogManager.Output($"Save configuration to");
            LogManager.Output($"Save configuration to {fileName}");
            this.xmlSerializator.Serialize(configuration, this.fileName);
        }
    }
}
