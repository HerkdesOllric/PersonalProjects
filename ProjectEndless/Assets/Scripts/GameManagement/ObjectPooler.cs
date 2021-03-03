using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Base.Game.Signal;
namespace Main
{
    public class ObjectPooler : MonoBehaviour
    {

        private static ObjectPooler _poolerInstance;

        public static ObjectPooler PoolerInstance
        {
            get { return _poolerInstance; }
        }

        [System.Serializable]
        public class GameObjectPool
        {
            public string SpawnObjectTag;
            public GameObject SpawnObject;
            public int SpawnObjectCount;
            public Transform SpawnLocation;
        }

        public Transform MainSpawn;
        public List<GameObjectPool> PoolsList;
        public Dictionary<string, Queue<GameObject>> PoolsDictionary;

        [System.Serializable]
        public class NpcPool
        {
            public string SpawnObjectTag;
            public NpcScriptable NpcAsset;
            public int NpcCount;
            public Transform NpcsSpawnLocation;
        }

        public List<NpcPool> NpcPoolList;
        public Dictionary<string, Queue<NpcScriptable>> NpcPoolsDictionary;

        private void Awake()
        {
            if (_poolerInstance != null && _poolerInstance != this)
            {
                Destroy(gameObject);
                return;
            }
            _poolerInstance = this;
        }
        private void OnEnable()
        {
            InıtGameobjectPool();
            InitScriptableObjectPool();
        }

        private void Start()
        {

        }

        public void InıtGameobjectPool()
        {
            PoolsDictionary = new Dictionary<string, Queue<GameObject>>();
            MainSpawn = GameObject.FindGameObjectWithTag("Respawn").transform;
            foreach (GameObjectPool pools in PoolsList)
            {
                GameObject toParentObject = new GameObject();
                toParentObject.name = pools.SpawnObjectTag + " Pool";
                toParentObject.transform.position = pools.SpawnLocation.position;
                toParentObject.transform.parent = MainSpawn;
                Queue<GameObject> objectPool = new Queue<GameObject>();
                for (int i = 0; i < pools.SpawnObjectCount; i++)
                {
                    GameObject toSpawnObj = Instantiate(pools.SpawnObject);
                    toSpawnObj.transform.position = pools.SpawnLocation.position;
                    toSpawnObj.transform.parent = toParentObject.transform;
                    toSpawnObj.transform.rotation = Quaternion.identity;
                    StartCoroutine(ObjectSpawnHelper(toSpawnObj));
                    objectPool.Enqueue(toSpawnObj);
                }
                PoolsDictionary.Add(pools.SpawnObjectTag, objectPool);
            }

        }

        public void InitScriptableObjectPool()
        {
            NpcPoolsDictionary = new Dictionary<string, Queue<NpcScriptable>>();
            MainSpawn = GameObject.FindGameObjectWithTag("Respawn").transform;
            foreach(NpcPool npc in NpcPoolList)
            {
                GameObject toParentObj = new GameObject();
                toParentObj.name = npc.NpcAsset.name;
                toParentObj.transform.position = MainSpawn.position;
                Queue<GameObject> spawnedNpcPool = new Queue<GameObject>();
                for (int i = 0; i < npc.NpcCount; i++)
                {
                    GameObject NpcAny = Instantiate(npc.NpcAsset.NpcPrefab);
                    NpcType1 CostumerScript = NpcAny.GetComponent<NpcType1>();
                    if(CostumerScript != null)
                    {
                        CostumerScript.BaseScript = npc.NpcAsset;
                        NpcAny.transform.parent = toParentObj.transform;
                        StartCoroutine(NpcSpawnHelper(NpcAny));
                    }
                    spawnedNpcPool.Enqueue(NpcAny);
                }
                PoolsDictionary.Add(npc.SpawnObjectTag, spawnedNpcPool);
            }
        }

