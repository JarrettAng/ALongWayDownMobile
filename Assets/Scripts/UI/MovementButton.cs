using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementButton : Button
{
    public override void PlayButtonClick() {
        SoundManager.Instance.PlaySound("MovementButtonClick");
    }
}
