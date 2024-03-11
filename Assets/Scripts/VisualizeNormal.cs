using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeNormal : MonoBehaviour
{
    [SerializeField] GameObject VectorArrow;
    [SerializeField] GameObject MainObject;

    void Start()
    {        
        SetNormalVectors();
    }   

    void SetNormalVectors()
    {
        MeshFilter meshFilter = MainObject.GetComponent<MeshFilter>();

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
                    Vector3 vertex = MainObject.transform.TransformPoint(vertices[i]); // Transform the vertex to world space
                    Vector3 normal = MainObject.transform.TransformDirection(normals[i]); // Transform the normal to world space

                    Debug.Log("Normal " + i + " " + vertex);
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
        GameObject instantiatedObject = Instantiate(VectorArrow, position, Quaternion.identity);        
        instantiatedObject.transform.rotation = Quaternion.LookRotation(normal);

        instantiatedObject.GetComponent<ArrowInt>().arrowint = aint;
    }

    private void EnableVector(int i)
    {
        
    }
}
