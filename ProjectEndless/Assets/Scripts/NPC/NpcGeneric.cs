using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Main
{
    public enum Class { Ranger, Mage, Tank}
    public class NpcGeneric : MonoBehaviour
    {
        //Just a generic class for our definetly non-bossy looking npc!

        public NpcScriptable BaseScript;

        public Player player;

        public GameObject DisplayHolder;

        public TextMeshPro ClassDisplay;
        public TextMeshPro LevelDisplay;
        public TextMeshPro healthDisplay;
        public TextMeshPro DamageDisplay;


        public string NpcName;
        public int NpcLevel;
        public float NpcMaxHealth;
        public float NpcCurrentHealth;
        public float NpcDamage;
        public Class NpcClass;

        public float HealthDistance;
        public float DamageDistance;

        public bool Invulnerability;


        public virtual void NpcFirstSetup()
        {
            player = FindObjectOfType<Player>();

            NpcClass = BaseScript.NpcClass;
            NpcLevel = BaseScript.NpcLevel;
            NpcMaxHealth = BaseScript.NpcBaseHealth;
            NpcDamage = BaseScript.NpcBaseDamage;

            ClassDisplay = DisplayHolder.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
            LevelDisplay = DisplayHolder.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();
            healthDisplay = DisplayHolder.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>();
            DamageDisplay = DisplayHolder.transform.GetChild(3).gameObject.GetComponent<TextMeshPro>();

            gameObject.name = BaseScript.NpcName;
            ClassDisplay.text = NpcClass.ToString();
            LevelDisplay.text = NpcLevel.ToString("Level: 0");
            healthDisplay.text = NpcMaxHealth.ToString("Health: 0");
            DamageDisplay.text = NpcDamage.ToString("Damage: 0");
            gameObject.SetActive(false);
        }


        public virtual void AttributeCalculation(Class npcClass)
        {
            NpcMaxHealth = BaseScript.NpcBaseHealth * (2 ^ (NpcLevel) * 10);
            NpcCurrentHealth = NpcMaxHealth;
            NpcDamage = BaseScript.NpcBaseDamage * (2 ^ (NpcLevel) * 10);
            UpdateDisplays();
            //Set base stats for npcAccording to its class
            //baseStats = new float[] { NpcMaxHealth, NpcDamage };
            //float[] X = new float[] { (baseStats[0] + 5) * NpcLevel, (baseStats[1] + 5) * NpcLevel };
            //baseStats = X;
            //NpcCurrentHealth = X[0];
            //NpcDamage = X[1];
        }

        public virtual void UpdateDisplays()
        {
            ClassDisplay.text = NpcClass.ToString();
            LevelDisplay.text = NpcLevel.ToString("Level: 0");
            healthDisplay.text = NpcCurrentHealth.ToString("Health: 0");
            DamageDisplay.text = NpcDamage.ToString("Damage: 0");
        }



        public float CalculateHealth(float any)
        {
            return 1f;
        }

        public float CalculateDamage(float any)
        {
            return 2f;
        }

        public void CalculateStats()
        {

        }
    }
}

