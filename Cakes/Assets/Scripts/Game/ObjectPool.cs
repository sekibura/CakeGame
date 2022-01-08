using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    private List<List<GameObject>> _pools = new List<List<GameObject>>();
    private List<GameObject> pooledObjects;
    private int amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    
    public void InitPools(GameObject[] objectsToPool, int amount)
    {
        amountToPool = amount;
        foreach (var item in objectsToPool)
        {
            pooledObjects = new List<GameObject>();
            GameObject tmp;
            for (int i = 0; i < amount; i++)
            {
                tmp = Instantiate(item);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
            _pools.Add(pooledObjects);
        }
    }

    //private void Start()
    //{
    //    pooledObjects = new List<GameObject>();
    //    GameObject tmp;
    //    for (int i = 0; i < amountToPool; i++)
    //    {
    //        tmp = Instantiate(objectToPool);
    //        tmp.SetActive(false);
    //        pooledObjects.Add(tmp);
    //    }
    //}

    public GameObject GetPooledObject(int index)
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!_pools[index][i].activeInHierarchy)
            {
                return _pools[index][i];
            }
        }
        return null;
    }

    public List<List<GameObject>> GetPools()
    {
        return _pools;
    }
    #region HOW TO USE
    // Use instead instantinate:
    //GameObject bullet = ObjectPool.SharedInstance.GetPooledObject(1);
    //if (bullet != null)
    //{
    //    bullet.transform.position = turret.transform.position;
    //    bullet.transform.rotation = turret.transform.rotation;
    //    bullet.SetActive(true);
    //}

    //bullet.SetActive(false);
    #endregion

}
