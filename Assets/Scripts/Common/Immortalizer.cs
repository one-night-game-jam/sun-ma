using UnityEngine;

namespace Common
{
    public class Immortalizer : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}