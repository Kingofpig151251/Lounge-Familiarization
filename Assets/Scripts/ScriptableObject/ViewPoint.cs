using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewViewPoint", menuName = "New ViewPoint", order = 0)]
public class ViewPoint : ScriptableObject
{
    public int m_index;
    public Lounge m_loungeName;
    public int[] m_relation;
    public ArrowSO[] m_arrowSO;
    public InfoSO[] m_infoSO;
    public Texture m_texture;
    public float m_rotation;

#if UNITY_EDITOR
    // 3rd step
    // 1st and 2nd step must be done before running this function
    // Ignoring this instruction may cause unexpected behavior

    /*
     * Configure the m_arrowSO[] length of each viewpoint in the inspector
     * Select all viewpoints in the lounge you're working on
     * Find and run this method in the context menu
     *
     * If you don't understand what this is about, don't touch this.
     * Also remove this method in production if you'd like, it won't compile anyway.
     */
    [ContextMenu("Create and link arrow")]
    public void CreateArrow()
    {
        // Configuration not required

        // Get the parent folder name
        var parentFolderName = AssetDatabase.GetAssetPath(this).Split('/').Reverse().Skip(1).First();

        for (var i = 0; i < m_arrowSO.Length; i++)
        {
            var destinationPath =
                $"Assets/ScriptableObject/Arrow/{parentFolderName}/VP{m_index}/VP{m_index} Arrow {i}.asset";
            if (AssetDatabase.LoadAssetAtPath<ArrowSO>(destinationPath) is null)
            {
                var newArrow = CreateInstance<ArrowSO>();
                newArrow.m_size = Vector3.one * .01f;
                AssetDatabase.CreateAsset(newArrow, destinationPath);
            }

            m_arrowSO[i] = AssetDatabase.LoadAssetAtPath<ArrowSO>(destinationPath);
        }
    }

    private void OnValidate()
    {
        if (!Application.isPlaying) return;
        if (ViewPointManager.Instance.m_currentViewPoint.name != name) return;
        ViewPointManager.Instance.m_firstViewPoint.transform.localEulerAngles = new Vector3(
            ViewPointManager.Instance.transform.localEulerAngles.x,
            ViewPointManager.Instance.m_currentViewPoint.m_rotation,
            ViewPointManager.Instance.transform.transform.localEulerAngles.z);
    }
#endif
}