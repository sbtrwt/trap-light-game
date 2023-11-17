using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveItem : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private ParticleSystem explosionEffectPrefab;
    private ParticleSystem explosionEffect;
    private LineRenderer circleRange;
    private int circleSteps = 360;
    private void Start()
    {
        circleRange = GetComponent<LineRenderer>();

        DrawCircle(circleSteps, radius);
        StartCoroutine("StartAfterDelay");
    }
    private void Detonate()
    {

        Debug.Log("Inside detonate");
        explosionEffect = Instantiate(explosionEffectPrefab);
        explosionEffect.transform.position = gameObject.transform.position;

        Vector2 position = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius);
        foreach (Collider2D hit in colliders)
        {
            Debug.Log("Inside detonate : " + hit.gameObject.tag);
            if (hit.gameObject.CompareTag(GlobalConstant.LIGHT_TAG))
                Destroy(hit.gameObject);
        }
        Destroy(gameObject);
        explosionEffect.Play();
    }

    private IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(4);
        Detonate();
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
