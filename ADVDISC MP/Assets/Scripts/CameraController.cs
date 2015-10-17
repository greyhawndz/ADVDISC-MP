using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public float moveSpeed = 0.6f;
	public float scrollSpeed = 15f;
	private float cameraDistance = 0f;
	
	
	// Update is called once per frame
	void Update () {
		moveCamera();
	}
	
	void moveCamera() {
		
		if(Input.GetKey(KeyCode.W)){
		Debug.Log("W pressed");
			this.transform.position += Vector3.up * moveSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.S)){
			this.transform.position += Vector3.down * moveSpeed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.D)){
			this.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
		}
		if(Input.GetKey (KeyCode.A)){
			this.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
		}
		if(Input.GetAxis("Mouse ScrollWheel") != 0){
			if(Input.GetAxis("Mouse ScrollWheel") >= 0){
				Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
				cameraDistance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;
				cameraDistance = Mathf.Clamp(cameraDistance, 1, 10);
				Debug.Log("Zoom in Camera Value: " +cameraDistance);	
				this.transform.position += new Vector3(this.transform.position.x, this.transform.position.y, cameraDistance);
			}
		
			if(Input.GetAxis("Mouse ScrollWheel") <= 0){
				Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
				cameraDistance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;
				Debug.Log("Zoom out Camera Value: " +cameraDistance);	
				this.transform.position += new Vector3(this.transform.position.x, this.transform.position.y, -cameraDistance);
			}
		}
		
		
	}
}
