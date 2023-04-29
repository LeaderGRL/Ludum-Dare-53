using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public class JSON
{
    public static T  Reader<T>(string path)
    {
        return JsonConvert.DeserializeObject<T>(File.ReadAllText(Application.streamingAssetsPath + path));
    }

    public struct Data
    {
        public string name;
        public string description;
        public int levelRequired;
        public int target;
    }

    public struct Module
    {
        public string name;
        public int level_1;
        public int level_2;
        public int level_3;
    }
}