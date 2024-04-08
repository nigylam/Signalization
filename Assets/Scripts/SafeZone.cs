using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SafeZone : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumeChanging = 0.1f;

    private bool _isPlaying = false;
    private float _startVolume = 0;
    private float _endVolume = 1;

    private void Update()
    {
        ChangeVolume();
    }

    private void Start()
    {
        _audioSource.volume = _startVolume;
    }

    private void ChangeVolume()
    {
        if (_isPlaying)
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _endVolume, _volumeChanging * Time.deltaTime);

        if (_audioSource.volume == 0)
        {
            _isPlaying = false;
            _audioSource.Pause();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _audioSource.Play();
        _endVolume = 1;
        _isPlaying = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _endVolume = 0;
    }
}
