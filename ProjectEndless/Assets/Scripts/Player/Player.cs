using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Game.Signal;
using System;
namespace Main
{
    public class Player : InputManager
    {
        public static Action PlayerGeneralAction;
        public static Action PlayerHoldAction;

        public static GameObject CurrentEnemy;

        NpcType1 EnemyMainScript;

        private void OnDisable()
        {
            //Events and signals
            SignalBus<SGStageChange, GameState>.Instance.UnRegister(StateChanger);
            PlayerGeneralAction = null;
            CurrentEnemy = null;

        }

        void Start()
        {
            StartSetup();
        }

        private void Update()
        {
            base.TouchControl();
        }

        void StartSetup()
        {
            base.TouchControlStart();
            SignalBus<SGStageChange, GameState>.Instance.Register(StateChanger);
        }

        #region Inputs, etc

        public override void OnSingleTap()
        {
            DamageEnemy(100);
        }

        #endregion

        #region Gameplay Functions

        public void DamageEnemy()
        {
            if (EnemyMainScript != null)
                EnemyMainScript.TakeDamage(15);
        }

        public void DamageEnemy(float damage)
        {
            if(EnemyMainScript != null)
            EnemyMainScript.TakeDamage(damage);
        }

        #endregion

        #region State Control

        void StateChanger(GameState state)
        {
            StartCoroutine(CheckState(state));
        }

        IEnumerator CheckState(GameState state)
        {
            PlayerGeneralAction = null;
            switch (state)
            {
                case GameState.Start:
                    print(state);
                    ChangeInputMap(CoreFuncts.Name_InputMap_MenuInteractions);
                    break;
                case GameState.ST1:
                    print(state);
                    ChangeInputMap(CoreFuncts.Name_InputMap_PlayerTesting);
                    EnemyMainScript = CurrentEnemy.GetComponent<NpcType1>();
                    break;
                case GameState.ST2:
                    print(state);
                    //PlayerGeneralAction += MoveTool;
                    break;
                case GameState.ST3:
                    print(state);
                    break;
                case GameState.ST4:
                    print(state);
                    break;
                case GameState.ST5:
                    print(state);
                    break;
                case GameState.Pause:
                    print(state);
                    break;
                case GameState.End:
                    print(state);
                    break;
            }
            yield return new WaitForFixedUpdate();
        }

        void ChangeInputMap(string mapName)
        {
            return;
        }

        #endregion


        //i was going to do something with this space.. but i forgot what.. meh


        #region Vectors Etc..

        protected Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
            float distance;
            xy.Raycast(ray, out distance);
            return ray.GetPoint(distance);
        }

        #endregion
    }

}
