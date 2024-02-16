using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
#if UNITY_EDITOR
using Unity.VisualScripting;
using UnityEditor;

public class Test1
{
    [MenuItem("Tools/CreateSO/Arrow")]
    public static void CreateArrowSO()
    {
        ArrowSO testData = ScriptableObject.CreateInstance<ArrowSO>();
        testData.m_position = Selection.activeTransform.gameObject.transform.position;
        testData.m_rotation = Selection.activeTransform.gameObject.transform.localEulerAngles;
        testData.m_size = Selection.activeTransform.gameObject.transform.localScale;
        testData.m_nextViewPointIndex = Selection.activeTransform.gameObject.GetComponent<InterfaceItem_Arrow>().m_nextViewPointIndex;

        string fullPath = "Assets/ScriptableObject/Arrow/VP.asset";
        UnityEditor.AssetDatabase.DeleteAsset(fullPath);
        UnityEditor.AssetDatabase.CreateAsset(testData, fullPath);
        UnityEditor.AssetDatabase.Refresh();
    }

    [MenuItem("GameObject/CreateSO/Info")]
    public static void CreateInfoSO()
    {
        InfoSO testData = ScriptableObject.CreateInstance<InfoSO>();
        testData.m_position = Selection.activeTransform.gameObject.transform.position;
        testData.m_rotation = Selection.activeTransform.gameObject.transform.localEulerAngles;
        testData.m_size = Selection.activeTransform.gameObject.transform.localScale;
        testData.m_title_ENG = Selection.activeTransform.gameObject.GetComponent<InterfaceItem_Info>().m_info.m_title_ENG;
        testData.m_title_SC = Selection.activeTransform.gameObject.GetComponent<InterfaceItem_Info>().m_info.m_title_SC;
        testData.m_title_TC = Selection.activeTransform.gameObject.GetComponent<InterfaceItem_Info>().m_info.m_title_TC;
        testData.m_content_ENG = Selection.activeTransform.gameObject.GetComponent<InterfaceItem_Info>().m_info.m_content_ENG;
        testData.m_content_SC = Selection.activeTransform.gameObject.GetComponent<InterfaceItem_Info>().m_info.m_content_SC;
        testData.m_content_TC = Selection.activeTransform.gameObject.GetComponent<InterfaceItem_Info>().m_info.m_content_TC;

        string fullPath = "Assets/ScriptableObject/Info/VP.asset";
        UnityEditor.AssetDatabase.DeleteAsset(fullPath);
        UnityEditor.AssetDatabase.CreateAsset(testData, fullPath);
        UnityEditor.AssetDatabase.Refresh();
    }

    // 1st step 
    // Modify and run this function to create ViewPoints
    [MenuItem("GameObject/CreateSO/1. Create view points (CONFIGURE BEFORE RUNNING)")]
    public static void CreateViewPointsAuto()
    {
        // Configurations
        const int viewPointCount = 21; // Largest number in the photo folder
        const string loungeName = "Pier_Business";
        const string assetNamePrefix = "Pier_Business VP ";
        const string photoNamePrefix = "The Pier B ";
        const Lounge lounge = Lounge.PierBusinessLounge;
        
        // Assets creation
        for (var i = 0; i <= viewPointCount; i++)
        {
            var newViewpoint = ScriptableObject.CreateInstance<ViewPoint>();
            var destinationPath = $"Assets/ScriptableObject/ViewPoints/{loungeName}/{assetNamePrefix}{i}.asset";
            var texturePath = $"Assets/Photo/{loungeName}/{photoNamePrefix}{i + 1}.png";
            
            // Set texture
            newViewpoint.m_index = i;
            newViewpoint.m_loungeName = lounge;
            var texture = AssetDatabase.LoadAssetAtPath<Texture>(texturePath);
            if (texture is null)
            {
                Debug.Log("Texture not found");
                continue; // Skip the item if the texture is missing (i.e. A number is skipped)
            }
            newViewpoint.m_texture = AssetDatabase.LoadAssetAtPath<Texture>(texturePath);
            
            // Add to asset database
            AssetDatabase.CreateAsset(newViewpoint, destinationPath);
        }
    }
    
    // 2nd step
    // Modify and run this function to Create arrow objects after the viewpoints are created
    [MenuItem("GameObject/CreateSO/2. Create Arrow folders (CONFIGURE BEFORE RUNNING)")]
    public static void CreateFoldersAuto()
    {
        // Configurations
        const int viewPointCount = 21;
        const string destinationPath = "Assets/ScriptableObject/Arrow/Pier_Business";

        // Folders creation
        for (var i = 0; i < viewPointCount; i++)
        {
            AssetDatabase.CreateFolder(destinationPath, $"VP{i}");
        }
    }
}
#endif