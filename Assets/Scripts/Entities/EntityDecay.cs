using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDecay : MonoBehaviour
{
    [SerializeField] private float decayAfterSeconds = 7f;

    [SerializeField] private float decayTime = 2f;
    [SerializeField] private float deathTime = 2f;

    [SerializeField] private ParticleSystem ps = default;

    private void Start() {
        StartCoroutine(DecayEntity());
    }

    private IEnumerator DecayEntity() {
        yield return new WaitForSeconds(decayAfterSeconds);

        Vector2 size = transform.localScale;

        float decayX = size.x * (Time.fixedDeltaTime / decayTime);
        float decayY = size.y * (Time.fixedDeltaTime / decayTime);

        float timeLeft = decayTime;
        while(timeLeft > 0) {
            size.x -= decayX;
            size.y -= decayY;

            transform.localScale = size;

            timeLeft -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        GetComponent<Collider2D>().enabled = false;
        if(ps != null) {
            ps.Stop();
        }

        yield return new WaitForSeconds(deathTime);

        Destroy(gameObject);
    }

}
