using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overview : MonoBehaviour, IPanel
{
    public Text caseNumberText;
    public Text nameText;
    public Text dateText;
    public Text locationNotes;
    public RawImage photo;
    public Text photoNotes;

    public void OnEnable()
    {
        caseNumberText.text = "CASE NUMBER " + UIManager.Instance.activeCase.caseID;
        nameText.text = UIManager.Instance.activeCase.clientName;
        dateText.text = UIManager.Instance.activeCase.date;
        locationNotes.text = "LOCATION NOTES: \n" + UIManager.Instance.activeCase.locationNotes;
        //rebuild photo and display it

        //convert bytes to png
        //convert texture2D to texture

        Texture2D reconstructedImage = new Texture2D(1, 1);
        reconstructedImage.LoadImage(UIManager.Instance.activeCase.photoData);
        Texture image = (Texture)reconstructedImage;

        photo.texture = image;
        photoNotes.text = "PHOTO NOTES: \n" + UIManager.Instance.activeCase.photoNotes;
    }

    public void ProcessInfo()
    {

    }

}