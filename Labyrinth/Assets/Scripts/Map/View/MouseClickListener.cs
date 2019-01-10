using System;
using UnityEngine;

namespace Scripts.Map.View
{
    public class MouseClickListener : MonoBehaviour
    {
        public event Action<IntVector2> TileClicked;
        public event Action<IntVector2> RightClicked;
        public delegate void ButtonPressHandler();
        public event ButtonPressHandler RightButtonClicked;
        public event ButtonPressHandler LeftButtonClicked;
        public event ButtonPressHandler UpButtonClicked;
        public event ButtonPressHandler DownButtonClicked;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                Vector3 position;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    position = hit.transform.gameObject.transform.parent.gameObject.transform.position;
                    
                    Plane plane = new Plane(Vector3.up, position);
                    float point = 0f;
                    if(plane.Raycast(ray, out point))
                    {
                        Vector3 target = ray.GetPoint(point);
                        
                        if (TileClicked != null)
                            TileClicked(new IntVector2((int) target.x, (int) target.z));
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                Vector3 position;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    position = hit.transform.gameObject.transform.parent.gameObject.transform.position;
                    
                    Plane plane = new Plane(Vector3.up, position);
                    float point = 0f;
                    if(plane.Raycast(ray, out point))
                    {
                        Vector3 target = ray.GetPoint(point);
                        
                        if (RightClicked != null)
                            RightClicked(new IntVector2((int) target.x, (int) target.z));
                    }
                }
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                UpButtonClicked();
            }
        
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                DownButtonClicked();
            }
        
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                LeftButtonClicked();
            }
        
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                RightButtonClicked();
            }
        }
    }
}