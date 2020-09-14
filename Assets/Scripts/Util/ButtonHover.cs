using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        anim.SetBool("Hovering", true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        anim.SetBool("Hovering", false);
    }
}
