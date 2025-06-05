using System.Collections;
using UnityEngine;

namespace Effects
{
   public class BounceEffect : MonoBehaviour
   {
      [SerializeField] float bounceHeight = 0.3f;
      [SerializeField] float bounceDuration = 0.3f;
      [SerializeField] float timeOffset = 0.5f;
      [SerializeField] float parabolaFactor = 4f;
   
      [SerializeField] int bounceCount = 2;

      Coroutine _bounceCoroutine;
   
      private void OnEnable()
      {
         StartBounce();
      }

      void StartBounce()
      {
         if(null != _bounceCoroutine)
            StopCoroutine(_bounceCoroutine);

         _bounceCoroutine = StartCoroutine(BounceHandler());
      }

      IEnumerator BounceHandler()
      {
         Vector3 startPosition = transform.position;

         for (int i = 0; i < bounceCount; i++)
         {
            yield return Bounce(startPosition, bounceHeight, bounceDuration / bounceCount);
         }

         transform.position = startPosition;
         _bounceCoroutine = null;
      }

      IEnumerator Bounce(Vector3 start, float height, float duration)
      {
         Vector3 peak = start + Vector3.up * height;
         float elapsedTime = 0f;
      
         while (elapsedTime < duration)
         {
            float time = elapsedTime / duration;
            float yPosition = Mathf.Lerp(start.y, peak.y, time) - Mathf.Pow(time - timeOffset, 2) * height * parabolaFactor;
            transform.position = new Vector3(start.x, yPosition, start.z);
            elapsedTime += Time.deltaTime;
            yield return null;
         }
      }
   }
}
