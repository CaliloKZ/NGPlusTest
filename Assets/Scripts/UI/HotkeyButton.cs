using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class HotkeyButton : Selectable, IPointerClickHandler, ISubmitHandler
    {
        [SerializeField] InputActionReference assignedHotkeyButton;
        [SerializeField] TMP_Text hotkeyLabel;
        [SerializeField] Image imageComponent;
        
        public UnityEvent OnClick;
        
        Coroutine _resetRoutine;
        WaitForSeconds _waitTimeFadeDuration;
        
        private void Reset()
        {
            targetGraphic = imageComponent;
        }

        protected override void Start()
        {
            base.Start();
            
            _waitTimeFadeDuration = new WaitForSeconds(colors.fadeDuration);
            assignedHotkeyButton.action.performed += HotkeyClicked;
            
            if(null != hotkeyLabel)
                hotkeyLabel.SetText(GetAssignedButton());
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            assignedHotkeyButton.action.performed -= HotkeyClicked;
        }
        
        private string GetAssignedButton()
        {
            if (assignedHotkeyButton != null && assignedHotkeyButton.action != null)
            {
                InputAction action = assignedHotkeyButton.action;
                foreach (InputBinding binding in action.bindings)
                {
                    if (binding.isPartOfComposite || binding.isComposite) 
                        continue;
                    
                    return InputControlPath.ToHumanReadableString(
                        binding.effectivePath,
                        InputControlPath.HumanReadableStringOptions.OmitDevice);
                    
                }
            }
            return string.Empty;
        }
        
        private void Clicked()
        {
            OnClick?.Invoke();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked();
        }
        
        public void OnSubmit(BaseEventData eventData)
        {
            DoStateTransition(SelectionState.Pressed, true);
            
            Clicked();
            
            if (_resetRoutine != null)
                StopCoroutine(OnFinishSubmit());
            
            _resetRoutine = StartCoroutine(OnFinishSubmit());
        }

        void HotkeyClicked(InputAction.CallbackContext obj)
        {
            DoStateTransition(SelectionState.Pressed, true);
            
            Clicked();
            
            if (_resetRoutine != null)
                StopCoroutine(OnFinishSubmit());
           
            _resetRoutine = StartCoroutine(OnFinishSubmit());
        }
        
        IEnumerator OnFinishSubmit()
        {
            yield return _waitTimeFadeDuration;
            DoStateTransition(currentSelectionState, false);
        }
    }
}