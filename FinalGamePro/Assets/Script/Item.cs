using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public int incread;

    public GameObject spawnObject;
    public string target;
}
