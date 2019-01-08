using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SideMenuController : MonoBehaviour
{
    public float speed = 10;

    public Image shadowTint;
    public Button closeButton;

    private Slider slider;
    private Color tintColor;
    private int isAnimation = 0;

    private void Start()
    {
        slider = GetComponent<Slider>();
        tintColor = new Color(0, 0, 0, 0);
    }

    private void Update()
    {
        //sideMenu between opened and closed state
        if (slider.value > 0 && slider.value < 1)
        {
            shadowTint.enabled = true;
            tintColor.a = slider.value / 2;
            shadowTint.color = tintColor;

            if (isAnimation != 0)
            {
                slider.value = (isAnimation > 0) ? slider.value + speed * Time.deltaTime : slider.value - speed * Time.deltaTime;
                Debug.Log("Animation " + slider.value );
            }
            else
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
                else // Touched in current time
                {
                    Debug.Log("Touched");
                }
            }
        }
        //sideMenu in closed state
        else if (slider.value == 0)
        {
            shadowTint.enabled = false;
            closeButton.enabled = false;
            isAnimation = 0;
        }
        //sideMenu in opened state
        else if (slider.value == 1)
        {
            closeButton.enabled = true;
            isAnimation = 0;
        }
        else
        {
            closeButton.enabled = false;
            isAnimation = 0;
        }

      
        
    }

    public void StartHiding()
    {
        slider.value = 0.9f;
        isAnimation = -1;
        closeButton.enabled = false;
    }

    public void StartOpening()
    {
        slider.value = 0.1f;
        isAnimation = 1;
        Debug.Log("Openning");
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
