using UnityEngine;
using System.Collections;
 
public class MagnifyGlass : MonoBehaviour
{
    public Camera magnifyCamera;
    public GameObject magnifyBorders;
    private LineRenderer LeftBorder, RightBorder, TopBorder, BottomBorder; // Reference for lines of magnify glass borders
    private float MGOX,MG0Y; // Magnify Glass Origin X and Y position
    private float MGWidth = Screen.width/5f,MGHeight = Screen.width/5f; // Magnify glass width and height
    private Vector3 mousePos;
 
    void Start ()
    {
        createMagnifyGlass ();
    }
    void Update ()
    {
        // Following lines set the camera's pixelRect and camera position at mouse position
        magnifyCamera.pixelRect = new Rect (Input.mousePosition.x - MGWidth / 2.0f, Input.mousePosition.y - MGHeight / 2.0f, MGWidth, MGHeight);
        mousePos = getWorldPosition (Input.mousePosition);
        magnifyCamera.transform.position = mousePos;
        mousePos.z = 0;
        magnifyBorders.transform.position = mousePos;
    }
 
    // Following method creates MagnifyGlass
    private void createMagnifyGlass()
    {
        MGOX = Screen.width / 2f - MGWidth/2f;
        MG0Y = Screen.height / 2f - MGHeight/2f;
        magnifyCamera.pixelRect = new Rect(MGOX, MG0Y, MGWidth, MGHeight);
        magnifyCamera.transform.position = new Vector3(0,0,0);
        if(Camera.main.orthographic)
        {
            magnifyCamera.orthographic = true;
            magnifyCamera.orthographicSize = Camera.main.orthographicSize / 5.0f;//+ 1.0f;
        }
        else
        {
            magnifyCamera.orthographic = false;
            magnifyCamera.fieldOfView = Camera.main.fieldOfView / 10.0f;//3.0f;
        }
     
    }
     
    // Following method calculates world's point from screen point as per camera's projection type
    public Vector3 getWorldPosition(Vector3 screenPos)
    {
        Vector3 worldPos;
        if(Camera.main.orthographic)
        {
            worldPos = Camera.main.ScreenToWorldPoint (screenPos);
            worldPos.z = Camera.main.transform.position.z;
        }
        else
        {
            worldPos = Camera.main.ScreenToWorldPoint (new Vector3 (screenPos.x, screenPos.y, Camera.main.transform.position.z));
            worldPos.x *= -1;
            worldPos.y *= -1;
        }
        return worldPos;
    }
}