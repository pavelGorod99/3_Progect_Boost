using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (MeshFilter))]
public class Triangle : MonoBehaviour
{
    [SerializeField] Material mat;

    //float width = 5;
    //float height = 5;s

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;
      
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
        //Mesh mesh = new Mesh();

        //Vector3[] vertices = new Vector3[4];

        //vertices[0] = new Vector3(-width, -height);
        //vertices[1] = new Vector3(-width, height);
        //vertices[2] = new Vector3( width, height);
        //vertices[3] = new Vector3( width, -height);

        //mesh.vertices = vertices;

        //mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };

        //GetComponent<MeshRenderer>().material = mat;

        //GetComponent<MeshFilter>().mesh = mesh;
    }

    void CreateShape()
    {
        vertices = new Vector3[]
        {
            new Vector3 (0, 0, 0),
            new Vector3 (0, 0, 1),
            new Vector3 (1, 0, 0),
            new Vector3 (1, 0, 1)
        };

        triangles = new int[]
        {
            0, 1, 2, 
            1, 3, 2
        };
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
