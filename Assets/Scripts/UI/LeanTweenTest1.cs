using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTweenTest1 : MonoBehaviour {
    private void Start() {
        transform.localScale = Vector2.zero;
    }

    public void Open() {
        LeanTween.scale(gameObject, new Vector3(1.7f, 1.7f, 1.7f), 0.5f).setEase(LeanTweenType.easeOutBounce);
    }

    public void Close() {
        LeanTween.scale(gameObject, new Vector3(0f, 0f, 0f), 0.5f).setEase(LeanTweenType.easeOutBounce);
    }
}
