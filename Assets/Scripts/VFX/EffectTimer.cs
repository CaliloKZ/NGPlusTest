using System.Collections;
using Pool;
using UnityEngine;

namespace VFX
{
   public class EffectTimer : MonoBehaviour
   {
      [SerializeField] ParticleSystem effect;
      Coroutine _effectCoroutine;
      WaitForSeconds _waitForEffectDuration;
   
      public void OnEnable()
      {
         _waitForEffectDuration = new WaitForSeconds(effect.main.duration);
         _effectCoroutine = StartCoroutine(EffectCoroutine());
      }

      void OnDisable()
      {
         StopCoroutine(_effectCoroutine);
         _effectCoroutine = null;
      }

      IEnumerator EffectCoroutine()
      {
         yield return _waitForEffectDuration;
         FastPool.Destroy(gameObject);
      }
   }
}
