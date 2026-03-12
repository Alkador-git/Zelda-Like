using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [System.Serializable]
    public struct AnimationData
    {
        public string name;
        public List<Sprite> sprites;
        public float frameRate; // Images par seconde
    }

    [SerializeField] private List<AnimationData> animations;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private AnimationData currentAnimation;
    private int currentFrame;
    private float timer;
    private bool isPlaying = true;

    private void Start()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        // Joue la première animation par défaut (souvent IdleDown)
        if (animations.Count > 0) PlayAnimation(animations[0].name);
    }

    private void Update()
    {
        if (!isPlaying || currentAnimation.sprites == null || currentAnimation.sprites.Count == 0) return;

        timer += Time.deltaTime;
        float timePerFrame = 1f / currentAnimation.frameRate;

        if (timer >= timePerFrame)
        {
            timer -= timePerFrame;
            currentFrame = (currentFrame + 1) % currentAnimation.sprites.Count;
            spriteRenderer.sprite = currentAnimation.sprites[currentFrame];
        }
    }

    public void PlayAnimation(string name)
    {
        // Évite de redémarrer l'animation si elle est déjà en cours
        if (currentAnimation.name == name) return;

        foreach (var anim in animations)
        {
            if (anim.name == name)
            {
                currentAnimation = anim;
                currentFrame = 0;
                timer = 0;
                spriteRenderer.sprite = currentAnimation.sprites[0];
                return;
            }
        }
    }
}