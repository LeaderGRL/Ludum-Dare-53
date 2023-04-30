using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class JSON
{
    public static T Reader<T>(string path)
    {
        return JsonConvert.DeserializeObject<T>(File.ReadAllText(Application.streamingAssetsPath + path));
    }

    public struct Data
    {
        public string name;
        public string description;
        public int levelRequired;
        public float danger;
        public Reward reward;
        public float time;
        public bool inProgress;
        public int id;
        public bool once;
        public bool done;
        public Dictionary<string, int> materials;
        public Dictionary<string, string> destination;
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