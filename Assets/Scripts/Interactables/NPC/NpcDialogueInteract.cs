using System;
using Player;
using ScriptableObjects;
using UI;
using UnityEngine;

namespace Interactables.NPC
{
    public class NpcDialogueInteract : MonoBehaviour
    {
        [SerializeField] NPCDialogue_SO dialogueData;
        void Awake()
        {
            PlayerInteractController.OnPlayerInteract += OnPlayerInteract;
        }

        void OnDestroy()
        {
            PlayerInteractController.OnPlayerInteract -= OnPlayerInteract;
        }

        void OnPlayerInteract(int gameObjectID)
        {
            if(gameObject.GetInstanceID() != gameObjectID)
                return;
            
            PlayerInputController.ChangePlayerState(PlayerState.Dialogue);
            NPCDialogueUI.Dialogue(dialogueData);
        }
    }
}