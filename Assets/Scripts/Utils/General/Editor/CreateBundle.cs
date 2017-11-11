using UnityEngine;
using UnityEditor;

public class CreateBundle : MonoBehaviour
{
    // create bundle
    [MenuItem("Assets/Build Asset Bundles/Android")]
    static void BuildAsset()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.Android);
    }
}