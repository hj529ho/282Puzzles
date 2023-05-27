using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace GGDok.SaveManager
{
    public class Serializer
    {
        public string Serialize(object obj)
        {
            Type type = obj.GetType();
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            string a = type.Name + "\n";
            for (int i = 0; i < fields.Length; i++)
            {
                if(fields[i].GetValue(obj).ToString() == typeof(List<bool>).ToString())
                {
                    
                    a += "\t" + fields[i].Name + " : ";
                    foreach (bool isget in (List<bool>)fields[i].GetValue(obj))
                    {
                        a+= $"{isget},";
                    }

                    a = a.Substring(0, a.Length - 1);
                    a += "\n";
                }
                else
                {
                    a += "\t" + fields[i].Name + " : " + fields[i].GetValue(obj) + "\n";
                }
            }
            return a;
        }
        public object DeSerialize(string data)
        {
            string[] splitData = data.Split('\n');
            object obj;
            Type type;
            FieldInfo[] fields;
            List<string[]> member = new List<string[]>();

            type = Type.GetType(splitData[0]);
            Debug.Log(type.Name);
            obj = Activator.CreateInstance(type);
            fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (string a in splitData)
            {
                if (a.Contains("\t"))
                {
                    string[] splitStr = { "\n", "\t", " : " };
                    string[] split = a.Split(splitStr, 2, StringSplitOptions.RemoveEmptyEntries);
                    member.Add(split);
                }
            }
            for (int i = 0; i < fields.Length; i++)
            {
                foreach (string[] keyvalue in member)
                {
                    if (fields[i].FieldType == typeof(int))
                    {
                        if (fields[i].Name == keyvalue[0])
                        {
                            fields[i].SetValue(obj, int.Parse(keyvalue[1]));
                        }
                    }
                    else if (fields[i].FieldType == typeof(bool))
                    {
                        if (fields[i].Name == keyvalue[0])
                        {
                            fields[i].SetValue(obj,bool.Parse(keyvalue[1]));
                        }
                    }
                    else if (fields[i].FieldType == typeof(string))
                    {
                        if (fields[i].Name == keyvalue[0])
                        {
                            fields[i].SetValue(obj, keyvalue[1]);
                        }
                    }
                    else if (fields[i].FieldType == typeof(List<bool>))
                    {
                        if (fields[i].Name == keyvalue[0])
                        {
                            string[] boolsStrings = keyvalue[1].Split(',');
                            List<bool> bools = new List<bool>();
                            foreach (string a in boolsStrings)
                            {
                                bools.Add(bool.Parse(a));
                            }
                            fields[i].SetValue(obj, bools);
                        }
                    }
                }
            }

            return obj;
        }
    }
}
