using UnityEngine;

public class Zoom : MonoBehaviour
{
    public GameObject objectZoom;
    Vector3 temp;


    public void ZoomIn()
    {
        temp = objectZoom.transform.localScale;
        temp.x += 2f;
        temp.y += 2f;
        temp.z += 2f;
        objectZoom.transform.localScale = temp;
    }


    public void ZoomOut()
    {
        temp = objectZoom.transform.localScale;
        temp.x -= 2f;
        temp.y -= 2f;
        temp.z -= 2f;
        objectZoom.transform.localScale = temp;
    }
}
