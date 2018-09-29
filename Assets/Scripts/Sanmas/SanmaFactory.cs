using UnityEngine;

namespace Sanmas
{
    public class SanmaFactory : MonoBehaviour
    {
        [SerializeField] SanmaCore prefab;

        public SanmaCore Create()
        {
            return Instantiate(prefab, transform.position, Quaternion.identity, transform);
        }
    }
}
