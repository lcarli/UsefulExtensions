﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace UsefulExtensions
{
    public static class SerializationExtension
    {
        public static string ToJson<T>(this T obj)
        {
            if (obj != null)
            {
                return JsonConvert.SerializeObject(obj);
            }

            return null;
        }

        public static T FromJson<T>(this string json)
        {
            if (string.IsNullOrEmpty(json)) return default(T);

            return (T)JsonConvert.DeserializeObject(json);
        }

        public static T FromXml<T>(this string xml)
        {
            if (string.IsNullOrEmpty(xml)) return default(T);

            T result;
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            using (StringReader str = new StringReader(xml))
            {
                result = (T)xmlSer.Deserialize(str);
            }

            return result;
        }

        public static string ToXml<T>(this T obj)
        {
            if (obj == null) return null;

            string result = "";
            XmlSerializer xmlSer = new XmlSerializer(obj.GetType());
            using (MemoryStream m = new MemoryStream())
            {
                xmlSer.Serialize(m, obj);
                result = Encoding.UTF8.GetString(m.GetBuffer()).Replace("\0", "");
            }

            return result;
        }

        public static void ToBinary<T>(this T obj, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(sw.BaseStream, obj);
            }
        }

        public static T FromBinary<T>(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    return (T)serializer.Deserialize(sr.BaseStream);
                }
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
