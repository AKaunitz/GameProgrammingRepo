using UnityEngine;

public class Enemy : Character
{
    public override void Act()
    {
        base.Act(); 
        Debug.Log("Enemy attacks");
    }
}