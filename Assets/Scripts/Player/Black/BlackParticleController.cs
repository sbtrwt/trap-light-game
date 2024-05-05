using UnityEngine;

namespace TrapLight.Player.Black
{
    public class BlackParticleController
    {
        private BlackParticleView blackView;
        private BlackParticleSO blackScriptableObject;

        private int currentHealth;
        private float horizontalAxis;
        private float verticalAxis;
     
        private int currentSpeed;
        public Vector3 Position => blackView.transform.position;

        public BlackParticleController(BlackParticleView blackPrefab)
        {
            blackView = Object.Instantiate(blackPrefab);
            blackView.SetController(this);
        }

        public void Init(BlackParticleSO blackScriptableObject)
        {
            this.blackScriptableObject = blackScriptableObject;
            InitializeVariables();
            //SetState(LightState.ACTIVE);
        }

        private void InitializeVariables()
        {
         
            currentHealth = blackScriptableObject.Health;
            currentSpeed = blackScriptableObject.Speed;
        }
        public void OnPopAnimationPlayed()
        { }

        private void GetInput()
        {
            horizontalAxis = Input.GetAxis("Horizontal");
            verticalAxis = Input.GetAxis("Vertical");

        }
        public void Move(Rigidbody2D playerRigidbody)
        {
            GetInput();
            playerRigidbody.MovePosition(playerRigidbody.position + new Vector2(horizontalAxis, verticalAxis) * Time.fixedDeltaTime * currentSpeed);
        }
    }
}
