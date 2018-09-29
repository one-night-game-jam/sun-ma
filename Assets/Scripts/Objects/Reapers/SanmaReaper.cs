using Sanmas;
using UnityEngine;

namespace Objects.Reapers
{
    public class SanmaReaper : MonoBehaviour, ISanmaEnterHandler
    {
        void ISanmaEnterHandler.OnEnter(SanmaCore sanmaCore)
        {
            Destroy(sanmaCore.gameObject);
        }
    }
}
