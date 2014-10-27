using System.Collections;
using UnityEngine;

namespace Scripts.Components.SpecialEvents
{
    public class CameraShakeEventController : BaseSpecialEventController
    {
        protected override IEnumerator EnumerateSpecialEvent()
        {
            var initialPosition = transform.position;
            for (var i = 0; i < 10; i++)
            {
                var randomPositionTranslation = new Vector3(Random.Range(-0.3f, 0.3f), 0f, Random.Range(-0.3f, 0.3f));
                transform.Translate(randomPositionTranslation);

                yield return new WaitForSeconds(0.025f);
            }
            transform.position = initialPosition;
        }
    }
}
