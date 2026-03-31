using UnityEngine;

public class Arrays : MonoBehaviour
{





    private string[] weaponNames = { "Knife", "Rifle", "Pistol" };
    private int[] weaponDamage = { 15, 40, 25 };




    private void Start()
    {
       
        for (int i = 0; i < weaponNames.Length; i++)
        {
            Debug.Log($"Weapon: {weaponNames[i]}, Damage: {weaponDamage[i]}");
        }
    }
}