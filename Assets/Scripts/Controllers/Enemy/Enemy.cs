using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] Transform target;
	[SerializeField] float movementSpeed;
	[SerializeField] float lifetime = 10;
	[SerializeField] float energyDrainRate = 50;
	[SerializeField] float range = 2f;
	[SerializeField] float minDistance = 1;

    [Header("Wander")]
	public float directionChangeInterval = 1;
	public float maxHeadingChange = 30;

	CharacterController controller;
	GhostStats player;
	Vector3 targetRotation;
	float heading;
	float timer;

	public float EnergyDrainRate { get => energyDrainRate; }

    void Awake()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

		if (target != null)
			player = target.GetComponent<GhostStats>();

		controller = GetComponent<CharacterController>();

		// Set random initial rotation
		heading = Random.Range(0, 360);
		transform.eulerAngles = new Vector3(0, heading, 0);


		StartCoroutine(NewHeading());
	}

	void Update()
	{
		if (target == null)
		{
			transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
			var forward = transform.TransformDirection(Vector3.forward);
			controller.SimpleMove(forward * movementSpeed * Time.deltaTime);
		}
		else
		{
			float distance = (target.position - transform.position).magnitude;
			while (distance >= minDistance)
			{
				controller.enabled = false;
				StopAllCoroutines();
				Vector3 targetPosition = target.position + new Vector3(-3, 0, 0);

				transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

				if (distance <= range && player != null)
					player.CurrentEnergy -= (EnergyDrainRate * Time.deltaTime * 4) / distance;
			}
        }

		if (timer < lifetime)
        {
			timer += Time.deltaTime;

			if (timer >= lifetime)
				Destroy(gameObject);
        }
	}

	IEnumerator NewHeading()
	{
		while (true)
		{
			NewHeadingRoutine();
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}

	void NewHeadingRoutine()
	{
		var floor = transform.eulerAngles.y - maxHeadingChange;
		var ceil = transform.eulerAngles.y + maxHeadingChange;
		heading = Random.Range(floor, ceil);
		targetRotation = new Vector3(0, heading, 0);
	}

}
