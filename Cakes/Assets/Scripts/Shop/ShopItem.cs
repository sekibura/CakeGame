using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopItem
{
    public string Name = "";
    public bool AlreadyPurchased = false;

    public ShopItem(string name, bool alreadyPurchased)
    {
        Name = name;
        AlreadyPurchased = alreadyPurchased;
    }
}
