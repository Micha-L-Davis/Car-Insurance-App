using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LocationPanel : MonoBehaviour, IPanel
{
    public RawImage mapImage;
    public InputField mapNotesInput;

    public string keyFrag0;
    public string keyFrag1;
    public string keyFrag2;
    public float latCoord, longCoord;
    public int zoom;
    public int imgSize;
    public string uRL = "https://maps.googleapis.com/maps/api/staticmap?";

    private IEnumerator Start()
    {
        //fetch geo data
        if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1.0f);
                maxWait--;
            }

            if (maxWait < 1)
            {
                Debug.Log("Timed Out");
                yield break;
            }

            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("Unable to determine device location");
            }
            else
            {
                latCoord = Input.location.lastData.latitude;
                longCoord = Input.location.lastData.longitude;
            }

            Input.location.Stop();
        }
        else
        {
            Debug.Log("Location Services are not enabled");
        }
        StartCoroutine(GetLocationRoutine());
    }

    IEnumerator GetLocationRoutine()
    {
        uRL = uRL + "center=" + latCoord + "," + longCoord + "&zoom=" + zoom + "&size=" + imgSize + "x" + imgSize + "&key=" + keyFrag2 + keyFrag1 + keyFrag0;
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
        if (!string.IsNullOrEmpty(mapNotesInput.text))
        {
            UIManager.Instance.activeCase.locationNotes = mapNotesInput.text;
        }
    }

}
