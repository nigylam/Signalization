using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class SafeZone : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumeChanging = 0.1f;

    private float _minVolume = 0;
    private float _maxVolume = 1;

    private void Start()
    {
        _audioSource.volume = _minVolume;
    }

    private void FadeIn()
    {
        _audioSource.Play();
        StopAllCoroutines();
        StartCoroutine(ChangeVolume(_audioSource.volume, _maxVolume));
    }

    private void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeVolume(_audioSource.volume, _minVolume));
    }

    private IEnumerator ChangeVolume(float currentVolume, float endVolume)
    {
        while(currentVolume != endVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(currentVolume, endVolume, _volumeChanging * Time.deltaTime);
            currentVolume = _audioSource.volume;
            yield return null;
        }

        if (_audioSource.volume == 0)
            _audioSource.Pause();
    }

    private void OnTriggerEnter(Collider other) => FadeIn();

    private void OnTriggerExit(Collider other) => FadeOut();
}
