﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public virtual void PlayButtonClick() {
        SoundManager.Instance.PlaySound("ButtonClick");
    }
}
