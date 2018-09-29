using UnityEngine;

namespace Common
{
    public class SoundManager : MonoBehaviour
    {
        static SoundManager _instance;

        [SerializeField] AudioSource _audioSource;

        void Start()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
            _audioSource.enabled = true;
        }
    }
}