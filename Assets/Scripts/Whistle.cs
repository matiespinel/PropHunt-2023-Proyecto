using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class Whistle : MonoBehaviour
{
    private AudioSource _audioSource;
    bool whistlerequestBool = true;

    [SerializeField] int delay = 10;
    PhotonView view;
    void Awake()
    {
        view = GetComponent<PhotonView>();
        _audioSource = GetComponent<AudioSource>();
    }

    
    void FixedUpdate()
    {
        if(whistlerequestBool) StartCoroutine(WhistleDelay());
    }

    IEnumerator WhistleDelay()
    {
        whistlerequestBool = !whistlerequestBool;
        WhistleStart();
        Debug.Log("WHISTLE");
        yield return new WaitForSeconds(delay);
        WhistleEnd();
        whistlerequestBool = !whistlerequestBool;
    }

    
    public void WhistleStart()
    {
        RegisterWhistleAudio();
    }

    public void WhistleEnd()
    {
        DeregisterWhistleAudio();
    }

    void PlayWhistleAudio() => _audioSource.Play();

    private void DeregisterWhistleAudio() => SoundManagerScript.OnAnySound += PlayWhistleAudio;

    private void RegisterWhistleAudio()  => SoundManagerScript.OnAnySound -= PlayWhistleAudio;
}
