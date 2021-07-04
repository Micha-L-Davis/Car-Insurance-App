using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoPanel : MonoBehaviour, IPanel
{
    public RawImage photoTaken;
    public InputField photoNotes;
    public GameObject overviewPanel;
    string imagePath;
    public void ProcessInfo()
    {
        //create a 2D texture
        //apply texture from image path
        //encode png
        //store bytes to photodata(active case)

        byte[] imageData = null;

        if (!string.IsNullOrEmpty(imagePath))
        {
            Texture2D img = NativeCamera.LoadImageAtPath(imagePath, 512, false);
            imageData = img.EncodeToPNG();
        }


        UIManager.Instance.activeCase.photoData = imageData;
        UIManager.Instance.activeCase.photoNotes = photoNotes.text;
    }
    public void TakePictureButton()
    {
        TakePicture(400);
    }

    public void NextButton()
    {
        overviewPanel.SetActive(true);
    }
    private void TakePicture(int maxSize)
    {
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create a Texture2D from the captured image
                Texture2D texture = NativeCamera.LoadImageAtPath(path, maxSize, false);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                photoTaken.texture = texture;
                photoTaken.gameObject.SetActive(true);
                imagePath = path;
            }
        }, maxSize);

        Debug.Log("Permission result: " + permission);
    }

}