using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LocationPanel : MonoBehaviour, IPanel
{
    public RawImage mapImage;
    public InputField mapNotesInput;

    public string aPIKey;
    public float latCoord, longCoord;
    public int zoom;
    public int imgSize;
    public string uRL = "https://maps.googleapis.com/maps/api/staticmap?";

    private void OnEnable()
    {
        uRL = uRL + "center=" + latCoord + "," + longCoord + "&zoom=" + zoom + "&size=" + imgSize + "x" + imgSize + "&key=" + aPIKey;

        //download static map
        StartCoroutine(GetLocationRoutine());
        //apply map to rawImage
        //
    }

    IEnumerator GetLocationRoutine()
    {
        using (UnityWebRequest map = UnityWebRequestTexture.GetTexture(uRL))
        {
            yield return map.SendWebRequest();

            if (map.result == UnityWebRequest.Result.ConnectionError || map.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Map Error: " + map.error);
            }

            mapImage.texture = DownloadHandlerTexture.GetContent(map);
        }
    }

    public void ProcessInfo()
    {

    }

}
