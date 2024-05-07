using TrapLight.Player.Explosion;
using UnityEngine;

namespace TrapLight.Player.Black
{
    public class BlackParticleController
    {
        private BlackParticleView blackView;
        private BlackParticleSO blackScriptableObject;
        private PlayerService playerService;

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

        public void Init(BlackParticleSO blackScriptableObject, PlayerService playerService)
        {
            this.blackScriptableObject = blackScriptableObject;
            this.playerService = playerService;
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
        public void Move(Rigidbody2D playerRigidbody)
        {
            GetInput();
            playerRigidbody.MovePosition(playerRigidbody.position + new Vector2(horizontalAxis, verticalAxis) * Time.fixedDeltaTime * currentSpeed);
        }

        private void SetExplosive()
        {
            playerService.SetExplosive();
        }
    }
}
