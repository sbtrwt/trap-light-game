using UnityEngine;

namespace TrapLight.Wave.Light
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
        public LightParticleController GetController()
        {
            return controller;
        }
        public void SetRenderer(Sprite spriteToSet) => spriteRenderer.sprite = spriteToSet;

        public void PopLightParticle()
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
