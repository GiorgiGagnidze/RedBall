using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public void Hide()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void Show()
    {

    }
}
