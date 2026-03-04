using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LifeUI : MonoBehaviour
{

    [SerializeField]
    private GameObject flowerLifPrefab;

    [SerializeField]
    private Transform container;

    private List<GameObject> flowers = new List<GameObject>();

    private void Start()
    {
        var world = WorldStateManager.Instance;

        if (world.IsLoad)
        {
            HandleStarted();
        }
        else
        {

            WorldStateManager.Instance.OnLoadedWorldState += HandleStarted;
        }
    }

    private void HandleStarted()
    { 
        UpdateAmountLifeFlowers(WorldStateManager.Instance.GetCurrentMaxHealth());
    }

    private void OnDisable()
    {
        if (WorldStateManager.Instance != null)
            WorldStateManager.Instance.OnLoadedWorldState -= HandleStarted;
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
