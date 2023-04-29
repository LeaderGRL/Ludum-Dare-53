using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
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
        public float danger;
        public Reward reward;
        public float time;
        
    }

    public struct Reward
    {
        public int xp;
        public int gold;
    }

    public struct ModuleData
    {
        public string name;
        public Dictionary<string, int> levels;
    }
}