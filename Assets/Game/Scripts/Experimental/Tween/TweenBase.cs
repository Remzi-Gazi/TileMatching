using UnityEngine;

public abstract class TweenBase : ScriptableObject 
{
    [TextArea] public string description;

#if UNITY_EDITOR
    public abstract void SetDefaults();
#endif
}