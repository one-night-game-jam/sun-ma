using UnityEngine;
using Sanmas;

namespace Objects.Sun
{
    public class SunCore : MonoBehaviour, ISanmaStayHandler, ISanmaExitHandler
    {
        [SerializeField]
        float gravity;

        [SerializeField]
        float firepower;

        [SerializeField]
        float maxDistance;

        public void OnStay(SanmaCore sanmaCore)
        {
            var diff = transform.position - sanmaCore.transform.position;
            var strength = Mathf.Max(maxDistance * maxDistance - diff.sqrMagnitude, 0f);

            sanmaCore.UpdateGravity(diff.normalized * strength * gravity);
            sanmaCore.Broil(strength * firepower * Time.deltaTime);
        }

        public void OnExit(SanmaCore sanmaCore)
        {
            sanmaCore.UpdateGravity(Vector2.zero);
        }
    }
}
