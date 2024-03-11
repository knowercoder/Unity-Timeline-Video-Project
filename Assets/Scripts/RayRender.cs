using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class RayRender : MonoBehaviour
{
    public Vector3 RayEndPos;
    public Vector3 NormalVector;

    [SerializeField] float lineThickness = 0.01f;

    Color lineColor = Color.red;

    void Start()
    {
        //Rayrender();
    }

    public void Rayrender()
    {        
        var lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.startWidth = lineThickness;
        lineRenderer.endWidth = lineThickness;

        
        lineRenderer.positionCount = 3;
        lineRenderer.SetPosition(0, transform.position);
        StartCoroutine(GrowLine(lineRenderer, transform.position, RayEndPos));
    }

    IEnumerator GrowLine(LineRenderer line, Vector3 startPoint, Vector3 endPoint)
    {
        float elapsedTime = 0f;

        while (elapsedTime < 1.5f)
        {            
            line.SetPosition(1, Vector3.Lerp(startPoint, endPoint, elapsedTime));
            line.SetPosition(2, Vector3.Lerp(startPoint, endPoint, elapsedTime));
            elapsedTime += Time.deltaTime * 0.5f;
            yield return null;
        }        
        line.SetPosition(1, endPoint);

        elapsedTime = 0f;
        Vector3 reflectPoint = ReflectPoint(endPoint);
        while (elapsedTime < 1.5f)
        {
            line.SetPosition(2, Vector3.Lerp(endPoint, reflectPoint, elapsedTime));
            elapsedTime += Time.deltaTime * 0.5f;
            yield return null;
        }
        line.SetPosition(2, reflectPoint);
    }

    Vector3 ReflectPoint(Vector3 point)
    {
        // Calculate the reflection of the point with respect to the normal vector
        Vector3 direction = point - transform.position;
        Vector3 reflection = direction - 2 * Vector3.Dot(direction, NormalVector) * NormalVector;
        return point + reflection * 0.5f;
    }
    
}
