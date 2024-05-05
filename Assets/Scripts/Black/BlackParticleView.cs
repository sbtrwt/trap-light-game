using UnityEngine;

namespace TrapLight.Black
{
    public class BlackParticleView : MonoBehaviour
    {
        private BlackParticleController controller;

        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }
        public void SetController(BlackParticleController controller)
        {
            this.controller = controller;
        }

        public void SetRenderer(Sprite spriteToSet) => spriteRenderer.sprite = spriteToSet;
        public void PopBlackParticle()
        {
            animator.enabled = true;
            animator.Play("Pop", 0);
        }
        public void PopAnimationPlayed()
        {
            spriteRenderer.sprite = null;
            gameObject.SetActive(false);
            controller.OnPopAnimationPlayed();
        }
    }
}
