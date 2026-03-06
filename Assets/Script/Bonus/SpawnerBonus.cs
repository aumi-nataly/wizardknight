using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using static UnityEditor.Rendering.ShadowCascadeGUI;

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
    private bool _canWork;

    private WorldStateManager _worldStateManager;

    [Inject]
    public void Construct(WorldStateManager worldStateManager)
    {
        _worldStateManager = worldStateManager;
        Debug.Log("SpawnerBonus: WorldStateManager внедрён! - "+ SpawnerId);
    }

    private void Start()
    {
        _worldStateManager.OnLoadedWorldState += HandleStarted;
    }

    private void OnDestroy()
    {
        _worldStateManager.OnLoadedWorldState -= HandleStarted;
    }

    private void HandleStarted()
    {
        _canWork = true;
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
        if (!_canWork)
            return;

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
