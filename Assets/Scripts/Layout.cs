using UnityEngine;
using System.Collections;

public class Layout : MonoBehaviour {

	public GameObject Maze;
	public GameObject North;
	public GameObject South;
	public GameObject East;
	public GameObject West;

	// Use this for initialization
	void Start () {
		Generator gen = Maze.GetComponent<Generator>();
		float height = gen.Height;
		float width = gen.Width;

		North.transform.position = new Vector3(width / 2.0f, North.transform.position.y, North.transform.position.z);
		South.transform.position = new Vector3(-width / 2.0f, South.transform.position.y, South.transform.position.z);
		West.transform.position = new Vector3(West.transform.position.x, West.transform.position.y, height / 2.0f);
		East.transform.position = new Vector3(East.transform.position.x, East.transform.position.y, -height / 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
