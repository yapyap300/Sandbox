using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipType { Head,Body,Hand,Foot,Passive}
[CreateAssetMenu(menuName = "SO/Item/Equip")]
public class Equipment : Item
{
    public EquipType equipType;
    //장비나 패시브가 영향을 미치는 스탯 4개
    public int modHp;
    public int modArmor;
    public float modMspeed;
    public float modASpeed;

    public override void Make()
    {
        base.Make();
        StatusManager.Instance.ChangeStatus(this);
    }
}
