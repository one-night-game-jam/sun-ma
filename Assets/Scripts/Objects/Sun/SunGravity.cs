using UnityEngine;

namespace Objects.Sun
{
    public class SunGravity : MonoBehaviour, IGravitationalField
    {
        [SerializeField]
        float gravity;

        Vector2 IGravitationalField.CalcGravity(Vector2 position)
        {
            var diff = transform.position.XY() - position;
            var sqrMagnitude = Mathf.Max(diff.sqrMagnitude, 0.0001f);
            return gravity / sqrMagnitude * diff.normalized;
        }
    }
}
