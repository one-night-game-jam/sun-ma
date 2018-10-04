using UnityEngine;

namespace Objects
{
    public interface IGravitationalField
    {
        Vector2 CalcGravity(Vector2 position);
    }
}
