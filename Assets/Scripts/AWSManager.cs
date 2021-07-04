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

    public string S3Region = RegionEndpoint.USEast2.SystemName;
    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }
    void Awake()
    {
        _instance = this;

        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        S3Client.ListBucketsAsync(new ListBucketsRequest(), (responseObject) =>
        {
            if (responseObject.Exception == null)
            {
                responseObject.Response.Buckets.ForEach((s3b) =>
                {
                    Debug.Log("Bucket Name: " + s3b.BucketName);
                });
            }
            else
            {
                Debug.Log("AWS Error:" + responseObject.Exception);
            }
        });
    } 

    public void UploadToS3(string fileName)
    {
        FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

        PostObjectRequest request = new PostObjectRequest()
        {
            Bucket = "casefiles11212",
            Key = fileName,
            InputStream = stream,
            CannedACL = S3CannedACL.Private,
            Region = RegionEndpoint.USEast2
        };

        S3Client.PostObjectAsync(request, (responseObj) => 
        {
            if (responseObj.Exception == null)
                Debug.Log("Successfully posted to bucket");
            else
                Debug.Log("Exception occurred during upload: " + responseObj.Exception);
        });
    }
}
