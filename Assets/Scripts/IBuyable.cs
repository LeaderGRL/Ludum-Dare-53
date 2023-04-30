using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuyable
{
    public int price { get; }
    public void Buy()
    {
        //PlayerManager.instance.LoseMoney(price);
    }
}
