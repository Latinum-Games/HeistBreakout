using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    // Fields 
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float fov;
    [SerializeField] private int rayCount;
    [SerializeField] private float viewDistance;

    private Mesh _mesh;
    private Vector3 _origin;

    // Start is called before the first frame update
    private void Start() {
        _mesh = new Mesh();
        _origin = Vector3.zero;
        GetComponent<MeshFilter>().mesh = _mesh;
    }

    private void FixedUpdate() {
        var angle = 0f;
        var angleIncrease = fov / rayCount;

        // RENDER FOV TRIANGLE
        var vertices = new Vector3[rayCount + 1 + 1];
        var uv = new Vector2[vertices.Length];
        var triangles = new int[rayCount * 3];

        vertices[0] = _origin;
        var vertexIndex = 1;
        var triangleIndex = 0;

        for (var i = 0; i <= rayCount; i++) {
            Vector3 vertex;
            RaycastHit2D raycastHit2d = Physics2D.Raycast(_origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            
            if (raycastHit2d.collider == null) {
                // No hit
                vertex = _origin + GetVectorFromAngle(angle) * viewDistance;
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

        _mesh.vertices = vertices;
        _mesh.uv = uv;
        _mesh.triangles = triangles;
        
        _mesh.RecalculateBounds();
        _mesh.RecalculateBounds();
    }

    public void SetOrigin(Vector3 origin) {
        this._origin = origin;
    }

    // UTILS FROM CODE MONKEY LIBRARY
    private static Vector3 GetVectorFromAngle(float angle) {
        // angle = 0 -> 360
        var angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}

