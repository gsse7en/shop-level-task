using System.Collections;
using System.Collections.Generic;
using UI.ShopItem;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData", menuName = "Items Data", order = 51)]
public class ItemsData : ScriptableObject
{
    [SerializeField]
    public List<ItemData> ItemList;
}
