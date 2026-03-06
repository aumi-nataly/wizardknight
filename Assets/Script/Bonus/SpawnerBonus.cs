using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using static UnityEditor.Rendering.ShadowCascadeGUI;

public class SpawnerBonus : MonoBehaviour
{

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



    private WorldStateManager _worldStateManager;
    private AudioManager _audioManager;

    [Inject]
    public void Construct(WorldStateManager worldStateManager, BonusFactory bonusFact, AudioManager audioManager)
    {
        _worldStateManager = worldStateManager;
        bonusFactory = bonusFact;
        _audioManager = audioManager;
    }

   

    private void SpawnBonus()
    {
        if (bonusFactory == null)
        {
            Debug.LogError("bonusFactory не назначена!");
            return;
        }

        if (_worldStateManager.IsCollectedBonus(SpawnerId))
        { return; }

        bonus = bonusFactory.GetBonusFromPool(spawnType);
        bonus.transform.position = transform.position;

        concreteBonus = bonus.GetComponent<Bonus>();
        concreteBonus.Construct(_worldStateManager, _audioManager);

        _isCollected = false;

    }
    private void DespawnBonus()
    {
        bonusFactory.ReturnBonusToPool(spawnType, bonus);
        bonus = null;
        concreteBonus = null;
    }


   public void MainTick()
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
        _worldStateManager.AddBonus(SpawnerId);
        DespawnBonus();
    }
}