        IEnumerator ObjectSpawnHelper(GameObject obj)
        {
            obj.SetActive(true);
            IPooledObject IPooledObj = obj.GetComponent<IPooledObject>();
            if (IPooledObj != null)
            {
                IPooledObj.OnFirstSpawn();
            }
            yield return new WaitForSeconds(0.2f);
            obj.SetActive(false);
        }

        IEnumerator NpcSpawnHelper(GameObject obj)
        {
            obj.SetActive(true);
            IPooledObject IPooledObj = obj.GetComponent<IPooledObject>();
            if (IPooledObj != null)
            {
                IPooledObj.OnFirstSpawn();
            }
            yield return null;
        }
        public GameObject SpawnObjectFromPool(string ObjPoolTag)
        {
            if (!PoolsDictionary.ContainsKey(ObjPoolTag))
            {
                Debug.Log("Coudn't find object with " + ObjPoolTag + " tag");
                return null;
            }
            GameObject ObjToSpawn = PoolsDictionary[ObjPoolTag].Dequeue();
            ObjToSpawn.SetActive(true);

            PoolsDictionary[ObjPoolTag].Enqueue(ObjToSpawn);

            IPooledObject IpooledObj = ObjToSpawn.GetComponent<IPooledObject>();
            if (IpooledObj != null)
            {
                IpooledObj.OnObjSpawn();
            }
            return ObjToSpawn;
        }

        public GameObject SpawnObjectFromPool(string ObjPoolTag, Vector3 SpawnPos)
        {
            if (!PoolsDictionary.ContainsKey(ObjPoolTag))
            {
                Debug.Log("Coudn't find object with " + ObjPoolTag + " tag");
                return null;
            }
            GameObject ObjToSpawn = PoolsDictionary[ObjPoolTag].Dequeue();
            ObjToSpawn.SetActive(true);
            ObjToSpawn.transform.position = SpawnPos;
            ObjToSpawn.transform.rotation = Quaternion.identity;
            PoolsDictionary[ObjPoolTag].Enqueue(ObjToSpawn);

            IPooledObject IpooledObj = ObjToSpawn.GetComponent<IPooledObject>();
            if (IpooledObj != null)
            {
                IpooledObj.OnObjSpawn();
            }
            return ObjToSpawn;
        }

        public GameObject SpawnObjectFromPool(string ObjPoolTag, Vector3 SpawnPos, Quaternion SpawnRot)
        {
            if (!PoolsDictionary.ContainsKey(ObjPoolTag))
            {
                Debug.Log("Coudn't find object with " + ObjPoolTag + " tag");
                return null;
            }
            GameObject ObjToSpawn = PoolsDictionary[ObjPoolTag].Dequeue();
            ObjToSpawn.SetActive(true);
            ObjToSpawn.transform.position = SpawnPos;
            ObjToSpawn.transform.rotation = SpawnRot;
            PoolsDictionary[ObjPoolTag].Enqueue(ObjToSpawn);

            IPooledObject IpooledObj = ObjToSpawn.GetComponent<IPooledObject>();
            if (IpooledObj != null)
            {
                IpooledObj.OnObjSpawn();
            }
            return ObjToSpawn;
        }

        public void SpawnObjectFromPoolF(string ObjPoolTag)
        {
            if (!PoolsDictionary.ContainsKey(ObjPoolTag))
            {
                Debug.Log("Coudn't find object with " + ObjPoolTag + " tag");
                return;
            }
            GameObject ObjToSpawn = PoolsDictionary[ObjPoolTag].Dequeue();
            ObjToSpawn.SetActive(true);
            //ObjToSpawn.transform.position = Vector3.zero;
            //ObjToSpawn.transform.rotation = Quaternion.identity;
            PoolsDictionary[ObjPoolTag].Enqueue(ObjToSpawn);

            IPooledObject IpooledObj = ObjToSpawn.GetComponent<IPooledObject>();
            if (IpooledObj != null)
            {
                IpooledObj.OnObjSpawn();
            }
        }
    }

}
