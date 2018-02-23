using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAudio : MonoBehaviour
{
    public void OnHover()
    {
        AudioController.Instance.Play("ButtonHover");
    }

    public void OnClick()
    {
        AudioController.Instance.Play("ButtonClick");
    }

}
