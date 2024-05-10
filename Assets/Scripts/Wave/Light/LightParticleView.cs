using TMPro;
using TrapLight.Player.Black;
using UnityEngine;

namespace TrapLight.Wave.Light
{
    public class LightParticleView : MonoBehaviour
    {
        private LightParticleController controller;

        [SerializeField] private SpriteRenderer spriteRenderer;
      
        [SerializeField] private TMP_Text healthText;
    
        public void SetController(LightParticleController controller)
        {
            this.controller = controller;
        }
        public LightParticleController GetController()
        {
            return controller;
        }
        public void SetRenderer(Sprite spriteToSet) => spriteRenderer.sprite = spriteToSet;

        public void SetColor(Color colortToSet) => spriteRenderer.color = colortToSet;
        public void SetHealth(string healthToSet) => healthText.text = healthToSet;
      
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.gameObject.CompareTag(GlobalConstant.BLACK_TAG))
            {
                BlackParticleView blackParticle = collision.collider.gameObject.GetComponent<BlackParticleView>();
                if (blackParticle != null)
                    controller.OnHitBlackParticle(blackParticle.GetController());
            }
        }
    }
}
