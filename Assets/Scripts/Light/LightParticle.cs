using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrapLight.Light
{
    public class LightParticle : MonoBehaviour
    {
        [SerializeField] private float speed = 0.4f;
        private Rigidbody2D rigidbody2D;
        private float maxGravityTime = 4;
        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
          StartCoroutine(  DecreaseGravity());
            //MoveParticle();
        }
        private void MoveParticle() {

            //rigidbody2D.MovePosition(transform.position + new Vector3(1,1) * (Time.deltaTime * speed));
            rigidbody2D.AddForce(new Vector3(1, 9) * (Time.deltaTime * speed));
        }
        private IEnumerator DecreaseGravity() 
        {
            rigidbody2D.gravityScale = speed;
            for(int i = 1; i <= maxGravityTime; i++)
            {
                rigidbody2D.gravityScale -= speed/ maxGravityTime;
                yield return null;
            }
            rigidbody2D.gravityScale = 0.1f;
        }
    }
}