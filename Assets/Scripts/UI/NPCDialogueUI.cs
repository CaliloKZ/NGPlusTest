using System;
using System.Collections;
using Player;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class NPCDialogueUI : MonoBehaviour
    {
        static NPCDialogueUI _instance;
        
        [SerializeField] UIController uiController;
        
        [SerializeField] GameObject dialoguePanel;
        [SerializeField] TMP_Text dialogueText;
        [SerializeField] TMP_Text nameText;
        [SerializeField] Image portraitImage;
        
        [SerializeField] InputActionReference nextLineActionReference;

        WaitForSeconds _waitForSecondsTypingSpeed;
        NPCDialogue_SO _dialogueData;
        int _dialogueIndex;
        bool _isTyping;
        
        Coroutine _typeLineCoroutine;

        void Awake()
        {
            if(null != _instance)
                Destroy(_instance);

            _instance = this;
            nextLineActionReference.action.Disable();
        }

        void OnDestroy()
        {
            nextLineActionReference.action.Disable();
            nextLineActionReference.action.performed -= NextLine;
        }

        public static void Dialogue(NPCDialogue_SO dialogueData)
        {
            _instance.StartDialogue(dialogueData);
        }

        void StartDialogue(NPCDialogue_SO dialogueData)
        {
            _dialogueData = dialogueData;
            _dialogueIndex = 0;
        
            nameText.SetText(_dialogueData.npcName);
            portraitImage.sprite = _dialogueData.npcPortrait;
            _waitForSecondsTypingSpeed = new WaitForSeconds(_dialogueData.typingSpeed);
            
            nextLineActionReference.action.performed += NextLine;
            nextLineActionReference.action.Enable();
            uiController.OnDialogUIToggle(true);
            dialoguePanel.SetActive(true);
            
            if(_typeLineCoroutine != null)
                StopCoroutine(_typeLineCoroutine);
            
            _typeLineCoroutine = StartCoroutine(TypeLine());
        }

        void NextLine(InputAction.CallbackContext callbackContext)
        {
            if (_isTyping)
            {
                if(null != _typeLineCoroutine)
                    StopCoroutine(_typeLineCoroutine);
                dialogueText.SetText(_dialogueData.dialogueLines[_dialogueIndex]);
                _isTyping = false;
                return;
            }

            _dialogueIndex++;
            
            if (_dialogueIndex >= _dialogueData.dialogueLines.Length)
            {
                EndDialogue();
                return;
            }

            if(_typeLineCoroutine != null)
                StopCoroutine(_typeLineCoroutine);
            
            _typeLineCoroutine = StartCoroutine(TypeLine());
        }

        IEnumerator TypeLine()
        {
            _isTyping = true;
            dialogueText.text = string.Empty;

            foreach (char letter in _dialogueData.dialogueLines[_dialogueIndex])
            {
                dialogueText.text += letter;
                yield return _waitForSecondsTypingSpeed;
            }
            _isTyping = false;
        }

        public void EndDialogue()
        {
            if(null != _typeLineCoroutine)
                StopCoroutine(_typeLineCoroutine);
            
            dialogueText.SetText(string.Empty);
            uiController.OnDialogUIToggle(false);
            nextLineActionReference.action.Disable();
            dialoguePanel.SetActive(false);
            PlayerInputController.ChangePlayerState(PlayerState.Idle);
        }
    }
}
