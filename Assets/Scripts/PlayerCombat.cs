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

    void Update()
    {

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        controller.SetAttacking(true);

        // Joue l'animation d'attaque selon la direction (ex: AttackUp)
        string dir = controller.GetLastDirectionName();
        spriteAnimator.PlayAnimation("Attack" + dir);

        // Oriente le point d'attaque vers la direction du joueur
        OrientAttackPoint(dir);

        // Détection des ennemis
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        // Temps d'attente pour laisser l'animation se finir (à ajuster)
        yield return new WaitForSeconds(0.3f);

        controller.SetAttacking(false);
    }

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

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null) Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}