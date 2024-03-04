using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;

public class ResetSo
{
    [MenuItem("Tools/ResetSO")]
    public static void CreateArrowSO()
    {
        if (Selection.activeObject is ArrowSO || Selection.activeObject is InfoSO)
        {
            if (Selection.assetGUIDs.Length > 0)
            {
                for (int i = 0; i < Selection.assetGUIDs.Length; i++)
                {
                    //Project窗口中已選擇的SO
                    var currentObj = Selection.objects[i] as ArrowSO;
                    currentObj.m_size = new Vector3(0.01f, 0.01f, 0.01f);
                }
            }
        }
    }
}
#endif