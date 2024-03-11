using UnityEngine;

public class CharacterMeshInteraction : MonoBehaviour
{
    public LayerMask meshLayer; // Set this in the inspector to the layer your mesh is on

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, meshLayer))
        {
            float meshHeight = hit.point.y;
            Debug.Log(hit.point.y);
            transform.position = new Vector3(transform.position.x, meshHeight + 0.1f, transform.position.z);
        }
    }
}
