using UnityEngine;
using System.Collections;

public class MatrixOperation : MonoBehaviour
{
    private Vector3[] vertices;
    private Vector3[] newVertices;
    public LineManager lineManager;

    public Vector3[] NewVertices
    {
        get
        {
            return newVertices;
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    public void clear()
    {
        newVertices = null;
    }

    public void translate(int x, int y)
    {
        vertices = lineManager.Vertices;
        newVertices = new Vector3[vertices.Length];

        for(int i = 0; i < vertices.Length; i++)
        {
            newVertices[i].x = vertices[i].x + x;
            newVertices[i].y = vertices[i].y + y;
        }
    }

    public void rotate()
    {

    }

    public void shear()
    {

    }

    public void scale()
    {

    }

    public void reflect(int operation)
    {
        /*****
        operation values:
        0 - Along X Axis
        1 - Along Y Axis
        *****/
        vertices = lineManager.Vertices;
        Vector2[] multiplier = new Vector2[2];

        if ( operation == 0 )
        {
            multiplier[0] = new Vector2(1, 0);
            multiplier[1] = new Vector2(0, -1);
        }
        else if( operation == 1 )
        {
            multiplier[0] = new Vector2(-1, 0);
            multiplier[1] = new Vector2(0, 1);
        }

        multiply(multiplier);
    }

    void multiply(Vector2[] multiplier)
    {
        print("KERRBIE MULTIPLY" + vertices.Length);
        newVertices = new Vector3[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            newVertices[i].x = vertices[i].x * multiplier[0].x + vertices[i].y * multiplier[1].x;
            newVertices[i].y = vertices[i].x * multiplier[0].y + vertices[i].y * multiplier[1].y;
            newVertices[i].z = vertices[i].z;
        }
    }
}
