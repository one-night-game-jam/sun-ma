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

        public void OnStay(SanmaCore sanmaCore)
        {
            var diff = transform.position - sanmaCore.transform.position;
            sanmaCore.UpdateGravity(diff * gravity);
            sanmaCore.Broil(diff.magnitude * firepower * Time.deltaTime);
        }

        public void OnExit(SanmaCore sanmaCore)
        {
            sanmaCore.UpdateGravity(Vector2.zero);
        }
    }
}
