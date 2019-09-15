using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	public float Camera_Move_Speed = 40.0f;

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.A))
		{
			transform.position = new Vector3(transform.position.x + Camera_Move_Speed, transform.position.y, transform.position.z);
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			transform.position = new Vector3(transform.position.x - Camera_Move_Speed, transform.position.y, transform.position.z);
		}

		if (Input.GetKeyDown(KeyCode.W))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - Camera_Move_Speed);
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Camera_Move_Speed);
		}

		if (Input.mouseScrollDelta.y > 0)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y - Camera_Move_Speed, transform.position.z);
		}
		else if (Input.mouseScrollDelta.y < 0)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + Camera_Move_Speed, transform.position.z);
		}

    }
}
