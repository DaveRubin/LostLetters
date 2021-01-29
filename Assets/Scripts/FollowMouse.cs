using UnityEngine;

namespace DefaultNamespace
{
    public class FollowMouse : MonoBehaviour
    {
        public Camera magnifyCamera;
        private LineRenderer LeftBorder, RightBorder, TopBorder, BottomBorder; // Reference for lines of magnify glass borders
        private float MGOX,MG0Y; // Magnify Glass Origin X and Y position
        private Vector3 mousePos;
     
        void Update ()
        {
            mousePos = getWorldPosition (Input.mousePosition);
            transform.position = mousePos;
            mousePos.z = 0;
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
}