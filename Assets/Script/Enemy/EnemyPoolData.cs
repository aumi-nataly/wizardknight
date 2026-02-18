using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyPoolData", menuName = "Game/Enemy Pool Data")]
public class EnemyPoolData: ScriptableObject
{
    public EnemyType enemyType;
    public GameObject prefab;
    public int count;
}
