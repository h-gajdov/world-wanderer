using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EarthGenerator : MonoBehaviour
{
    [SerializeField, HideInInspector] private MeshFilter[] filters;
    private TerrainFace[] faces;

    public Texture2D[] heightMaps;
    public float heightMultiplier = 2f;
    public int numberOfFacesPerSide = 4;
    
    [Range(0, 256)]
    [SerializeField] private int resolution = 10;
    [SerializeField] private float radius = 1;
    [SerializeField] private bool generate = false;
    
    public static EarthGenerator instance;
    
    private void FillFaces()
    {
        if(!generate) return;
        
        if(filters == null || filters.Length == 0) filters = new MeshFilter[6 * numberOfFacesPerSide];
        faces = new TerrainFace[6 * numberOfFacesPerSide];
        Vector3[] directions =
        {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back
        };
        
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < numberOfFacesPerSide; j++)
            {
                int faceIndex = j + i * numberOfFacesPerSide;
                if (filters[faceIndex] == null)
                {
                    GameObject meshObj = new GameObject("mesh");
                    meshObj.transform.parent = transform;
                    meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                    filters[faceIndex] = meshObj.AddComponent<MeshFilter>();
                    filters[faceIndex].sharedMesh = new Mesh();   
                }
                
                faces[faceIndex] = new TerrainFace(filters[faceIndex].sharedMesh, resolution, j % (int)Mathf.Sqrt(numberOfFacesPerSide), j / (int)Mathf.Sqrt(numberOfFacesPerSide), radius, directions[i]);
            }
        }
    }

    private void GenerateMesh()
    {
        if (faces == null) return;
        foreach (TerrainFace face in faces) face.ConstructMesh();
    }
    
    private void OnValidate()
    {
        instance = this;
        FillFaces();
        GenerateMesh();
    }
}
