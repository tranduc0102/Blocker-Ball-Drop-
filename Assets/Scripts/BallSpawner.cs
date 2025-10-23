using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : Singleton<BallSpawner>
{
    [Header("Spawn Settings")]
    public GameObject ballPrefab;
    public float spawnRadius = 5f;
    public int maxAttempts = 10;
    public float checkStep = 1f;
    [Header("Collision Settings")]
    public LayerMask obstacleMask;       
    public float clearRadius = 0.5f;

    public void SpawnBalls(int count)
    {
        Vector3 origin = transform.position;
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPos = origin + UnityEngine.Random.insideUnitSphere * spawnRadius;
            Vector3 safePos = FindNearestClearPosition(randomPos);
            if (safePos != Vector3.zero)
            {
                Instantiate(ballPrefab, safePos, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning($"BallSpawner: Could not find safe position for ball #{i}");
            }
        }
    }
    public void SpawnAtSafePosition(Vector3 desiredPosition)
    {
        Vector3 safePos = FindNearestClearPosition(desiredPosition);
        if (safePos != Vector3.zero)
        {
            Instantiate(ballPrefab, safePos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("BallSpawner: No safe position found at or near " + desiredPosition);
        }
    }

    private bool IsPositionClear(Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(position, clearRadius, obstacleMask);
        return hits.Length == 0;
    }
    private Vector3 FindNearestClearPosition(Vector3 origin)
    {
        if (IsPositionClear(origin))
        {
            return origin;
        }

        foreach (Vector3 dir in Directions3D())
        {
            for (float step = checkStep; step <= spawnRadius; step += checkStep)
            {
                Vector3 testPos = origin + dir * step;
                if (IsPositionClear(testPos))
                {
                    return testPos;
                }
            }
        }
        return Vector3.zero;
    }

    private IEnumerable<Vector3> Directions3D()
    {
        yield return Vector3.up;
        yield return Vector3.down;
        yield return Vector3.left;
        yield return Vector3.right;
        yield return Vector3.forward;
        yield return Vector3.back;
        yield return (Vector3.up + Vector3.left).normalized;
        yield return (Vector3.up + Vector3.right).normalized;
        yield return (Vector3.down + Vector3.left).normalized;
        yield return (Vector3.down + Vector3.right).normalized;
        yield return (Vector3.forward + Vector3.left).normalized;
        yield return (Vector3.forward + Vector3.right).normalized;
        yield return (Vector3.back + Vector3.left).normalized;
        yield return (Vector3.back + Vector3.right).normalized;
    }
}
