using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;


public class DownloadAssetBundle : MonoBehaviour
{
    public GameObject model = null;

    private void Start()
    {
        StartCoroutine(DownloadAssetBundleFromServer());
    }

    private IEnumerator DownloadAssetBundleFromServer()
    {
        string url = "https://drive.google.com/uc?export=download&id=1E5iElFTCkavKaouWtg25Cu5hmxcCktVL";
        using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error on the get request at : " + url + " " + www.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                model = bundle.LoadAsset(bundle.GetAllAssetNames()[0]) as GameObject;
                bundle.Unload(false);
                yield return new WaitForEndOfFrame();
            }
            www.Dispose();
            
        }
    }

    public GameObject InstantiateGameObjFromServer()
    {
        if (model != null)
        {
            return model;
        }
        else
        {
            Debug.LogWarning("AssetBundle Not downloaded");
            return null;
        }
    }
}
