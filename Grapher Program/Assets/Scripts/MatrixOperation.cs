using UnityEngine;
using System.Collections;

public class MatrixOperation : MonoBehaviour
{
    private Vector3[] vertices;
    public LineManager lineManager;

    public Vector3[] Vertices
    {
        get
        {
            return vertices;
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    public void translate()
    {

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
        print("KERRBIE MULTIPLY" + Vertices.Length);
        Vector3[] newMatrix = new Vector3[Vertices.Length];

        for (int i = 0; i < Vertices.Length; i++)
        {
            newMatrix[i].x = Vertices[i].x * multiplier[0].x + Vertices[i].y * multiplier[1].x;
            newMatrix[i].y = Vertices[i].x * multiplier[0].y + Vertices[i].y * multiplier[1].y;
            newMatrix[i].z = Vertices[i].z;
        }

        vertices = newMatrix;
    }
}
