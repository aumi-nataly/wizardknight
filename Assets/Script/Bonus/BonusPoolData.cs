using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BonusPoolData", menuName = "Game/Bonus Pool Data")]
public class BonusPoolData : ScriptableObject
{
    public BonusType bonusType;
    public GameObject prefab;
    public int count;
}
