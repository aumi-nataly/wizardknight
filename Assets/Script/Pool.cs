using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public class Pool : MonoBehaviour
{

    public Queue<GameObject> CreatePool(GameObject prefab, int size, bool fire = false)
    {
        Queue<GameObject> pool = new Queue<GameObject>();

        for (int i = 0; i < size; i++) 
        {
            var obj = Instantiate<GameObject>(prefab);
            if (fire)
            {
                LifetimeScope.Find<GameLifetimeScope>().Container.InjectGameObject(obj);
            }
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }    
        
        return pool;
    }


    public GameObject GetFromPool(Queue<GameObject> pool) 
    { 
        var obj = pool.Dequeue();

        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj, Queue<GameObject> pool)   
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj); 
    }
}
