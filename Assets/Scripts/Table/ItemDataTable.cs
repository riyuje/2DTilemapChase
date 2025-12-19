using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemDataTable", menuName = "Scriptable Objects/ItemDataTable")]
public class ItemDataTable : ScriptableObject
{
    public List<ItemData> itemDataList = new();
}
