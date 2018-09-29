using UnityEngine;

namespace Objects.Dishes
{
    public class DishMover : MonoBehaviour
    {
        [SerializeField]
        float speed;

        private void Update()
        {
            var position = transform.position;
            position.x += speed * Time.deltaTime;
            transform.position = position;
        }
    }
}
