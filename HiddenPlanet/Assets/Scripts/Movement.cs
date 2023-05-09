using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 10f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem MainBooster;
    [SerializeField] ParticleSystem LeftBooster;
    [SerializeField] ParticleSystem RightBooster;
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
        void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }
    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!MainBooster.isPlaying)
        {
            MainBooster.Play();
        }
    }
    private void StopThrusting()
    {
        audioSource.Stop();
        MainBooster.Stop();
    }
        private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!LeftBooster.isPlaying)
        {
            LeftBooster.Play();
        }
    }
    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!RightBooster.isPlaying)
        {
            RightBooster.Play();
        }
    }
    private void StopRotating()
    {
        RightBooster.Stop();
        LeftBooster.Stop();
    }
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}