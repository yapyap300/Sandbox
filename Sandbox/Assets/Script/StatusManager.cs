using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    private static StatusManager instance;
    public PlayerStatus myStatus;
    Equipment[] equipment = new Equipment[4];

    public static StatusManager Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ChangeStatus(Equipment newItem)
    {
        int type = (int)newItem.equipType;
        if (equipment[type] != null)
        {
            myStatus.maxHP -= equipment[type].modHp;
            myStatus.armor -= equipment[type].modArmor;
            myStatus.mSpeed -= equipment[type].modMspeed;
            myStatus.aSpeed -= equipment[type].modASpeed;
        }
        myStatus.maxHP += newItem.modHp;
        myStatus.armor += newItem.modArmor;
        myStatus.mSpeed += newItem.modMspeed;
        myStatus.aSpeed += newItem.modASpeed;
    }
}
