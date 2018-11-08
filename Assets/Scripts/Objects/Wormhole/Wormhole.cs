using Sanmas;
using UnityEngine;

namespace Objects.Wormhole
{
    public class Wormhole : MonoBehaviour
    {
        [SerializeField]
        Transform target;

        public Vector2 WarpPoint()
        {
            return target.position;
        }
    }
}
