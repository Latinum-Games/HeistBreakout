using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    // Fields 
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float fov;
    [SerializeField] private int rayCount;
    [SerializeField] private float viewDistance;

    private Mesh mesh;
    private Vector3 origin;

    // Start is called before the first frame update
    private void Start() {
        mesh = new Mesh();
        origin = Vector3.zero;
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void LateUpdate() {
        float angle = 0f;
        float angleIncrease = fov / rayCount;

        // RENDER FOV TRIANGLE
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;
        int vertexIndex = 1;
        int triangleIndex = 0;

        for (int i = 0; i <= rayCount; i++) {
            Vector3 vertex;
            RaycastHit2D raycastHit2d = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2d.collider == null) {
                // No hit
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            } else {
                // Hit Object
                vertex = raycastHit2d.point;

            }

            vertices[vertexIndex] = vertex;

            if (i > 0){
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 origin) {
        this.origin = origin;
    }

    // UTILS FROM CODE MONKEY LIBRARY

    public static Vector3 GetVectorFromAngle(float angle) {
        // angle = 0 -> 360
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }


}

