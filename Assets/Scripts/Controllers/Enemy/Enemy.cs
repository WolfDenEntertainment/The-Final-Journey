using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] GhostStats target;
    [SerializeField] float range = 5f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float energyDrainRate = 2.5f;
    [SerializeField] float lifetime = 30f;

    new Rigidbody rigidbody;
    float timer;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<GhostStats>();
    }

    void FixedUpdate()
    {        
        if (IsPlayerInRange(range))
            target.CurrentEnergy -= (energyDrainRate * range * Time.deltaTime) / 10;
    }

    void Update()
    {
        transform.LookAt(target.transform);
        
        while (!IsPlayerInRange(range))
            Follow();

        if (timer < lifetime)
            timer += Time.deltaTime;

        if (timer >= lifetime)
            Destroy(gameObject);
    }

    private bool IsPlayerInRange(float range)
    {
        rigidbody.velocity = Vector3.zero;
        return (target.transform.position - transform.position).magnitude <= range;
    }

    void Follow()
    {
        rigidbody.velocity = Vector3.ClampMagnitude(transform.forward * moveSpeed, 1.5f);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }
}