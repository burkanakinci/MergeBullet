using System.Collections.Generic;
using UnityEngine;
using System;
public class ObjectPool : CustomBehaviour
{
    [Serializable]
    public class Pool
    {
        public GameObject PooledPrefab;
        public string PooledTag;
        public int SpawnedSize = 1;
        public DeactiveParents PrefabParent;
    }
    [SerializeField] private List<Pool> m_Pools;
    public Dictionary<string, Queue<IPooledObject>> m_poolDictionary;

    private IPooledObject m_SpawnOnPool;
    private IPooledObject m_TempSpawned;
    public override void Initialize()
    {
        m_poolDictionary = new Dictionary<string, Queue<IPooledObject>>();
        for (int i = 0; i < m_Pools.Count; i++)
        {
            Queue<IPooledObject> m_TempObjectList = new Queue<IPooledObject>();
            for (int j = 0; j < m_Pools[i].SpawnedSize; j++)
            {
                m_TempSpawned = Instantiate(m_Pools[i].PooledPrefab, GameManager.Instance.Entities.GetDeactiveParent(m_Pools[i].PrefabParent)).GetComponent<IPooledObject>();
                m_TempSpawned.GetGameObject().gameObject.SetActive(false);
                m_TempSpawned.GetGameObject().Initialize();
                m_TempSpawned.SetDeactiveParent(m_Pools[i].PrefabParent);
                m_TempSpawned.SetPooledObjectTag(m_Pools[i].PooledTag);
                m_TempObjectList.Enqueue(m_TempSpawned);
            }
            m_poolDictionary.Add(m_Pools[i].PooledTag, m_TempObjectList);
        }
    }
    public IPooledObject SpawnFromPool(string _pooledTag, Vector3 _position = new Vector3(), Quaternion _rotation = new Quaternion(), Transform _parent = null)
    {
        if (!m_poolDictionary.ContainsKey(_pooledTag))
        {
            return null;
        }
        if (m_poolDictionary[_pooledTag].Count > 0)
        {
            m_SpawnOnPool = m_poolDictionary[_pooledTag].Dequeue();
        }
        else
        {
            for (int i = 0; i < m_Pools.Count; i++)
            {
                if (m_Pools[i].PooledTag  == _pooledTag)
                {
                    m_SpawnOnPool = Instantiate(m_Pools[i].PooledPrefab).GetComponent<IPooledObject>();
                    m_SpawnOnPool.GetGameObject().Initialize();
                    m_SpawnOnPool.SetDeactiveParent(m_Pools[i].PrefabParent);
                    m_SpawnOnPool.SetPooledObjectTag(m_Pools[i].PooledTag);
                    break;
                }
            }
        }
        if (_position != null)
        {
            m_SpawnOnPool.GetGameObject().transform.position = _position;
        }
        if (_rotation != null)
        {
            m_SpawnOnPool.GetGameObject().transform.rotation = _rotation;
        }

        m_SpawnOnPool.GetGameObject().transform.SetParent(_parent);

        m_SpawnOnPool.GetGameObject().gameObject.SetActive(true);
        m_SpawnOnPool.OnObjectSpawn();
        return m_SpawnOnPool;
    }

    public void AddObjectPool(string _prefabType, IPooledObject _pooledObject)
    {
        if (!m_poolDictionary[_prefabType].Contains(_pooledObject))
            m_poolDictionary[_prefabType].Enqueue(_pooledObject);
    }
}