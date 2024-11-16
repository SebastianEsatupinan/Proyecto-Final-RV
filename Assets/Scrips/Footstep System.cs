using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSystem : MonoBehaviour
{
    public AudioSource AudioSource;

    public AudioClip concrete;

    RaycastHit hit;
    public Transform RayStart;
    public float range;
    public LayerMask layerMask;

    public void Footstep()
    {
        if (Physics.Raycast(RayStart.position, RayStart.transform.up * -1, out hit, range, layerMask))
        {
            if (hit.collider.CompareTag("concrete"))
            {
                PlayFootstepSoundL(concrete);
            }

        }
    }

    void PlayFootstepSoundL(AudioClip audio)
    {
        AudioSource.pitch = Random.Range(0.8f, 1f);
        AudioSource.PlayOneShot(audio);
    }




}
