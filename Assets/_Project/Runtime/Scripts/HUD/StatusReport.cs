using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusReport : MonoBehaviour
{
    [SerializeField] private Image _inversion;
    [SerializeField] private Image _double;
    // Update is called once per frame
    public void InverseReveal()
    {
        FindObjectOfType<AudioManager>().Play("sfx_bonus");
        _inversion.color = new Color(_inversion.color.r, _inversion.color.g, _inversion.color.b, 255);
    }
    public void InverseDisappear()
    {
        _inversion.color = new Color(_inversion.color.r, _inversion.color.g, _inversion.color.b, 0);
    }
    public void DoubleReveal()
    {
        FindObjectOfType<AudioManager>().Play("sfx_bonus");
        _double.color = new Color(_inversion.color.r, _inversion.color.g, _inversion.color.b, 255);
    }
    public void DoubleDisappear()
    {
        _double.color = new Color(_inversion.color.r, _inversion.color.g, _inversion.color.b, 0);
    }

}
