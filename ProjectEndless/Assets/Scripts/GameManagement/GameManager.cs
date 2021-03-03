using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Game.Signal;
namespace Main
{
    public class GameManager : Singleton<GameManager>
    {
        //We all have our own flaws! Mine is using "GameManager" namespace for non-gamemanager buisness

        public GameState CurrentState;

        ObjectPooler objectPooler;
        public void Awake()
        {
            SignalBus<SGStageChange, GameState>.Instance.Register(ChangeState);
            //objectPooler = ObjectPooler.Instance;
        }

        private void Start()
        {
            SignalBus<SGStageChange, GameState>.Instance.Fire(GameState.Start);
            //objectPooler.SpawnObjectFromPoolF(CoreFuncts.Pool_Name_Costumer1);

        }

        private void OnDisable()
        {
            SignalBus<SGStageChange, GameState>.Instance.UnRegister(ChangeState);
        }

        void ChangeState(GameState AnyState)
        {
            CurrentState = AnyState;
        }
    }
}

