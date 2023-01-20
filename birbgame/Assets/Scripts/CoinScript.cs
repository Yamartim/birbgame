using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinScript : Interactable
{

    // declaring the event that can be subscribed to by the many other classes that interact with coins
    public static event Action OnCoinCollect;

    public bool collected { get; private set; }

    private MeshRenderer mesh;
    private CapsuleCollider coll;
    private Light plight;
    private ParticleSystem coinSparkle;
    private AudioSource collectSound;

    private void Start() {
        mesh = GetComponent<MeshRenderer>();
        coll = GetComponent<CapsuleCollider>();
        plight = GetComponentInChildren<Light>();
        collectSound = GetComponentInChildren<AudioSource>();
        coinSparkle = GetComponentInChildren<ParticleSystem>();
    }

    public override void Interacion(GameObject p)
    {
        collected = true;

        mesh.enabled = false;
        coll.enabled = false;
        plight.enabled = false;
        collectSound.Play();
        coinSparkle.Play();
        OnCoinCollect?.Invoke(); //the question mark identifies if the event exists first
        //release particles and some sfx
    }
}