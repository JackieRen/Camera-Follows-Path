using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public Path path;
	public Transform Player;
    public float speed = 3f;

	void Update () {
		transform.LookAt (Player.position);
        //transform.position = path.CameraPositionFollowPath (Player.position);
        transform.position = Vector3.Lerp(transform.position, path.CameraPositionFollowPath(Player.position), Time.deltaTime * speed);
    }

}
