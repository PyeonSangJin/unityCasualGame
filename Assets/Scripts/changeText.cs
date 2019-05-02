﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeText : MonoBehaviour
{
    public Text text;
    public Slider hpBar;

    private void Awake()
    {
        text.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = (hpBar.value * 100).ToString() + "%";
    }
}
