using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OneForAll.Core.Utility
{
    /// <summary>
    /// 帮助类：序列化/反序列化
    /// </summary>
   public static class SerializationHelper
    {
        #region Binary 序列化

        /// <summary>
        /// Binary 序列化对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>数组</returns>
        public static byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;

            using var memoryStream = new MemoryStream();
            DataContractSerializer ser = new DataContractSerializer(typeof(object));
            ser.WriteObject(memoryStream, obj);
            var data = memoryStream.ToArray();
            return data;
        }

        /// <summary>
        /// Binary 反序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="data">对象数组</param>
        /// <returns>对象</returns>
        public static T Deserialize<T>(byte[] data)
        {
            if (data == null)
                return default(T);

            using var memoryStream = new MemoryStream(data);
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(memoryStream, new XmlDictionaryReaderQuotas());
            DataContractSerializer ser = new DataContractSerializer(typeof(T));
            var result = (T)ser.ReadObject(reader, true);
            return result;
        }

        #endregion

        #region JSON 序列化

        /// <summary>
        /// 序列化对象为JSON格式
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="dateFormat">日期序列化格式</param>
        /// <param name="ignoreNull">是否忽略NULL值</param>
        /// <returns>Json字符串</returns>
        public static string SerializeToJson(object obj, string dateFormat = "yyyy-MM-dd HH:mm:ss", bool ignoreNull = false)
        {

            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                DateFormatString = dateFormat,
                NullValueHandling = ignoreNull ? NullValueHandling.Ignore : NullValueHandling.Include
            });
        }

        /// <summary>
        /// 反序列化JSON对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>json对象</returns>
        public static T DeserializeFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        #endregion

        #region Xml序列化

        private static void SerializeXmlInternal(Stream stream, object obj, Encoding encoding)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            var serializer = new XmlSerializer(obj.GetType());
            var settings = new XmlWriterSettings
            {
                Indent = true,
                NewLineChars = "\r\n",
                Encoding = encoding,
                IndentChars = "    "
            };
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, obj);
                writer.Close();
            }
        }

        /// <summary>
        /// 将一个对象序列化为XML字符串
        /// </summary>
        public static string SerializeXml(object obj, Encoding encoding)
        {
            using (var stream = new MemoryStream())
            {
                SerializeXmlInternal(stream, obj, encoding);
                stream.Position = 0;
                using (var reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 将一个对象按XML序列化的方式写入到一个文件
        /// </summary>
        public static void SerializeXmlToFile(object obj, string path, Encoding encoding)
        {
            if (!string.IsNullOrEmpty(path))
            {
                using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    SerializeXmlInternal(file, obj, encoding);
                }
            }
        }

        /// <summary>
        /// 从XML字符串中反序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="xmlString">xml字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>对象</returns>
        public static T DeserializeXml<T>(string xmlString, Encoding encoding) where T : new()
        {
            try
            {
                using (StringReader sr = new StringReader(xmlString))
                {
                    var xmldes = new XmlSerializer(typeof(T));
                    return (T)xmldes.Deserialize(sr);
                }
            }
            catch
            {
                return new T();
            }
        }
        /// <summary>
        /// 读入一个文件，并按XML的方式反序列化对象。
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="path">保存路径</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>对象值</returns>
        public static T DeserializeXmlFromFile<T>(string path, Encoding encoding) where T : new()
        {
            var xml = string.Empty;
            if (File.Exists(path))
            {
                xml = File.ReadAllText(path, encoding);
            }
            return DeserializeXml<T>(xml, encoding);
        }

        #endregion
    }
}
