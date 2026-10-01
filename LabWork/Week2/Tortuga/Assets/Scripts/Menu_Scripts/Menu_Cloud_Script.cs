using UnityEngine;

public class Menu_Cloud_Script : MonoBehaviour
{
    [Range(-1f,1f)]
    public float scrollSpeed = 0.5f;
    public float VerticalscrollSpeed = 0f;
    private float offset;
    private float voffset; 
    //Vertical Offset
    private Material mat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        //used to scroll the background clouds. creating the nice moving effect.
        //when scene first loads, gets the Renderer
    }

    // Update is called once per frame
    void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10f;
        //each frame the texture on the background is offset by the small amount, creating the scrolling effect
        voffset += (Time.deltaTime * VerticalscrollSpeed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset,-voffset));
    }
}
