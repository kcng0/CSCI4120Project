using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public InventoryItemContoller[] InventoryItems;
    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
        ListItems();
        
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
        ListItems();
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var items in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = items.itemName;
            itemIcon.sprite = items.icon;

            obj.GetComponent<InventoryItemContoller>().AddItem(items);
        }

        
    }

}
