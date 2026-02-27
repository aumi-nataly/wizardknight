using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBonus : MonoBehaviour
{
    [SerializeField]
    private BonusFactory bonusFactory;

    [SerializeField]
    private BonusType spawnType;

    [SerializeField]
    private float spawnDistance;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private int SpawnerId;

    private GameObject bonus;

    private Bonus concreteBonus;
    private bool _isCollected;


    private void SpawnBonus()
    {
        if (bonusFactory == null)
        {
            Debug.LogError("bonusFactory не назначена!");
            return;
        }

        if (WorldStateManager.Instance.IsCollectedBonus(SpawnerId))
        { return; }

        bonus = bonusFactory.GetBonusFromPool(spawnType);
        bonus.transform.position = transform.position;

        concreteBonus = bonus.GetComponent<Bonus>();
        _isCollected = false;

    }
    private void DespawnBonus()
    {
        bonusFactory.ReturnBonusToPool(spawnType, bonus);
        bonus = null;
        concreteBonus = null;
    }


    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);

        if (dist < spawnDistance && bonus == null)
        {
            SpawnBonus();
            return;
        }

        if (dist > spawnDistance * 2f && bonus != null)
        {
            DespawnBonus();
            return;
        }

        if (concreteBonus != null && concreteBonus.IsReadyToDespawn())
        {
            UpdateCollected();
            return;
        }
    }

    private void UpdateCollected()
    {
        if (_isCollected) return; // Уже обработан

        _isCollected = true;
        WorldStateManager.Instance.AddBonus(SpawnerId);
        DespawnBonus();
    }
}
