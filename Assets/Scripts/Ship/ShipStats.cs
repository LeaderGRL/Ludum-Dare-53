using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats
{

    public bool Available;
    public Dictionary<string, Module> modulesDict;


    public ShipStats()
    {
        Available = true;
        JSON.ModuleData[] moduleDatas = JSON.Reader<JSON.ModuleData[]>("/Modules/Modules.json");
        modulesDict = new Dictionary<string, Module>();
        for (int i = 0; i < moduleDatas.Length; i++)
        {
            Module newModule = new Module();
            newModule.Name = moduleDatas[i].name; ;
            newModule.Level = 1;
            newModule.Stat = moduleDatas[i].levels["level_1"];

            modulesDict.Add(moduleDatas[i].name, newModule);
        }
    }

    public void UpgradeModuleLevel(string moduleName)
    {
        JSON.ModuleData[] moduleDatas = JSON.Reader<JSON.ModuleData[]>("/Modules/Modules.json");
        foreach (var item in moduleDatas)
        {
            if (item.name == moduleName)
            {
                Module moduleModif = new Module();
                moduleModif.Name = item.name;
                moduleModif.Level = modulesDict[moduleName].Level+1;
                moduleModif.Stat = item.levels["level_"+ moduleModif.Level];

                modulesDict[moduleName] = moduleModif;
            }
        }
    }

    
    public override string ToString()
    {
        string res = "";
        foreach (var item in modulesDict)
        {
            res += item.Value.ToString();
            res += "\n";
        }
        return res;
    }

    public struct Module
    {
        public string Name;
        public int Level;
        public float Stat;

        public override string ToString()
        {
            return "Name: " + Name + ", Level: " + Level + ", Stat: "+ Stat;
        }
    }

}
