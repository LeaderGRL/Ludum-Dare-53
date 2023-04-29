using UnityEngine;
using System.IO;
using Newtonsoft.Json;


public class JSON
{
    public static Data[] Reader(string path)
    {
        return JsonConvert.DeserializeObject<Data[]>(File.ReadAllText(Application.streamingAssetsPath + path));
    }

    public struct Data
    {
        public string name;
        public string description;
        public int levelRequired;
        public int target;
    }
}