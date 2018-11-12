using UnityEngine;

public class ColorPicker : MonoBehaviour
{

    public Material[] mgColorMat;
    Material currMar;
    Renderer renderer;


	void Start ()
    {
        renderer = this.GetComponent<Renderer>();
	}

    
    //render blue color
    public void BlueColor()
    {
        renderer.material = mgColorMat[0];
        currMar = renderer.material;
    }

    
    //render red color
    public void RedColor()
    {
        renderer.material = mgColorMat[1];
        currMar = renderer.material;
    }
    
    
    //render yellow color
    public void YellowColor()
    {
        renderer.material = mgColorMat[2];
        currMar = renderer.material;
    }


    //render Green color
    public void GreenColor()
    {
        renderer.material = mgColorMat[3];
        currMar = renderer.material;
    }
}
