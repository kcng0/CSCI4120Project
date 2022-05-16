using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite icon;
    public ItemType itemType;
    public enum ItemType
    {
        dagger,
        potion,
        book,
        key_1, key_2, key_3, key_4, key_5, key_6, key_7,
        syringe

    }
}
