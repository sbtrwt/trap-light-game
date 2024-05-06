using System.Collections;
using UnityEngine;
namespace TrapLight.Player.Explosion
{
    public class ExplosionView : MonoBehaviour
    {
        private ExplosionController controller;
        [SerializeField] private float radius = 5f;
        [SerializeField] private ParticleSystem explosionEffectPrefab;
        private ParticleSystem explosionEffect;
        private LineRenderer circleRange;
        private int circleSteps = 360;
        private void Start()
        {
            circleRange = GetComponent<LineRenderer>();
        }

        public void Init()
        {
            DrawCircle(circleSteps, radius);
            StartCoroutine(StartAfterDelay());
        }
        public void SetController(ExplosionController controller)
        {
            this.controller = controller;
        }

        private void Detonate()
        {
            //SoundManager.Instance.Play(SoundType.Explosion);

            explosionEffect = Instantiate(explosionEffectPrefab);
            explosionEffect.transform.position = gameObject.transform.position;

            //Vector2 position = transform.position;
            //Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius);
            //foreach (Collider2D hit in colliders)
            //{

            //    if (hit.gameObject.CompareTag(GlobalConstant.LIGHT_TAG))
            //    {
            //        Destroy(hit.gameObject);
            //        if (GameController.Instance != null)
            //            GameController.Instance.DecreaseLightParticleCount();
            //    }

            //    if (hit.gameObject.CompareTag(GlobalConstant.BLACK_TAG))
            //    {
            //        BlackParticle black = hit.gameObject.GetComponent<BlackParticle>();
            //        if (black != null)
            //        {
            //            black.AddHealth(-20);
            //        }
            //    }

            //}
            //Destroy(gameObject);
            controller.ResetExplosion();
            Destroy(explosionEffect.gameObject, 1f);

        }

        private IEnumerator StartAfterDelay()
        {
            yield return new WaitForSeconds(4);
            Detonate();
            //if (GameController.Instance != null)
            //{
            //    GameController.Instance.IncrementExplosionCount();
            //    GameController.Instance.ValidateGame();
            //}
        }

        private void DrawCircle(int steps, float radius)
        {
            if (circleRange == null) return;

            circleRange.positionCount = steps;
            float delta, rad, xScaled, yScaled, x, y;
            Vector3 pos;
            for (int i = 0; i < steps; i++)
            {
                delta = (float)i / steps;
                rad = delta * 2 * Mathf.PI;
                xScaled = Mathf.Cos(rad);
                yScaled = Mathf.Sin(rad);
                x = xScaled * radius + transform.position.x;
                y = yScaled * radius + transform.position.y;

                pos = new Vector3(x, y, 0);
                circleRange.SetPosition(i, pos);
            }

        }
    }
}
