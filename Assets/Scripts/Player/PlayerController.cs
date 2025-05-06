using Inventory;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] Vector2 InitialPosition;
        public static PlayerController Instance {get; private set;}
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("Detectado un Player duplicado. Destruyendo el nuevo.");
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            gameObject.transform.position = InitialPosition;
        }

        private void RestartComponents()
        {
            
        }
    }
}