using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using System.Diagnostics;
using System.Threading;
using System;

public class BundleLoader : MonoBehaviour
{
    private string path;

    private AssetBundle currentLoadedAssetBundle;
    // Start is called before the first frame update
    //  private AssetBundle loadedAssetBundle;
    void Start()
    {
        path = Path.Combine(Application.streamingAssetsPath, "prefabs");

        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        currentLoadedAssetBundle = LoadAssetBundle(path);
        stopWatch.Stop();
        // Get the elapsed time as a TimeSpan value.
        TimeSpan ts = stopWatch.Elapsed;

        // Format and display the TimeSpan value.
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                            ts.Hours, ts.Minutes, ts.Seconds,
                                            ts.Milliseconds / 10);
        UnityEngine.Debug.Log("RunTime " + elapsedTime);
     //   currentLoadedAssetBundle = LoadAssetBundle(path);
    
    
    }

    private AssetBundle LoadAssetBundle(string bundleUrl)
    {
             var bundleLoadRequest = AssetBundle.LoadFromFileAsync(bundleUrl);
             AssetBundle loadedAssetBundle = bundleLoadRequest.assetBundle;
       // AssetBundle loadedAssetBundle = AssetBundle.LoadFromFile(bundleUrl);
        if (loadedAssetBundle == null)
        {
            UnityEngine.Debug.Log("Failed to load AssetBundle!");
        }
        else
        {
            UnityEngine.Debug.Log("AssetBundle succesfully loaded from" + bundleUrl);
        }
        return loadedAssetBundle;
    }

    public AssetBundle GetLoadedBundle() //const
    {
        return currentLoadedAssetBundle;
    }

    // Update is called once per frame
    void Update()
    {

    }
}