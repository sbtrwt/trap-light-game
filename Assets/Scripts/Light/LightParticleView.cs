using UnityEngine;

namespace TrapLight.Light
{
    public class LightParticleView : MonoBehaviour
    {
        private LightParticleController controller;

        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }
        public void SetController(LightParticleController controller)
        {
            this.controller = controller;
        }
        public void SetRenderer(Sprite spriteToSet) => spriteRenderer.sprite = spriteToSet;

        public void PopLight()
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
