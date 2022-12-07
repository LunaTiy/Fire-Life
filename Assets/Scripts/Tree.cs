using System;
using System.Collections;
using UnityEngine;

public class Tree : MonoBehaviour, IGrabAvailable
{
    [SerializeField] private GameObject _logPrefab;
    [SerializeField] private AudioSource _audioSource;

    public GameObject GetItem()
    {
        StartCoroutine(PlayGrabSound());

        GameObject log = Instantiate(_logPrefab, transform.position, Quaternion.identity);
        return log;
    }

    private IEnumerator PlayGrabSound()
    {
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        
        Destroy(gameObject);
    }
}