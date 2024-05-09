using System;
using TMPro;
using TrapLight.Player.Explosion;
using TrapLight.UI;
using UnityEngine;

namespace TrapLight.Player.Black
{
    public class BlackParticleController
    {
        private BlackParticleView blackView;
        private BlackParticleSO blackScriptableObject;
        private PlayerService playerService;
        private UIService uiService;

        private int currentHealth;
        private float horizontalAxis;
        private float verticalAxis;

        private int currentSpeed;
        private int explosionCount;
        public Vector3 Position => blackView.transform.position;
        public bool IsExplosionEmpty => explosionCount <= 0;
        public bool IsAlive => currentHealth > 0;
        public BlackParticleController(BlackParticleView blackPrefab)
        {
            blackView = UnityEngine.Object.Instantiate(blackPrefab);
            blackView.SetController(this);
        }

        public void Init(BlackParticleSO blackScriptableObject, PlayerService playerService, UIService uIService)
        {
            this.blackScriptableObject = blackScriptableObject;
            this.playerService = playerService;
            this.uiService = uIService;

            InitializeVariables();
        }

        private void InitializeVariables()
        {

            currentHealth = blackScriptableObject.Health;
            currentSpeed = blackScriptableObject.Speed;
            blackView.SetHealthText();
        }

        private void GetInput()
        {
            horizontalAxis = Input.GetAxis("Horizontal");
            verticalAxis = Input.GetAxis("Vertical");
            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                SetExplosive();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerService.OnWallDrawStart(blackView.transform.position);
            }

            else if (Input.GetKeyUp(KeyCode.Space))
            {
                playerService.OnWallDrawEnd(blackView.transform.position);
                playerService.OnWallColliderDraw();
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                playerService.OnWallDrawing(blackView.transform.position);
            }
        }

        public void SetHealthText(TMP_Text healthText)
        {
            healthText.text = currentHealth.ToString();
        }

        public void Move(Rigidbody2D playerRigidbody)
        {
            GetInput();
            playerRigidbody.MovePosition(playerRigidbody.position + new Vector2(horizontalAxis, verticalAxis) * Time.fixedDeltaTime * currentSpeed);
        }

        private void SetExplosive()
        { 
            if (IsExplosionEmpty) return;

            explosionCount--;
            playerService.SetExplosive();
            uiService.SetExplosionCountText(explosionCount);
        }

        public void SetExplosionCount(int explosionCount)
        {
            this.explosionCount = explosionCount;
            uiService.SetExplosionCountText(explosionCount);
        }

        private void ResetHealth() => currentHealth = blackScriptableObject.Health;
        private void ResetPosition() => blackView.transform.position = blackScriptableObject.Position;
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            currentHealth = currentHealth < 0 ? 0 : currentHealth;
            blackView.SetHealthText();
            if (!IsAlive)
            {
                playerService.OnGameOver();
            }
        }
        public void ResetBlackParticle()
        {
            ResetHealth();
            ResetPosition();
            blackView.SetHealthText();
        }
    }
}
