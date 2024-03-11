using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimmyFace : MonoBehaviour
{
    [SerializeField] Material TimmyMat;

    [Header("Textures")]
    [SerializeField] Texture NormalFaceTex;    
    [SerializeField] Texture SmileTex;
    [SerializeField] Texture OpenSmileFaceTex;
    [SerializeField] Texture SurpriseTex;

    void Start()
    {
        //if (TimmyMat.HasProperty("_BaseColorMap"))
        //{            
        //    OpenSmileFace();            
        //}
        //else
        //{
        //    Debug.LogWarning("The material does not have an _BaseColorMap property. Ensure it is using an HDRP shader with albedo texture support.");
        //}
        
    }

    public void NormalFace()
    {
        TimmyMat.SetTexture("_BaseColorMap", NormalFaceTex);
    }

    public void OpenSmileFace()
    {
        TimmyMat.SetTexture("_BaseColorMap", OpenSmileFaceTex);
    }

    public void SmileFace()
    {
        TimmyMat.SetTexture("_BaseColorMap", SmileTex);
    }

    public void SurpriseFace()
    {
        TimmyMat.SetTexture("_BaseColorMap", SurpriseTex);
    }

}
