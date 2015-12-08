namespace Gangstarz.Managers
{
    using System;
    using System.IO;
    using System.Xml.Serialization;   

    public class XMLManager<T>
    {
        public Type Type { get; set; }

        public XMLManager()
        {
            Type = typeof(T);
        }

        public T Load(string path)
        {
            T instance;

            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                instance = (T)xml.Deserialize(reader);
            }

            return instance;
        }

        public void Save(string path, object obj)
        {
            using (var writer = new StreamWriter(path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                xml.Serialize(writer, obj);
            }
        }
    }
}
