using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Equip, Use, Material, Tool}
[CreateAssetMenu(menuName = "SO/Item")]
public class Item : ScriptableObject
{
    new public string name;
    public ItemType type;

    public virtual void Make()
    {

    }
    public virtual void Drop()
    {

    }
}
