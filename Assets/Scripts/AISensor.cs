using System;
using System.Collections;
using System.Collections.Generic;
// using Mono.Cecil.Cil;
using UnityEngine;
using Object = System.Object;

[ExecuteInEditMode]
public class AISensor : MonoBehaviour
{
    [Header("Wedge")]
    public float distance = 10f;
    public float angle = 30f;
    public float heigth = 1.0f;
    public  Color meshColor = Color.red;
    private Mesh mesh;

    [Header("Player scanning")] 
    public int scanFrequencey = 30;
    public LayerMask layers;
    public LayerMask occlusionLayers;
    public List<GameObject> objects = new List<GameObject>();
    private Collider[] colliders = new Collider[50];
    private int count;
    private float scanInterval;
    private float scanTimer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        scanInterval = 1.0f / scanFrequencey;
    }

    // Update is called once per frame
    void Update()
    {
        scanTimer -= Time.deltaTime;
        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
        }
    }
    void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, distance, colliders, layers, QueryTriggerInteraction.Collide);

        objects.Clear();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = colliders[i].gameObject;
            if (IsInSight(obj))
            {
                objects.Add(obj);
                //start following the player
                // if (!sensor.InSight(Player.GameObject)
            }
        }
    }

    public bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;

        if (direction.y < 0 || direction.y > heigth)
        {
            return false;
        }

        direction.y = 0;
        float deltaAngel = Vector3.Angle(direction, transform.forward);
        if (deltaAngel > angle)
        {
            return false;
        }

        origin.y += heigth / 2;
        dest.y = origin.y;
        if (Physics.Linecast(origin, dest, occlusionLayers))
        {
            return false;
        }
        
        return true;
    }

    Mesh createWedge()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * heigth;
        Vector3 topLeft = bottomLeft + Vector3.up * heigth;
        Vector3 topRight = bottomRight + Vector3.up * heigth;

        int vert = 0;
        
        //left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;
        
        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;
        
        //right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;
        
        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngel = (angle * 2) / segments;

        for (int i = 0; i < segments; i++)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngel, 0) * Vector3.forward * distance;
            
            topLeft = bottomLeft + Vector3.up * heigth;
            topRight = bottomRight + Vector3.up * heigth;
            
            //far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;
        
            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;
        
            //top side
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;
        
            //bottom side
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;
            
            currentAngle += deltaAngel;
        }

        for (int i = 0; i < numVertices; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        return mesh;
    }

    private void OnValidate()
    {
        mesh = createWedge();
        scanInterval = 1.0f / scanFrequencey;
    }

    private void OnDrawGizmos()
    {
        if (mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }
        
        Gizmos.DrawWireSphere(transform.position, distance);
        
        for(int i = 0; i < count; i++)
        {
            Gizmos.DrawSphere(colliders[i].transform.position, 0.2f);
        }

        Gizmos.color = Color.green;
        foreach (var obj in objects)
        {
            Gizmos.DrawSphere(obj.transform.position, 0.2f);
        }
    }
}
