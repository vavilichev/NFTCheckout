using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NFTCheckout))]
public class NFTCheckoutEditor : Editor
{
    private SerializedProperty chainProperty;
    private SerializedProperty networkProperty;
    private SerializedProperty accountProperty;
    private SerializedProperty countractProperty;
    private SerializedProperty tokenIdProperty;
    private SerializedProperty meshRendererProperty;

    private void OnEnable()
    {
        chainProperty = serializedObject.FindProperty("chain");
        networkProperty = serializedObject.FindProperty("network");
        accountProperty = serializedObject.FindProperty("account");
        countractProperty = serializedObject.FindProperty("contract");
        tokenIdProperty = serializedObject.FindProperty("tokenId");
        meshRendererProperty = serializedObject.FindProperty("meshRenderer");
    }

    public override void OnInspectorGUI()
    {
        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour((NFTCheckout)target), typeof(NFTCheckout), false);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(chainProperty);
        EditorGUILayout.PropertyField(networkProperty);
        GUI.enabled = true;
        
        EditorGUILayout.PropertyField(accountProperty);
        EditorGUILayout.PropertyField(countractProperty);
        EditorGUILayout.PropertyField(tokenIdProperty);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(meshRendererProperty);

        serializedObject.ApplyModifiedProperties();
    }
}