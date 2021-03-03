using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Main
{
    public enum Class { Ranger, Mage, Tank}
    public class NpcGeneric : MonoBehaviour
    {
        //Just a generic class for our definetly non-bossy looking npc!

        public string NpcName;
        public int BaseLevel;
        public float BaseHealth;
        public float BaseDamage;
        public Class BaseClass;

        public float HealthDistance;
        public float DamageDistance;
    }
}

