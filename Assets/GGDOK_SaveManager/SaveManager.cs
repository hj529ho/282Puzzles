using System;
using UnityEngine;
using System.IO;


namespace GGDok.SaveManager
{
    public class SaveManager
    {
        private Serializer _serializer = new Serializer();
        private string filePath;
        public void Init()
        {
            filePath =  $"C:/Users/{Environment.UserName}/{Application.productName}";
            var folder = Directory.CreateDirectory(filePath);
        }
        
        public void Save(object obj)
        {
            FileStream test = new FileStream($"{filePath}/{obj.GetType().Name}.ggdok", FileMode.Create);
            StreamWriter writer = new StreamWriter(test);
            writer.Write(_serializer.Serialize(obj));
            writer.Close();
        }
        
        public object Load(Type type)
        {
            string path = $"{filePath}/{type.Name}.ggdok";
            if (!CheckSaveData(type))
            {
                return null;
            }
            string text = File.ReadAllText(path);
            return _serializer.DeSerialize(text);
        }

        public bool CheckSaveData(Type type)
        {
            // string path = filePath + $"{type.Name}.ggdok";
            return File.Exists(filePath + $"/{type.Name}.ggdok");
        }
    }
}