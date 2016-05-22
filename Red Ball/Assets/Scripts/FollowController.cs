using UnityEngine;

[System.Serializable]
public class CameraEdge
{
    public float top;
    public float bottom;
}
public class FollowController : MonoBehaviour {
    public Transform target;
    public CameraEdge edge;
	
	// Update is called once per frame
	void Update () {
        if (target != null)
            transform.position = new Vector3(target.position.x, Mathf.Clamp( target.position.y, edge.bottom, edge.top), transform.position.z);
	}
}
