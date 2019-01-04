using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SideMenuController : MonoBehaviour
{
    public float speed = 10;

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (slider.value > 0 && slider.value < 1)
        {
            if (!IsTouched())
            {
                if (slider.value < 0.4)
                {
                    slider.value = slider.value - speed * Time.deltaTime;
                }
                else
                {
                    slider.value = slider.value + speed * Time.deltaTime;
                }
            }
            else
                Debug.Log("Touched");
        }
    }

    private bool IsTouched()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
		if (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended ) 
        {
            return true;
        }
#elif UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            return true;
        }
#endif
        return false;

    }
}
