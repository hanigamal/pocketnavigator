using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
namespace MapUtilities
{
    public class SerializableList<TValue>:List<TValue>, IXmlSerializable
    {
        #region IXmlSerializable Members

        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            bool wasEmpty = reader.IsEmptyElement;

            reader.Read();

            if (wasEmpty)
            {
                return;
            }

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {

                
                reader.ReadStartElement("VALUE");

                TValue value = (TValue)valueSerializer.Deserialize(reader);

                reader.ReadEndElement();
                this.Add(value);

               
                reader.MoveToContent();
            }

            reader.ReadEndElement();
        }

        void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TValue value in this)
            {
                
                writer.WriteStartElement("VALUE");

                valueSerializer.Serialize(writer, value);

                writer.WriteEndElement();
                
            }
        }

        #endregion
    }
}
