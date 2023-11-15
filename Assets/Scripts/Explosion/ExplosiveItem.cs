using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveItem : MonoBehaviour
{
    [SerializeField]private float radius=10f;
    [SerializeField] private ParticleSystem explosionEffectPrefab;
    private ParticleSystem explosionEffect;
    private void Start()
    {
        StartCoroutine("Detonate");
    }
    private IEnumerator Detonate() {

        Debug.Log("Inside detonate");
        explosionEffect = Instantiate(explosionEffectPrefab);
        explosionEffect.transform.position = gameObject.transform.position;
       
        Vector2 position = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius);
        foreach (Collider2D hit in colliders) {
            Debug.Log("Inside detonate : "+hit.gameObject.tag);
            if(hit.gameObject.CompareTag(GlobalConstant.LIGHT_TAG))
                Destroy(hit.gameObject, 5f);
        }
        Destroy(gameObject, 5f);

        yield return new WaitForSeconds(5);

        explosionEffect.Play();
    }
}
