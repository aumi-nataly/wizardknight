using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField]
    private List<EnemyPoolData> enemyList;

    private Pool poolData;
    public Dictionary<EnemyType,Queue<GameObject>> dicPoolEnemies = new Dictionary<EnemyType, Queue<GameObject>>();

    private void Awake()
    {
        poolData = gameObject.AddComponent<Pool>(); 

        foreach (EnemyPoolData data in enemyList)
        {
            dicPoolEnemies.Add(data.enemyType, poolData.CreatePool(data.prefab, data.count));
        }
    }


    public GameObject GetEnemyFromPool(EnemyType enemyType)
    {
        var curPool = dicPoolEnemies[enemyType];
        var obj = poolData.GetFromPool(curPool);
        var propertyEnemy = obj.GetComponent<Enemy>();
        return obj;
    }

    public void ReturnEnemyToPool(EnemyType enemyType, GameObject enemy)
    {
        var curPool = dicPoolEnemies[enemyType];
        poolData.ReturnToPool(enemy,curPool);
    }
}
