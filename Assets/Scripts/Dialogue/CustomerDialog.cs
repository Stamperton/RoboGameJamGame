﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomerDialog
{
    public float dialogTimer = 2f;
    public DialogChoices dialogResponse;
    [TextArea] public string dialogText;

}
