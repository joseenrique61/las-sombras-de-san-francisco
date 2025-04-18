using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Closet : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private BoxCollider2D closetCollider;
    private bool canHide = false;
    private bool isHide = false;

    private Transform originalPosition;
    private Transform hidePosition;
    private PlayerControls controls;

    public void Start()
    {
        closetCollider = GetComponent<BoxCollider2D>();
        hidePosition = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            canHide = true;
            originalPosition = collision.transform;
            player = collision.gameObject;
        }
    }

    /*public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            canHide = true;
            originalPosition = collision.transform;
            player = collision.gameObject;
        }
    }*/

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            canHide = false;
        }
    }

    private void Awake()
    {
        controls = new PlayerControls();

        // Cuando se presiona la acción Interact
        controls.Player.HideInteraction.performed += ctx => TryHide();
        controls.Player.NoHideInteraction.performed += ctx => TryNoHide();
    }

    void TryHide()
    {
        if (canHide && player != null)
       {   
            controls.Player.Move.Disable();
            closetCollider.enabled = false;
            player.transform.position = hidePosition.position;
            isHide = true;
            Debug.Log(isHide);
            // Opcional: cambiar estado, animación, desactivar collider, etc.
        }
    }

    void TryNoHide()
    {
        if (isHide && player != null)
        {
            controls.Player.Move.Enable();
            closetCollider.enabled = true;
            player.transform.position = originalPosition.position;
            isHide = false;
            canHide = false;
            Debug.Log(isHide);
            // Opcional: cambiar estado, animación, desactivar collider, etc.
        }
    }
}
