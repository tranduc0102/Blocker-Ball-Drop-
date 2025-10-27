using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
[DisallowMultipleComponent]
public class BallCollide : MonoBehaviour
{
    [Header("Sound")]
    [Tooltip("Audio clip to play on impact")]
    public AudioClip impactClip;

    [Range(0f, 5f)]
    [Tooltip("Ignore collisions whose relative speed is below this")]
    public float minSpeed = 0.5f;

    [Tooltip("Scale output volume by impact speed?")]
    public bool scaleVolume = true;

    [Range(0f, 2f)]
    [Tooltip("Cooldown between two sounds (seconds)")]
    public float cooldown = 0.1f;

    private AudioSource _src;
    private float _lastHitTime;

    private void Awake()
    {
        _src = GetComponent<AudioSource>();
        if (_src == null)
        {
            _src = gameObject.AddComponent<AudioSource>();
        }

        _src.playOnAwake = false;
        _src.volume = PlayerPrefs.GetFloat("VolumeSound", 1);
        _src.spatialBlend = 1f;
        _src.rolloffMode = AudioRolloffMode.Logarithmic;
        _src.maxDistance = 10f;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (impactClip == null || col == null) return;

        float relativeSpeed = col.relativeVelocity.magnitude;

        if (relativeSpeed < minSpeed || Time.time - _lastHitTime < cooldown)
            return;

        _lastHitTime = Time.time;

        float volume = 1f;
        if (scaleVolume)
        {
            volume = Mathf.Min(Mathf.Clamp01(relativeSpeed / 5f), PlayerPrefs.GetFloat("VolumeSound", 1));
        }
        _src.transform.position = col.contacts[0].point;
        _src.PlayOneShot(impactClip, volume);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            AudioManager.Instance.PlayGate();
        }
    }

    private void OnDestroy()
    {
        if (_src != null && Application.isPlaying)
        {
            Destroy(_src);
        }
    }
}