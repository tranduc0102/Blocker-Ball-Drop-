using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject ballPrefab;
    [SerializeField] private int _count = 10;
    public float spawnRadius = 3f;
    public float spawnDelay = 0.1f;
    public float appearDuration = 0.4f;
    public float explosionForce = 2f;   
    public float upwardForce = 1f;        

    public void SetCountSpawn(int count)
    {
        _count = count;
    }

    public void SpawnBalls()
    {
        StopAllCoroutines();
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        Vector3 origin = transform.position;

        for (int i = 0; i < _count; i++)
        {
            Vector2 random2D = Random.insideUnitCircle * spawnRadius * 0.3f;
            Vector3 spawnPos = new Vector3(origin.x + random2D.x, origin.y + random2D.y, origin.z);

            GameObject ball = Instantiate(ballPrefab, spawnPos, Quaternion.identity, transform);
            ball.transform.localScale = Vector3.zero;

            ball.transform.DOScale(1f, appearDuration).SetEase(Ease.OutBack);

            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 forceDir = (spawnPos - origin).normalized;
                forceDir.z = 0f;

                rb.AddForce(forceDir * explosionForce + Vector3.up * upwardForce, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 0.7f, 1f, 0.25f);
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
#endif
}
