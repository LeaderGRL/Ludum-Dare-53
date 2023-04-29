using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

    public struct ModuleData
    {
        public string name;
        public Dictionary<string, int> levels;
    }
}