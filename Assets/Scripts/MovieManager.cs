using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.Rendering.HighDefinition;

public class MovieManager : MonoBehaviour
{
    [Header("Timeline")]
    [SerializeField] PlayableDirector Timeline1;
    [SerializeField] PlayableDirector Timeline2;
    [SerializeField] bool[] PlayTimeline;

    [Header("Light Ray Interactions")]
    [SerializeField] GameObject SphereObject;
    [SerializeField] GameObject VectorArrowPrefab;
    [SerializeField] RayRender[] Rayrenderers;

    [Header("Materials")]
    [SerializeField] Material myUnlit_Mat;
    [SerializeField] Material Unlit_Fresnel_Mat;

    List<GameObject> VectorArrows =  new List<GameObject>();

    //int[] vectorints = new int[] { 210,  272 , 236, };
    int[] vectorints = new int[] { 275, 211, 121, }; //241

    private void Awake()
    {
        //Play Timeline
        if (PlayTimeline[0])
            Timeline1.Play();
        else if (PlayTimeline[1])
            Timeline2.Play();
    }

    private void Start()
    {        

        //Invoke("SetNormalVectors", 2);
        //Invoke("RenderRay", 2);
    }   

    public void SetNormalVectors()
    {
        MeshFilter meshFilter = SphereObject.GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices = mesh.vertices;
            Vector3[] normals = mesh.normals;

            if (vertices.Length == normals.Length)
            {
                Debug.Log("Normal Length: " + normals.Length);
                for (int i = 0; i < vertices.Length; i++)
                {
                    Vector3 vertex = SphereObject.transform.TransformPoint(vertices[i]); // Transform the vertex to world space
                    Vector3 normal = SphereObject.transform.TransformDirection(normals[i]); // Transform the normal to world space

                    Debug.Log("Normal " + i + " " + vertex + " " + normal);
                    InstantiatePrefabAtVertex(vertex, normal, i);
                }
            }
            else
            {
                Debug.LogError("Number of vertices and normals do not match.");
            }
            
        }
        else
        {
            Debug.LogError("MeshFilter component not found on the GameObject.");
        }
        
    }

    void InstantiatePrefabAtVertex(Vector3 position, Vector3 normal, int aint)
    {
        GameObject instantiatedObject = Instantiate(VectorArrowPrefab, position, Quaternion.identity);
        instantiatedObject.transform.rotation = Quaternion.LookRotation(normal);

        instantiatedObject.GetComponent<ArrowInt>().arrowint = aint;
        VectorArrows.Add(instantiatedObject);
        instantiatedObject.transform.DOScale(1.5f, 0.75f).SetEase(Ease.OutFlash);
    }

    public void EnableNormalVectors()
    {
        for(int i = 0; i < VectorArrows.Count; i++)
        {
           if(i != vectorints[0] && i != vectorints[1] && i != vectorints[2])
                VectorArrows[i].SetActive(false);
           else
                VectorArrows[i].SetActive(true);
        }
    }    

    public void RenderRay1()
    {
        Rayrenderers[0].RayEndPos = new Vector3(9.21f, 9.50f, 12.89f);
        Rayrenderers[0].NormalVector = new Vector3(0.41f, 0.90f, -0.13f);
        Rayrenderers[0].Rayrender();
    }

    public void RenderRay2()
    {
        Rayrenderers[1].RayEndPos = new Vector3(9.46f, 9.29f, 12.93f);
        Rayrenderers[1].NormalVector = new Vector3(0.83f, 0.55f, -0.06f);
        Rayrenderers[1].Rayrender();
    }

    public void RenderRay3()
    {
        Rayrenderers[2].RayEndPos = new Vector3(8.99f, 9.54f, 12.85f);
        Rayrenderers[2].NormalVector = new Vector3(0.03f, 0.98f, -0.20f);
        Rayrenderers[2].Rayrender();
    }

    public void ChangeSphereMaterial1()
    {
        SphereObject.GetComponent<MeshRenderer>().material = myUnlit_Mat;
    }

    public void ChangeSphereMaterial2()
    {
        SphereObject.GetComponent<MeshRenderer>().material = Unlit_Fresnel_Mat;
    }

    public void Disableray()
    {
        foreach (RayRender renderer in Rayrenderers)
        {
            renderer.gameObject.SetActive(false);
        }

        for (int i = 0; i < VectorArrows.Count; i++)
        {           
            VectorArrows[i].SetActive(false);
        }
    }

    public void AnimateFresnelPower()
    {
        StartCoroutine(AnimateFresnelPower_COR());
    }

    IEnumerator AnimateFresnelPower_COR()
    {
        float pow = 1;
        while (pow <= 4)
        {
            Unlit_Fresnel_Mat.SetFloat("_Power", pow);
            pow += Time.deltaTime * 0.5f;
            yield return null;
        }
    }
}
