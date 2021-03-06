using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Game.Signal;
using TMPro;
namespace Main
{
    public class MenuInteractions : MonoBehaviour
    {
        //I was just testing something dont judge me!!

        public GameObject StartBTN;


        public void BTNStartGame()
        {
            NpcGenerator.Instance.SpawnAnyNpcGO(CoreFuncts.NpcType_Ranger);
            StartBTN.SetActive(false);
        }

        public void BTNRestartScene()
        {
            SignalBus<SGLevelChange, int>.Instance.Fire(0);
        }
    }
}

