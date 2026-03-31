using UnityEngine;
using System.Collections.Generic;


public class Lists: MonoBehaviour
{
    private List<string> inventory = new List<string>();

    private void Start()
    {

        inventory.Add("Stone");
        inventory.Add("Sword");
        inventory.Add("Wood");

        Debug.Log("----inventory updated----");
        PrintInventory();

        inventory.Remove("Sword");

        Debug.Log("----inventory updated----");
        PrintInventory();

        inventory.Insert(1, "Bow");

        Debug.Log("----inventory updated----");
        PrintInventory();
    }

    private void PrintInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            Debug.Log($"Slot: {inventory[i]}");
        }
    }
}