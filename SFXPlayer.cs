using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource _audioSource;

    [SerializeField] AudioClip [] swordSwings;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip land;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip move;

    private void AdjustPitch(bool adjustPitch = true)
    {
        _audioSource.pitch = adjustPitch ? Random.Range(1f, 1.25f) : 1f;
    }

    public void SwingSword(int idx = 0)
    {
        AdjustPitch();
        _audioSource.PlayOneShot(swordSwings[idx]);
    }

    public void CharacterHit()
    {
        AdjustPitch();
        _audioSource.PlayOneShot(hit);
    }

    public void Jump()
    {
        AdjustPitch(false);
        _audioSource.PlayOneShot(jump);
    }

    public void Land()
    {
        AdjustPitch(false);
        _audioSource.PlayOneShot(land);
    }

    public void Death()
    {
        AdjustPitch(false);
        _audioSource.PlayOneShot(death);
    }
    public void Move()
    {
        AdjustPitch(false);
        _audioSource.PlayOneShot(move);
    }
}
