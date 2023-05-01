using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/Resource")]
public class Resource : ScriptableObject
{
    public string type;
    // public string name;
    public int quantity;
    public Sprite icon;
    public int value;
    public bool stackable;
    public string currentLocation;
    public float fuelLevel;
    public int id;
}
