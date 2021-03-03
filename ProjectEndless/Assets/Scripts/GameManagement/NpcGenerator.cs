using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Main
{
    public class NpcGenerator : Singleton<NpcGenerator>
    {

        //Works similar to standart object pooler, but works off with scriptableobjects insted of prefabs(but uses the prefabs inside the scriptableobjects)

        public List<ActiveNpcPool> ActiveNpcPoolList;
        public Dictionary<string, Queue<GameObject>> Dic_ActiveNpcPools;
        
        public class ActiveNpcPool
        {
            public string NpcPoolTag;
            public GameObject NpcGameobject;
            public int NpcPoolCount;
            public Transform SpawnPosition;
        }


        //-------------------------------------------------------------------------------------------------------------------

        public List<NpcTypesPool> NpcTypesList;
        public Dictionary<string, Queue<NpcScriptable>> Dic_NpcTypes;

        int SpawnedObjectPools;
        [System.Serializable]
        public class NpcTypesPool
        {
            public string NpcTypesPoolTag;
            public NpcScriptable NpcAsset;
            public int NpcPoolCount;
            public Transform FirstSpawnPosition;
        }


        private void OnEnable()
        {
            
        }

        private void Start()
        {
            InitPools();
        }

        void InitPools()
        {
            Dic_NpcTypes = new Dictionary<string, Queue<NpcScriptable>>();
            Dic_ActiveNpcPools = new Dictionary<string, Queue<GameObject>>();
            foreach(NpcTypesPool npcTypes in NpcTypesList)
            {
                GameObject toParentObject = new GameObject();
                toParentObject.name = npcTypes.NpcAsset.NpcName + " (Pool)";
                Queue<GameObject> spawnedNpcPool = new Queue<GameObject>();
                for (int i = 0; i < npcTypes.NpcPoolCount; i++)
                {
                    GameObject npcAny = Instantiate(npcTypes.NpcAsset.NpcPrefab);
                    NpcType1 npcScript = npcAny.GetComponent<NpcType1>();
                    if (npcScript != null)
                    {
                        npcScript.BaseScript = npcTypes.NpcAsset;
                        npcAny.transform.parent = toParentObject.transform;
                        toParentObject.transform.position = npcTypes.FirstSpawnPosition.position;
                        StartCoroutine(NpcSpawnHelper(npcAny));
                    }
                    spawnedNpcPool.Enqueue(npcAny);
                }
                Dic_ActiveNpcPools.Add(npcTypes.NpcTypesPoolTag, spawnedNpcPool);
            }
        }

        public GameObject SpawnAnyNpcGO(string npcPoolTag)
        {
            if (!Dic_ActiveNpcPools.ContainsKey(npcPoolTag))
            {
                Debug.Log("Coudn't find object with " + npcPoolTag + " tag");
                return null;
            }
            GameObject npcToSpawn = Dic_ActiveNpcPools[npcPoolTag].Dequeue();
            npcToSpawn.SetActive(true);
            Dic_ActiveNpcPools[npcPoolTag].Enqueue(npcToSpawn);
            IPooledObject iPooledObj = npcToSpawn.GetComponent<IPooledObject>();
            if(iPooledObj != null)
            {
                iPooledObj.OnObjSpawn();
            }
            return npcToSpawn;
        }




        IEnumerator NpcSpawnHelper(GameObject obj)
        {
            obj.SetActive(true);
            IPooledObject IPooledObj = obj.GetComponent<IPooledObject>();
            if (IPooledObj != null)
            {
                IPooledObj.OnFirstSpawn();
            }
            obj.SetActive(false);
            yield return null;
        }
    }
}

