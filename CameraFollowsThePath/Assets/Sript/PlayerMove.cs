using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public float speed = 5;

	private Vector3 m_velocity;

	void Update () {
		m_velocity = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * speed;
		transform.position += m_velocity * Time.fixedDeltaTime; 
	}
}
