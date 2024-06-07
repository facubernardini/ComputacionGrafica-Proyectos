using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dado : MonoBehaviour
{
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;
    private GameObject dado1;
    private GameObject dado2;

    public Material materialDado;

    void Start()
    {
        dado1 = new GameObject();
        dado1.name = "Dado1";
        dado1.AddComponent<MeshFilter>();
        dado1.GetComponent<MeshFilter>().mesh = new Mesh();
        dado1.AddComponent<MeshRenderer>();

        CreateModel();
        UpdateMesh();
        CreateMaterial();

        dado2 = Instantiate(dado1, dado1.transform.position, dado1.transform.rotation);
        dado2.name = "Dado2";

        dado1.transform.position = new Vector3(3.9f, 0.844f, 16.872f); // Nueva posicion del dado
        dado1.transform.localScale = new Vector3(0.0314f, 0.0314f, 0.0314f); // Nueva escala del dado

        dado2.transform.position = new Vector3(6f, 0.816f, 16.715f); // Nueva posicion del dado
        dado2.transform.localScale = new Vector3(0.0314f, 0.0314f, 0.0314f); // Nueva escala del dado
    }

    private void CreateModel()
    {
        vertices = new Vector3[]
        {
            new Vector3(0,0,0), //v0
            new Vector3(1,0,0), //v1
            new Vector3(0,0,1), //v2
            new Vector3(1,0,1), //v3

            new Vector3(0,0,1), //v4
            new Vector3(0,1,1), //v5
            new Vector3(0,1,0), //v6
            new Vector3(0,0,0), //v7

            new Vector3(1,0,1), //v8
            new Vector3(1,1,1), //v9
            new Vector3(0,1,1), //v10
            new Vector3(0,0,1), //v11

            new Vector3(1,0,0), //v12
            new Vector3(1,1,0), //v13
            new Vector3(1,1,1), //v14
            new Vector3(1,0,1), //v15

            new Vector3(0,0,0), //v16
            new Vector3(0,1,0), //v17
            new Vector3(1,1,0), //v18
            new Vector3(1,0,0), //v19

            new Vector3(0,1,0), //v20
            new Vector3(0,1,1), //v21
            new Vector3(1,1,1), //v22
            new Vector3(1,1,0), //v23
        };

        triangles = new int[]
        {
           0,1,2,
           1,3,2,

           4,5,7,
           5,6,7,

           8,9,11,
           11,9,10,

           12,13,15,
           13,14,15,

           16,17,19,
           17,18,19,

           20,21,23,
           21,22,23
        };

        uvs = new Vector2[]
        {
            new Vector2(0.25f, 0.333f), //v0
            new Vector2(0.5f, 0.333f), //v1
            new Vector2(0.25f, 0.666f), //v2
            new Vector2(0.5f, 0.666f), //v3

            new Vector2(0.25f, 0.666f), //v4
            new Vector2(0, 0.666f), //v5
            new Vector2(0,0.333f), //v6
            new Vector2(0.25f,0.333f), //v7

            new Vector2(0.5f, 0.666f), //v8
            new Vector2(0.5f, 1), //v9
            new Vector2(0.25f, 1), //v10
            new Vector2(0.25f, 0.666f), //v11

            new Vector2(0.5f, 0.333f), //v12
            new Vector2(0.75f, 0.333f), //v13
            new Vector2(0.75f, 0.666f), //v14
            new Vector2(0.5f, 0.666f), //v15

            new Vector2(0.25f, 0.333f), //v16
            new Vector2(0.25f, 0), //v17
            new Vector2(0.5f, 0), //v18
            new Vector2(0.5f, 0.333f), //v19

            new Vector2(0.75f, 0.333f), //v20
            new Vector2(0.75f, 0.666f), //v21
            new Vector2(1, 0.666f), //v22
            new Vector2(1, 0.333f), //v23
        };
    }

    private void UpdateMesh()
    {
        dado1.GetComponent<MeshFilter>().mesh.vertices = vertices;
        dado1.GetComponent<MeshFilter>().mesh.triangles = triangles;
        dado1.GetComponent<MeshFilter>().mesh.uv = uvs;
    }

    private void CreateMaterial()
    {
        dado1.GetComponent<MeshRenderer>().material = materialDado;
    }
}