using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiManchi
{
    namespace MiEnum
    {
        public enum CanvasLayer
        {
            First = 0,
            Second,
            Third,
            Fourth,
            Fifth,

            System,
            Loading,

            dontDestroy,
        }
        public enum ButtonStstus
        {
            None = 1 << 0,
            Enter = 1 << 1,
            Exit = 1 << 2,
            Down = 1 << 3,
            Up = 1 << 4,
            Long = 1 << 5,
            Click = 1 << 6,
        }
        public enum DialogMode
        {
            stack,
            none,
        }
        public enum CharacterStatus
        {
            None,
            Stand,
            Walk,
            Jump,
            Scrunch,
            Skill_R,
            skill_F,
        }
        public enum Z_EnemyStatus
        {
            None,
            Stand,
            Walk,
            Jump,
            Scrunch,
            Skill_R,
            skill_F,
        }
        public enum ClassificationStatus
        {
            None,
            Character_Z,
            Enemy_Z,
        }
        public enum HarmType
        {
            Noen = 0,
            Add = 1,
            Remove = -1,
        }
        public enum MethodMode
        {
            None = 1 << 0,
            System = 1 << 1,
            Manual = 1 << 2,
        }
        public enum ObjectType 
        {
            None = 1 << 0,
            Character = 1 << 1,
            Enemy = 1 << 2,
            Boos = 1 << 3,
        }

    }
}