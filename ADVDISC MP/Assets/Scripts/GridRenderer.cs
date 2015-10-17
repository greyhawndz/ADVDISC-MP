using UnityEngine;
using System.Collections;

public class GridRenderer : MonoBehaviour {
	public GameObject markerPrefab;
	public int height = 15;
	public int width = 15;


	void Awake(){
		RenderGrid();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void RenderGrid(){
		for(int x = 0; x < width; x++){
			GameObject marker = (GameObject) Instantiate(markerPrefab, new Vector3(0,0,0), Quaternion.identity);
			marker.transform.position = new Vector3(marker.transform.position.x + x, marker.transform.position.y, marker.transform.position.z);
			marker.transform.parent = this.transform;
		}
		for(int x = 0; x > -width; x--){
			GameObject marker = (GameObject) Instantiate(markerPrefab, new Vector3(0,0,0), Quaternion.identity);
			marker.transform.position = new Vector3(marker.transform.position.x + x, marker.transform.position.y, marker.transform.position.z);
			marker.transform.parent = this.transform;
		}
		for(int y = 0; y < height; y++){
			GameObject marker = (GameObject) Instantiate(markerPrefab, new Vector3(0,0,0), Quaternion.identity);
			marker.transform.position = new Vector3(marker.transform.position.x, marker.transform.position.y + y, marker.transform.position.z);
			marker.transform.parent = this.transform;
		}
		for(int y = 0; y > -height; y--){
			GameObject marker = (GameObject) Instantiate(markerPrefab, new Vector3(0,0,0), Quaternion.identity);
			marker.transform.position = new Vector3(marker.transform.position.x, marker.transform.position.y + y, marker.transform.position.z);
			marker.transform.parent = this.transform;
		}
	}
}
