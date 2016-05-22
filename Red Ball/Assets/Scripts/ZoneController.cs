using UnityEngine;

public class ZoneController : MonoBehaviour
{
    public float upForce = 9.8f;
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().InZone(new Vector2(0, upForce),gameObject.tag);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().LeaveZone();
        }
    }
}
