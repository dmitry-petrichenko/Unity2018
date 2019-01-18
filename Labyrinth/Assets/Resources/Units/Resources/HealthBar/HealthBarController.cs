using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image HealthBar;

    public void Set(float value)
    {
        HealthBar.fillAmount = value;
    }
}
