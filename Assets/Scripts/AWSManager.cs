using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using System.IO;
using System;
using Amazon.S3.Util;
using System.Collections.Generic;
using Amazon.CognitoIdentity;
using Amazon;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

public class AWSManager : MonoBehaviour
{
    private static AWSManager _instance;
    public static AWSManager Instance
    {
        get
        {
            if (_instance == null)
                throw new UnityException("AWS Manager is NULL");
            return _instance;
        }
    }

    private AmazonS3Client _s3Client;
    public AmazonS3Client S3Client
    {
        get
        {
            if (_s3Client == null)
                _s3Client = new AmazonS3Client(new CognitoAWSCredentials("us-east-2:50315d2e-251d-4ec2-b4f7-85be6bec04b1", RegionEndpoint.USEast2), _S3Region);
            return _s3Client;
        }
    }

    private string S3Region = RegionEndpoint.USWest2.SystemName;
    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }
    void Awake()
    {
        _instance = this;

        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;
    } 

    public void UploadToS3(string path, string caseID)
    {
        FileStream stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

        PostObjectRequest request = new PostObjectRequest()
        {
            Bucket = "casefiles11212",
            Key = "case#" + caseID,
            InputStream = stream,
            CannedACL = S3CannedACL.Private,
            Region = _S3Region
        };

        S3Client.PostObjectAsync(request, (responseObj) => 
        {
            if (responseObj.Exception == null)
            {
                Debug.Log("Successfully posted to bucket");
                SceneManager.LoadScene(0);
            }

            else
                Debug.Log("Exception occurred during upload: " + responseObj.Exception);
        });
    }

    public void GetList(string caseNumber)
    {
        string target = "case#" + caseNumber;
        ListObjectsRequest request = new ListObjectsRequest()
        {
            BucketName = "casefiles11212"
        };

        S3Client.ListObjectsAsync(request, (responseObject) =>
        {
            if (responseObject.Exception == null)
            {
                bool caseFound = responseObject.Response.S3Objects.Any(obj => obj.Key == target);

                if (caseFound == true)
                {
                    Debug.Log("Case Found!");
                    S3Client.GetObjectAsync("casefiles11212", target, (responseObj) =>
                    {
                        //read data and apply to case

                        //check if responsestream is null
                        if (responseObj.Response != null)
                        {
                            //byte array to store data from file
                            byte[] data = null;

                            //use stream reader to read response data
                            using (StreamReader reader = new StreamReader(responseObj.Response.ResponseStream))
                            {
                                //access a memory stream
                                using (MemoryStream memory = new MemoryStream())
                                {
                                    //populate data byte array with memory stream data
                                    byte[] buffer = new byte[512];
                                    var bytesRead = default(int);

                                    while ((bytesRead = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        memory.Write(buffer, 0, bytesRead);
                                    }
                                    data = memory.ToArray();
                                }
                            }

                            //convert bytes to a case object
                            using (MemoryStream memory = new MemoryStream(data))
                            {
                                BinaryFormatter bf = new BinaryFormatter();
                                Case downloadedCase = (Case)bf.Deserialize(memory);
                                Debug.Log("Downloaded Case ID: " + downloadedCase.caseID);
                                UIManager.Instance.activeCase = downloadedCase;
                            }
                        }
                    });
                }
                else
                {
                    Debug.Log("Case not found!");
                }
            }
            else
                Debug.Log("Error getting list of items from S3: " + responseObject.Exception);
        });
    }
}
