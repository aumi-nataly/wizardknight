using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFactory : MonoBehaviour
{
    [SerializeField]
    private List<BonusPoolData> bonusList;

    private Pool poolData;
    public Dictionary<BonusType, Queue<GameObject>> dicPoolBonus = new Dictionary<BonusType, Queue<GameObject>>();


    private void Awake()
    {
        poolData = gameObject.AddComponent<Pool>();

        foreach (BonusPoolData data in bonusList)
        {
            dicPoolBonus.Add(data.bonusType, poolData.CreatePool(data.prefab, data.count));
        }
    }

    public GameObject GetBonusFromPool(BonusType bonusType)
    {
        var curPool = dicPoolBonus[bonusType];
        var obj = poolData.GetFromPool(curPool);
        return obj;
    }

    public void ReturnBonusToPool(BonusType bonusType, GameObject bonus)
    {
        var curPool = dicPoolBonus[bonusType];
        poolData.ReturnToPool(bonus, curPool);
    }
}
