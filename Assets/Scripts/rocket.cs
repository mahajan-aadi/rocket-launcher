using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audio;
    [SerializeField] float thrust = 50f, rotation = 50f;
    level_manager level_Manager;
    [SerializeField] AudioClip success, death, thrust_sound;
    [SerializeField] ParticleSystem success_particle, death_particle, thrust_particle;
    enum State { Alive, Dying, }
    State current_state = State.Alive;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        level_Manager = FindObjectOfType<level_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (current_state == State.Alive)
        {
            process_check();
            rotate();
        }
    }

    private void process_check()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
            thrust_particle.Play();
            if (!audio.isPlaying)
            {
                audio.Stop();
                audio.PlayOneShot(thrust_sound); 
            }
        }
        else { audio.Stop(); thrust_particle.Stop(); }
    }

    private void rotate()
    {
        Vector3 rotation_to_do = Vector3.forward * rotation * Time.deltaTime;
        rigidbody.freezeRotation = true;
        if (Input.GetKey(KeyCode.LeftArrow)) { transform.Rotate(rotation_to_do); }
        if (Input.GetKey(KeyCode.RightArrow)) { transform.Rotate(-rotation_to_do); }
        rigidbody.freezeRotation = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (current_state == State.Alive) { check_collision_type(collision); }
    }

    private void check_collision_type(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case ("friendly"):
                break;
            case ("Finish"):
                current_state = State.Dying;
                Instantiate(success_particle, Vector3.zero, Quaternion.identity);
                audio.Stop();
                audio.PlayOneShot(success);
                level_Manager.next_level();
                break;
            default:
                current_state = State.Dying;
                Instantiate(death_particle, transform.position, Quaternion.identity);
                audio.Stop();
                audio.PlayOneShot(death);
                level_Manager.lost();
                break;
        }
    }
}
