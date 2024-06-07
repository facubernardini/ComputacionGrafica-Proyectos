using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

public class FileReader : MonoBehaviour
{
    private Vector3[] vertices;
    private int[] caras;
    private Color[] color;
    private GameObject objeto;
    private float vertmaxx, vertmaxy, vertmaxz, vertminx, vertminy, vertminz;
    private int cantVertices = 0;
    private int cantCaras = 0;
    private int cantLineas;

    public void leer(String fileName)
    {
        string path = "Assets/Modelos3d/" + fileName + ".obj";

        StreamReader reader = new StreamReader(path);
        string fileData = (reader.ReadToEnd());

        ReadEachLine(fileData);

        reader.Close();

        objeto = new GameObject(fileName);
        objeto.AddComponent<MeshFilter>();
        objeto.GetComponent<MeshFilter>().mesh = new Mesh();
        objeto.AddComponent<MeshRenderer>();
        UpdateMesh(objeto);
        CreateMaterial(objeto);
    }

    public GameObject getObjeto() { 

        return objeto;
    }

    void ReadEachLine(string fileData)
    {
        string[] lines = fileData.Split('\n');

        cantLineas = lines.Length;

        // Cuento la cantidad de vertices y caras que tiene mi modelo
        
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("v ")) //Es un vertice
            {
                cantVertices++; 
            }

            if (lines[i].StartsWith("f ")) //Es una cara
            {
                cantCaras++;
            }
        }

        // Asigno el mismo color a cada vertice
        color = new Color[cantVertices];

        for (int i = 0; i < cantVertices; i++)
        {
            color[i] = new Color(0.5f, 0.5f, 0.5f, 1);
        }

        // Guardo las coordenadas de los vertices
        vertices = new Vector3[cantVertices];
        int punteroVertices = 0;

        bool flag = true; // Flag para guardar las componentes del primer vertice encontrado en el archivo obj (solo una vez)

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("v ")) //Vertices
            {
                string[] coordenadas = lines[i].Split(' ');

                float x = float.Parse(coordenadas[1], CultureInfo.InvariantCulture);
                float y = float.Parse(coordenadas[2], CultureInfo.InvariantCulture);
                float z = float.Parse(coordenadas[3], CultureInfo.InvariantCulture);

                if (flag)
                {
                    flag = false;

                    vertminx = x;
                    vertminy = y;
                    vertminz = z;

                    vertmaxx = x;
                    vertmaxy = y;
                    vertmaxz = z;
                } else
                {
                    if (x < vertminx) { vertminx = x; }
                    if (x > vertmaxx) { vertmaxx = x; }
                    if (y < vertminy) { vertminy = y; }
                    if (y > vertmaxy) { vertmaxy = y; }
                    if (z < vertminz) { vertminz = z; }
                    if (z > vertmaxz) { vertmaxz = z; }
                }

                vertices[punteroVertices++] = new Vector3(x,y,z);
            }
        }

        float restax = (vertminx + vertmaxx) / 2;
        float restay = (vertminy + vertmaxy) / 2;
        float restaz = (vertminz + vertmaxz) / 2;
        
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].x = vertices[i].x - restax;
            vertices[i].y = vertices[i].y - restay;
            vertices[i].z = vertices[i].z - restaz;
        }

        // Guardo los vertices en el orden correcto, que luego formaran los triangulos
        caras = new int[cantCaras * 3];
        int punteroCaras = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].StartsWith("f ")) //Caras
            {
                string[] cara = lines[i].Split(' '); //Separo los vertices

                string[] verticesCaras = cara[1].Split('/'); //Separo v, vt y vn
                caras[punteroCaras] = int.Parse(verticesCaras[0]) - 1;
                punteroCaras++;

                verticesCaras = cara[2].Split('/'); //Separo v, vt y vn
                caras[punteroCaras] = int.Parse(verticesCaras[0]) - 1;
                punteroCaras++;

                verticesCaras = cara[3].Split('/'); //Separo v, vt y vn
                caras[punteroCaras] = int.Parse(verticesCaras[0]) - 1;
                punteroCaras++;
            }
        }
    }

    private void UpdateMesh(GameObject obj)
    {
        obj.GetComponent<MeshFilter>().mesh.vertices = vertices;
        obj.GetComponent<MeshFilter>().mesh.triangles = caras;
        obj.GetComponent<MeshFilter>().mesh.colors = color;
    }

    private void CreateMaterial(GameObject obj)
    {
        Material miMaterial = new Material(Shader.Find("Shader"));
        obj.GetComponent<MeshRenderer>().material = miMaterial;
    }

    public void SetColor(float r, float g, float b)
    {
        // Asigno el mismo color a cada vertice
        color = new Color[cantVertices];

        for (int i = 0; i < cantVertices; i++)
        {
            color[i] = new Color(r, g, b, 1);
        }

        objeto.GetComponent<MeshFilter>().mesh.colors = color;
    }

}