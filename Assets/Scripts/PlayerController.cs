using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float torqueAmount = 1f;
    [SerializeField] private float boostSpeed = 30f;
    [SerializeField] private float normalSpeed = 20f;
    [SerializeField] private ParticleSystem snowParticles;

    private Rigidbody2D _rb;
    private SurfaceEffector2D _surfaceEffector;
    private bool _canMove = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _surfaceEffector = FindObjectOfType<SurfaceEffector2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            snowParticles.Play();
        }
    }
    
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            snowParticles.Stop();
        }
    }

    private void Update()
    {
        if (!_canMove) return;
        RotatePlayer();
        RespondToBoost();
    }

    public void DisableControls()
    {
        _canMove = false;
    }

    private void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rb.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _rb.AddTorque(-torqueAmount);
        }
    }

    private void RespondToBoost()
    {
        _surfaceEffector.speed = Input.GetKey(KeyCode.UpArrow) ? boostSpeed : normalSpeed;
    }
}