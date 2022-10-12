using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory
{
    public static Inventory main;

    protected override bool isPlayerInventory() { return true; }

    private void Awake()
    {
        main = this;
    }
}
