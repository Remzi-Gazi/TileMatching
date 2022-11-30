using UnityEngine;

[CreateAssetMenu(fileName = "GameSceneData", menuName ="Scriptable Objects/GameSceneData")]
public class GameScene : ScriptableObject
{
	[Header("Information")]
	#if UNITY_EDITOR
	public UnityEditor.SceneAsset sceneAsset;
	#endif
	[HideInInspector]
	public string scenePath;
	[TextArea] public string shortDescription;

	#if UNITY_EDITOR
	public void OnValidate()
    {
		scenePath = UnityEditor.AssetDatabase.GetAssetPath(sceneAsset);
	}
	#endif
}
