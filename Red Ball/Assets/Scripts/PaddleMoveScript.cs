using UnityEngine;

public class PaddleMoveScript : MonoBehaviour {
	private bool toTheRight;

	// Use this for initialization
	void Start () {
		toTheRight = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale != 0)
		if (toTheRight)
			transform.position = new Vector2(transform.position.x + (float)0.03 ,transform.position.y);
		else
			transform.position = new Vector2(transform.position.x - (float)0.03 ,transform.position.y);
	}
	
	public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Border" ) 
			toTheRight = !toTheRight;
    }
	
}
