using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Réglages de déplacement")]
    public float moveSpeed = 5f;

    [Header("Composants")]
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 _movement;
    private const float _inputThreshold = 0.1f;

    [Header("Débogage")]
    public bool debugMode = false;

    void Update()
    {
        // Lecture des entrées avec le New Input System
        Vector2 rawInput = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) rawInput.y += 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) rawInput.y -= 1;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) rawInput.x -= 1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) rawInput.x += 1;
        }

        // Normalisation pour éviter d'aller plus vite en diagonale
        _movement = rawInput.normalized;

        if (debugMode)
        {
            Debug.Log($"[Input] Raw: {rawInput} | Normalized: {_movement} | Magnitude: {_movement.magnitude}");
        }

        // Mise à jour de l'Animator
        if (_movement.magnitude > _inputThreshold)
        {
            animator.SetFloat("Horizontal", _movement.x);
            animator.SetFloat("Vertical", _movement.y);
            animator.SetFloat("Speed", 1f);

            animator.SetFloat("LastHorizontal", _movement.x);
            animator.SetFloat("LastVertical", _movement.y);
        }
        else
        {
            animator.SetFloat("Speed", 0f);

            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 0f);
        }
    }

    void FixedUpdate()
    {
        // 4. Application du mouvement physique (recommandé pour Rigidbody2D)
        rb.MovePosition(rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
    }
}