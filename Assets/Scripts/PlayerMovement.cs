using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public SpriteAnimator spriteAnimator; // Référence au nouveau script

    private Vector2 _movement;
    private Vector2 _lastDirection = Vector2.down; // Regard vers le bas par défaut

    void Update()
    {
        Vector2 rawInput = Vector2.zero;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) rawInput.y += 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) rawInput.y -= 1;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) rawInput.x -= 1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) rawInput.x += 1;
        }

        _movement = rawInput.normalized;
        UpdateAnimationState();
    }

    void UpdateAnimationState()
    {
        string stateName = "";

        if (_movement.magnitude > 0.1f)
        {
            _lastDirection = _movement;
            stateName = "Walk" + GetDirectionName(_movement);
        }
        else
        {
            stateName = "Idle" + GetDirectionName(_lastDirection);
        }

        spriteAnimator.PlayAnimation(stateName);
    }

    // Traduit un Vector2 en nom de direction (Zelda-like)
    string GetDirectionName(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            return dir.x > 0 ? "Right" : "Left";
        }
        else
        {
            return dir.y > 0 ? "Up" : "Down";
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
    }
}