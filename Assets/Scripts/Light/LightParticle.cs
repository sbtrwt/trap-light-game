using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrapLight.Light
{
    public class LightParticle : MonoBehaviour
    {
        [SerializeField] private float speed = 0.4f;
        private Rigidbody2D rb2D;
        private float maxGravityTime = 4;
        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
          StartCoroutine(  DecreaseGravity());
        }
        
        private IEnumerator DecreaseGravity() 
        {
            rb2D.gravityScale = speed;
            for(int i = 1; i <= maxGravityTime; i++)
            {
                rb2D.gravityScale -= speed/ maxGravityTime;
                yield return null;
            }
            rb2D.gravityScale = 0.1f;
        }
    }
}