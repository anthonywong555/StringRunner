using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VibrationString))]
public class VibrationStringEditor : Editor
{
    public override void OnInspectorGUI ()
    { 
        base.OnInspectorGUI();

        if (!Application.isPlaying)
        {
            VibrationString vS = target as VibrationString;

            vS.CreateWater();
        }
    }
}
