using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [System.Serializable]
    public struct AnimationData
    {
        public string name;
        public List<Sprite> frames;
        public float fps;
        public bool loop;
    }

    [SerializeField] private List<AnimationData> animations;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private AnimationData _currentAnim;
    private int _frameIndex;
    private float _timer;
    private bool _isDone;

    // Initialise le SpriteRenderer et lance l'animation par défaut
    private void Start()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        PlayAnimation("IdleDown");
    }

    // Gère l'avancement des frames de l'animation
    private void Update()
    {
        if (_isDone || _currentAnim.frames == null || _currentAnim.frames.Count == 0) return;

        _timer += Time.deltaTime;
        float timePerFrame = 1f / _currentAnim.fps;

        if (_timer >= timePerFrame)
        {
            _timer -= timePerFrame;
            _frameIndex++;

            if (_frameIndex >= _currentAnim.frames.Count)
            {
                if (_currentAnim.loop)
                {
                    _frameIndex = 0;
                }
                else
                {
                    _frameIndex = _currentAnim.frames.Count - 1;
                    _isDone = true;
                }
            }
            spriteRenderer.sprite = _currentAnim.frames[_frameIndex];
        }
    }

    // Lance une animation par son nom
    public void PlayAnimation(string animName)
    {
        if (_currentAnim.name == animName) return;

        foreach (var anim in animations)
        {
            if (anim.name == animName)
            {
                _currentAnim = anim;
                _frameIndex = 0;
                _timer = 0;
                _isDone = false;
                spriteRenderer.sprite = _currentAnim.frames[0];
                return;
            }
        }
    }
}