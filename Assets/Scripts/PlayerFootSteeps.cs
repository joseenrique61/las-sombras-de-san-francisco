using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFootSteeps : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios;
    [SerializeField] private float footSteepDelay;
    private float footSteepTimer;
    private Vector2 movement;
    private PlayerInput input;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        footSteepTimer = 0f;
        footSteepDelay = 0.4f;
    }

    void Update()
    {
        movement = input.actions["Move"].ReadValue<Vector2>();

        if (movement.magnitude > 0.1f)
        {
            footSteepTimer -= Time.deltaTime;

            if (footSteepTimer <= 0f)
            {
                Debug.Log("Step");
                SoundsMannager.Instance.PlayRandomSFX(audios);
                //footSteepTimer = Random.Range(0.4f, 0.6f);
                footSteepTimer = footSteepDelay;
            }
        }
        else if (footSteepTimer < footSteepDelay)
        {
            footSteepTimer = footSteepDelay;
        }
    }
}

