using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    public PlayerController controller;
    public SpriteAnimator spriteAnimator;
    public Transform attackPoint; // Objet enfant placé devant le joueur
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int damage = 1;

    // Détecte l'appui sur la barre d'espace pour attaquer
    void Update()
    {

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartCoroutine(PerformAttack());
        }
    }

    // Effectue une attaque du joueur
    private IEnumerator PerformAttack()
    {
        controller.SetAttacking(true);

        string dir = controller.GetLastDirectionName();
        spriteAnimator.PlayAnimation("Attack" + dir);

        OrientAttackPoint(dir);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        yield return new WaitForSeconds(0.3f);

        controller.SetAttacking(false);
    }

    // Oriente le point d'attaque selon la direction
    private void OrientAttackPoint(string direction)
    {
        switch (direction)
        {
            case "Up":
                attackPoint.localPosition = Vector2.up * attackRange;
                break;
            case "Down":
                attackPoint.localPosition = Vector2.down * attackRange;
                break;
            case "Left":
                attackPoint.localPosition = Vector2.left * attackRange;
                break;
            case "Right":
                attackPoint.localPosition = Vector2.right * attackRange;
                break;
        }
    }

    // Affiche le rayon d'attaque dans l'éditeur
    void OnDrawGizmosSelected()
    {
        if (attackPoint != null) Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}