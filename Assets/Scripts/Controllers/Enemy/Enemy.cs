using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GhostStats target;
    [SerializeField] float range = 3f;
    [SerializeField] float energyDrainRate = 2.5f;
    [SerializeField] float lifetime = 30f;
    [SerializeField] float speed = 2.0f;

    float timer;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<GhostStats>();

        Debug.Log("Spawn Position:  " + transform.position.ToString());
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            timer = 0;
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        transform.LookAt(target.transform.position);

        Vector3 direction = target.transform.position - transform.position;

        if (direction.magnitude > range)
            Follow(direction);

        if (direction.magnitude <= range) 
            target.CurrentEnergy -= (energyDrainRate * range * Time.deltaTime) / 3.75f;
    }

    void Follow(Vector3 direction)
    {
        transform.Translate(speed * Time.deltaTime * direction.normalized, Space.World);
    }
}