using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Main
{
    [CreateAssetMenu(fileName = "NpcScriptableObject", menuName = "NPC Assets/GenericNpc")]
    public class NpcScriptable : ScriptableObject
    {

        //Hi all! i see that you arrived here! wow, i hope you like it, to me its a bit shocking tho.. this codes are a bit.. weird!
        public GameObject NpcPrefab;
        public string NpcName;
        [Range(1, 50)]
        public int NpcLevel;
        [Range(10, 50)]
        public float NpcBaseHealth;
        [Range(5,15)]
        public float NpcBaseDamage;


        public Class NpcClass;

        [Range(5, 50)]
        public float HealthDistance;
        [Range(5, 50)]
        public float DamageDistance;

    }
}

