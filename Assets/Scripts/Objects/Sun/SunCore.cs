using UnityEngine;
using Sanmas;

namespace Objects.Sun
{
    public class SunCore : MonoBehaviour, ISanmaStayHandler, ISanmaExitHandler
    {
        [SerializeField]
        private float gravity;

        public void OnStay(SanmaCore sanmaCore)
        {
            sanmaCore.UpdateGravity((transform.position - sanmaCore.transform.position) * gravity);
        }

        public void OnExit(SanmaCore sanmaCore)
        {
            sanmaCore.UpdateGravity(Vector2.zero);
        }
    }
}
