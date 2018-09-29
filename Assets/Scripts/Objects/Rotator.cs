using UnityEngine;

namespace Objects
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField]
        float rotationSpeed;

        private void Update()
        {
            this.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }
}
