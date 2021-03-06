using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Game.Signal;
namespace Main
{
    public enum GameState { Start, ST1, ST2, ST3, ST4, ST5, Pause, End }
    public static class CoreFuncts
    {
        //Im just lazy, and.. uh.. i make lots of mistakes why typing so i am storing strings here!


        public const string Name_InputMap_MenuInteractions = "MenuInteractions";
        public const string Name_InputMap_PlayerTesting = "PlayerTesting";

        public const string Pool_Name_NpcType1 = "NpcType1";
        public const string Pool_Name_NpcType2 = "NpcType2";
        public const string Pool_Name_NpcType3 = "NpcType3";
        public const string Pool_Name_NpcType4 = "NpcType4";

        public const string Pool_Name_EnemyType1 = "EnemyType1";
        public const string Pool_Name_EnemyType2 = "EnemyType2";
        public const string Pool_Name_EnemyType3 = "EnemyType3";
        public const string Pool_Name_EnemyType4 = "EnemyType4";

        
        public const string NpcType_Ranger = "NpcRanger";
        public const string NpcType_Mage = "NpcMage";
        public const string NpcType_Tank = "NpcTank";
    }
}

