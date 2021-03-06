using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Base.Game.Signal;
namespace Main
{
    public class NpcType1 : NpcGeneric,IPooledObject,INpcInterface
    {

        //Math is all kind of messed up but hey, i am not a mathmatician.. Apperantly im not good at english too...
        //Anyway this is just one kind of npc, i am still working on making him better. He needs more love

        private float _startHealth;
        public Material[] NpcMaterials;
        public void OnFirstSpawn()
        {
            base.NpcFirstSetup();
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
            base.AttributeCalculation(NpcClass);
            StartCoroutine(CooldownTimer());
        }

        public void OnTakeDamage()
        {

        }

        public void OnTakeDamage(float damage)
        {

        }

        public void OnRespawn()
        {
            Player.CurrentEnemy = gameObject;
            SignalBus<SGStageChange, GameState>.Instance.Fire(GameState.ST1);
            foreach (Material mat in NpcMaterials)
            {
                mat.color = Random.ColorHSV();
            }
            NpcMaterials[1].color = NpcMaterials[2].color;
            gameObject.transform.LookAt(player.transform);
            base.AttributeCalculation(NpcClass);
            StartCoroutine(CooldownTimer());
        }
        //void ArttibutesCalculation()
        //{
        //    NpcClass = (Class)Random.Range(0, 2);
        //    switch (NpcClass)
        //    {
        //        case Class.Ranger:
        //            NpcMaxHealth = CalculateHealth(10);
        //            NpcDamage = CalculateDamage(50);
        //            break;
        //        case Class.Mage:
        //            NpcMaxHealth = CalculateHealth(30);
        //            NpcDamage = CalculateDamage(30);
        //            break;
        //        case Class.Tank:
        //            NpcMaxHealth = CalculateHealth(50);
        //            NpcDamage = CalculateDamage(10);
        //            break;
        //    }
        //    UpdateStats();
        //    _startHealth = NpcMaxHealth;
        //}

        //int CalculateHealth(float distanceHealth)
        //{
        //    //float _tempHealth = BaseScript.NpcBaseHealth * NpcLevel;
        //    //float _tempHealthDistance = (distanceHealth) * NpcLevel;
        //    //_tempHealth = Random.Range(_tempHealth - _tempHealthDistance, _tempHealth + _tempHealthDistance);
        //    //return Mathf.Abs(Mathf.RoundToInt(_tempHealth));
        //    float Max_Stat = BaseScript.NpcBaseHealth * (2 ^ (NpcLevel / 2));
        //    return Mathf.RoundToInt(Max_Stat);
        //}

        //int CalculateDamage(float distanceDamage)
        //{
        //    //float _tempDamage = BaseS.NpcBaseDamage * _level;
        //    //float _TempDamageDistance = (distanceDamage) * _level;
        //    //_tempDamage = Random.Range(_tempDamage - _TempDamageDistance, _tempDamage + _TempDamageDistance);

        //    float _tempDamage = BaseScript.NpcBaseDamage * NpcLevel;
        //    _tempDamage = (NpcHealth / 10) +_tempDamage;
        //    return Mathf.Abs(Mathf.RoundToInt(_tempDamage));
        //}

        float MaxCooldown = .1f;
        float cooldown;
        bool canTakeDamage;

        
        public void TakeDamage(float damage)
        {
            if (canTakeDamage)
            {
                NpcCurrentHealth -= damage;
                base.UpdateDisplays();
                if (NpcCurrentHealth > 0)
                {
                    StartCoroutine(CooldownTimer());
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
            NpcCurrentHealth = 0;
            base.UpdateDisplays();
            SignalBus<SGScoreChange, float, float>.Instance.Fire(_startHealth, 0);
            yield return new WaitForSeconds(1);
            NpcLevel = Mathf.RoundToInt(Mathf.MoveTowards(NpcLevel, 50, 1));
            float probs = Random.value;
            if(probs < .5f)
            {
                NpcGenerator.Instance.RespawnNpcFromPool(this.gameObject);
            }
            else
            {
                string[] newList = new string[] { CoreFuncts.NpcType_Mage, CoreFuncts.NpcType_Ranger, CoreFuncts.NpcType_Tank };
                string X = newList[Random.Range(0, newList.Length)];
                NpcGenerator.Instance.SpawnAnyNpcGO(X);
            }
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

        //void UpdateStats()
        //{
        //    ClassDisplay.text = NpcClass.ToString();
        //    LevelDisplay.text = NpcLevel.ToString("Level: 0");
        //    healthDisplay.text = NpcCurrentHealth.ToString("Health: 0");
        //    DamageDisplay.text = NpcDamage.ToString("Damage: 0");
        //}


        #region UnsuedCode

        Color RandomColor()
        {
            Color newColor = new Color();
            newColor.r = Random.Range(0f, 255f);
            newColor.b = Random.Range(0f, 255f);
            newColor.g = Random.Range(0f, 255f);
            newColor.a = 255f;
            return newColor;
        }


        #endregion

    }
}

