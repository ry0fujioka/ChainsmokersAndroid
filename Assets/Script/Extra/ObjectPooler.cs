using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private bool poolCanExpand = true;

    private List<GameObject> pooledObjects;
    private GameObject parentObject;
    
    private void Start()
    {
        parentObject = new GameObject("Pool");
        Refill();
	}
    
    //新しいプールを作る
    public void Refill()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            AddObjectToPool();
        }
    }

    //プールから現在使えるオブジェクトを獲得する
    public GameObject GetObjectFromPool()
    {
        // 今使われていないプーリングされたオブジェクトを探す
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        // もしオブジェクトが足りない場合、プールが拡張できる設定の場合拡張する
        if (poolCanExpand)
        {
            return AddObjectToPool();
        }

        return null;
    }

    public GameObject AddObjectToPool()
    {
        // 能動的に新しいオブジェクトをプールに追加する
        GameObject newObject = Instantiate(objectPrefab);
        newObject.SetActive(false);
        newObject.transform.parent = parentObject.transform;
        
        pooledObjects.Add(newObject);
        return newObject;
    }     
}