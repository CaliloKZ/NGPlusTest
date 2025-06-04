using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NPCDialogue_SO", menuName = "Scriptable Objects/NPCDialogue_SO")]
    public class NPCDialogue_SO : ScriptableObject
    {

        public string npcName;
        public Sprite npcPortrait;
        public string[] dialogueLines;
        public float typingSpeed = 0.05f;
    }
}
