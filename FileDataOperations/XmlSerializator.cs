using System.IO;
using System.Xml.Serialization;

namespace FileDataOperations
{
    internal class XmlSerializator<T> where T : class, new()
    {
        private XmlSerializer xmlSerializer;
        public XmlSerializator()
        {
            this.xmlSerializer = new XmlSerializer(typeof(T));
        }
        public T Deserialise(string fileName)
        {
            T result = default(T);
            using (var memStream = new MemoryStream(File.ReadAllBytes(fileName)))
            {
                result = (T)this.xmlSerializer.Deserialize(memStream);
            }
            return result;
        }

        public void Serialize(T data, string fileName)
        {
            using (var memStream = new MemoryStream())
            {
                this.xmlSerializer.Serialize(memStream, data);
                File.WriteAllBytes(fileName, memStream.ToArray());
            }
        }
    }
}
