using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;

public class LifeUI : MonoBehaviour
{

    [SerializeField]
    private GameObject flowerLifPrefab;

    [SerializeField]
    private Transform container;

    private List<GameObject> flowers = new List<GameObject>();

    private WorldStateManager _worldStateManager;

    [Inject]
    public void Construct(WorldStateManager worldStateManager)
    {
        _worldStateManager = worldStateManager;
    }

    private void Start()
    {
       
        if (_worldStateManager.IsLoad)
        {
            HandleStarted();
        }
        else
        {

            _worldStateManager.OnLoadedWorldState += HandleStarted;
        }
    }

    private void HandleStarted()
    { 
        UpdateAmountLifeFlowers(_worldStateManager.GetCurrentMaxHealth());
    }

    private void OnDisable()
    {
        _worldStateManager.OnLoadedWorldState -= HandleStarted;
    }

    public void UpdateAmountLifeFlowers(int sum)
    {
        while (flowers.Count < sum)
        {
            var l = Instantiate(flowerLifPrefab, container);
            flowers.Add(l);
        }

        while (flowers.Count > sum)
        {
            var l = flowers[flowers.Count - 1];
            flowers.RemoveAt(flowers.Count - 1);
            Destroy(l);
        }

    }
   
}
