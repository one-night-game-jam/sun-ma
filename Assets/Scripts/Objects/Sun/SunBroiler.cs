using UnityEngine;

namespace Objects.Sun
{
    public class SunBroiler : MonoBehaviour, IBroiler
    {
        [SerializeField]
        float heatingPower;

        [SerializeField]
        float pow;

        float IBroiler.CalcPower(Vector2 position)
        {
            var distance = Mathf.Max(Vector2.Distance(transform.position, position), 0.0001f);
            return heatingPower / Mathf.Pow(distance, pow);
        }
    }
}
