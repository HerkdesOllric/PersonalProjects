using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Base.Game.Signal;
namespace Main
{
    public class NpcType1 : NpcGeneric,IPooledObject
    {

        //Math is all kind of messed up but hey, i am not a mathmatician.. Apperantly im not good at english too...
        //Anyway this is just one kind of npc, i am still working on making him better. He needs more love
        Player player;


        public NpcScriptable BaseScript;
        [HideInInspector]
        public TextMeshPro ClassDisplay;
        [HideInInspector]
        public TextMeshPro LevelDisplay;
        [HideInInspector]
        public TextMeshPro healthDisplay;
        [HideInInspector]
        public TextMeshPro DamageDisplay;


        public Class NpcClass;
        public int NpcLevel;
        public float NpcHealth;
        public float NpcDamage;


        private float _startHealth;
        public Material[] NpcMaterials;
        public void OnFirstSpawn()
        {
            player = FindObjectOfType<Player>();
            NpcClass = BaseScript.NpcClass;
            NpcLevel = BaseScript.NpcLevel;
            NpcHealth = BaseScript.NpcBaseHealth;
            NpcDamage = BaseScript.NpcBaseDamage;
            gameObject.name = BaseScript.NpcName;
            ClassDisplay.text = NpcClass.ToString();
            LevelDisplay.text = NpcLevel.ToString("Level: 0");
            healthDisplay.text = NpcHealth.ToString("Health: 0");
            DamageDisplay.text = NpcDamage.ToString("Damage: 0");
            gameObject.SetActive(false);
        }

        public void OnObjSpawn()
        {
            Player.CurrentEnemy = gameObject;
            SignalBus<SGStageChange, GameState>.Instance.Fire(GameState.ST1);
            foreach(Material mat in NpcMaterials)
            {
                mat.color = Random.ColorHSV();
            }
            NpcMaterials[1].color = NpcMaterials[2].color;
            gameObject.transform.LookAt(player.transform);
            ArttibutesCalculation();
            StartCoroutine(CooldownTimer());
        }

        Color RandomColor()
        {
            Color newColor = new Color();
            newColor.r = Random.Range(0f, 255f);
            newColor.b = Random.Range(0f, 255f);
            newColor.g = Random.Range(0f, 255f);
            newColor.a = 255f;
            return newColor;
        }

        void ArttibutesCalculation()
        {
            NpcClass = (Class)Random.Range(0, 2);
            switch (NpcClass)
            {
                case Class.Ranger:
                    NpcHealth = CalculateHealth(10);
                    NpcDamage = CalculateDamage(50);
                    break;
                case Class.Mage:
                    NpcHealth = CalculateHealth(30);
                    NpcDamage = CalculateDamage(30);
                    break;
                case Class.Tank:
                    NpcHealth = CalculateHealth(50);
                    NpcDamage = CalculateDamage(10);
                    break;
            }
            UpdateStats();
            _startHealth = NpcHealth;
        }

        int CalculateHealth(float distanceHealth)
        {
            float _tempHealth = BaseScript.NpcBaseHealth * NpcLevel;
            float _tempHealthDistance = (distanceHealth) * NpcLevel;
            _tempHealth = Random.Range(_tempHealth - _tempHealthDistance, _tempHealth + _tempHealthDistance);
            return Mathf.Abs(Mathf.RoundToInt(_tempHealth));
        }

        int CalculateDamage(float distanceDamage)
        {
            //float _tempDamage = BaseS.NpcBaseDamage * _level;
            //float _TempDamageDistance = (distanceDamage) * _level;
            //_tempDamage = Random.Range(_tempDamage - _TempDamageDistance, _tempDamage + _TempDamageDistance);

            float _tempDamage = BaseScript.NpcBaseDamage * NpcLevel;
            _tempDamage = (NpcHealth / 10) +_tempDamage;
            return Mathf.Abs(Mathf.RoundToInt(_tempDamage));
        }

        float MaxCooldown = .1f;
        float cooldown;
        bool canTakeDamage;
        public void TakeDamage(float damage)
        {
            if (canTakeDamage)
            {
                NpcHealth -= damage;
                if (NpcHealth > 0)
                {
                    StartCoroutine(CooldownTimer());
                    UpdateStats();
                }
                else
                {
                    StartCoroutine(Die());
                }
            }
        }



        IEnumerator Die()
        {
            canTakeDamage = false;
            NpcHealth = 0;
            UpdateStats();
            SignalBus<SGScoreChange, float, float>.Instance.Fire(_startHealth, 0);
            yield return new WaitForSeconds(1);
            NpcLevel = Mathf.RoundToInt(Mathf.MoveTowards(NpcLevel, 50, 1));
            NpcGenerator.Instance.SpawnAnyNpcGO(CoreFuncts.Pool_Name_NpcType1);
        }

        IEnumerator CooldownTimer()
        {
            canTakeDamage = false;
            cooldown = MaxCooldown;
            while(cooldown > 0)
            {
                cooldown -= 1 * Time.deltaTime;
                yield return null;
            }
            canTakeDamage = true;
        }

        void UpdateStats()
        {
            ClassDisplay.text = NpcClass.ToString();
            LevelDisplay.text = NpcLevel.ToString("Level: 0");
            healthDisplay.text = NpcHealth.ToString("Health: 0");
            DamageDisplay.text = NpcDamage.ToString("Damage: 0");
        }
    }
}

