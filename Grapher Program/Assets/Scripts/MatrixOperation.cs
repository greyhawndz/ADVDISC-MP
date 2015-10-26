using UnityEngine;
using System.Collections;

public class MatrixOperation : MonoBehaviour
{
    private Vector2[] multiplier;
    private Vector3[] vertices;
    private Vector3[] vertices2;
    private Vector3[] newVertices;
    private Vector3[] newVertices2;
    private string matrixValues;
    public LineManager lineManager;

    //public Vector3[] NewVertices
    //{
    //    get
    //    {
    //        return newVertices;
    //    }
    //}

    public Vector3[][] NewVertices
    {
        get
        {
            Vector3[][] ret = new Vector3[2][];
            ret[0] = newVertices;

            if (lineManager.Hyperbola)
            {
                ret[1] = newVertices2;
            }
            else
            {
                ret[1] = null;
            }

            return ret;
        }
    }

    public string MatrixValues
    {
        get
        {
            return matrixValues;
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    public void getData( bool isHyperbola )
    {
        vertices = lineManager.Vertices;

        //get reflected parabola
        if( isHyperbola )
        {
            multiplier = new Vector2[2];

            if ( !lineManager.Horizontal )
            {
                multiplier[0] = new Vector2(1, 0);
                multiplier[1] = new Vector2(0, -1);
            }
            else if ( lineManager.Horizontal )
            {
                multiplier[0] = new Vector2(-1, 0);
                multiplier[1] = new Vector2(0, 1);
            }

            multiply(vertices, multiplier);
        }
    }

    public void clear()
    {
        newVertices = null;
        matrixValues = "";
    }

    public void reflect(int operation)
    {

        /*****
        operation values:
        0 - Along X Axis
        1 - Along Y Axis
        *****/

        getData(lineManager.Hyperbola);
        multiplier = new Vector2[2];

        if (operation == 0)
        {
            multiplier[0] = new Vector2(1, 0);
            multiplier[1] = new Vector2(0, -1);
        }
        else if (operation == 1)
        {
            multiplier[0] = new Vector2(-1, 0);
            multiplier[1] = new Vector2(0, 1);
        }

        multiply(multiplier);
    }

    public void rotate( int operation, int degrees )
    {
        /*****
        operation values:
        0 - clockwise
        1 - counter-clockwise
        *****/

        getData(lineManager.Hyperbola);
        multiplier = new Vector2[2];

        float angle = (float) (Mathf.PI * degrees / 180.0);

        if (operation == 0)
        {
            multiplier[0] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            multiplier[1] = new Vector2(-Mathf.Sin(angle), Mathf.Cos(angle));
        }
        else if (operation == 1)
        {
            multiplier[0] = new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle));
            multiplier[1] = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        }

        multiply(multiplier);
    }

    public void scale(float scaleFactorX, float scaleFactorY)
    {
        getData(lineManager.Hyperbola);
        multiplier = new Vector2[2];

        multiplier[0] = new Vector2(scaleFactorX, 0);
        multiplier[1] = new Vector2(0, scaleFactorY);

        multiply(multiplier);
    }

    public void shear(int operation, int shearFactor)
    {
        /*****
        operation values:
        0 - horizontal
        1 - vertical
        *****/

        getData(lineManager.Hyperbola);
        multiplier = new Vector2[2];

        if (operation == 0)
        {
            multiplier[0] = new Vector2(1, 0);
            multiplier[1] = new Vector2(shearFactor, 1);
        }
        else if (operation == 1)
        {
            multiplier[0] = new Vector2(1, shearFactor);
            multiplier[1] = new Vector2(0, 1);
        }

        multiply(multiplier);
    }

    public void translate(int x, int y)
    {
        int multIndex = 0;
        getData(lineManager.Hyperbola);
        newVertices = new Vector3[vertices.Length];

        //multiplier variable is used so that additional declarations won't be needed, even though nothing is being multiplied

        if (!lineManager.Hyperbola)
        {
            multiplier = new Vector2[vertices.Length];
        }
        else if( lineManager.Hyperbola )
        {
            multiplier = new Vector2[vertices.Length * 2];
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            newVertices[i].x = vertices[i].x + x;
            newVertices[i].y = vertices[i].y + y;
            multiplier[i].x = x;
            multiplier[i].y = y;
            multIndex = i;
        }
        multIndex++;

        if( lineManager.Hyperbola )
        {
            newVertices2 = new Vector3[vertices.Length];

            for (int j = 0; j < vertices.Length; j++)
            {
                newVertices2[j].x = vertices[j].x + x;
                newVertices2[j].y = vertices[j].y + y;
                multiplier[multIndex + j].x = x;
                multiplier[multIndex + j].y = y;
            }
        }

        writeMatrixValues(multiplier, true);
    }

    void multiply(Vector2[] multiplier)
    {
        newVertices = new Vector3[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            newVertices[i].x = vertices[i].x * multiplier[0].x + vertices[i].y * multiplier[1].x;
            newVertices[i].y = vertices[i].x * multiplier[0].y + vertices[i].y * multiplier[1].y;
            newVertices[i].z = vertices[i].z;
        }

        if( lineManager.Hyperbola )
        {
            newVertices2 = new Vector3[vertices2.Length];

            for (int i = 0; i < vertices2.Length; i++)
            {
                newVertices2[i].x = vertices2[i].x * multiplier[0].x + vertices2[i].y * multiplier[1].x;
                newVertices2[i].y = vertices2[i].x * multiplier[0].y + vertices2[i].y * multiplier[1].y;
                newVertices2[i].z = vertices2[i].z;
            }
        }

        writeMatrixValues(multiplier, false);
    }

    void multiply(Vector3[] multiplicand, Vector2[] multiplier)
    {
        vertices2 = new Vector3[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices2[i].x = multiplicand[i].x * multiplier[0].x + multiplicand[i].y * multiplier[1].x;
            vertices2[i].y = multiplicand[i].x * multiplier[0].y + multiplicand[i].y * multiplier[1].y;
            vertices2[i].z = multiplicand[i].z;
        }
    }

    void writeMatrixValues( Vector2[] multiplier, bool isTranslation )
    {
        matrixValues = "The Matrix\n";
        for ( int i = 0; i < vertices.Length; i++ )
        {
            matrixValues += vertices[i].x.ToString() + "\t" + vertices[i].y.ToString() + "\n";
        }

        if( isTranslation )
        {
            matrixValues += "\nis added to the matrix\n";
        }
        else
        {
            matrixValues += "\nis multiplied to the matrix\n";
        }

        for( int j = 0; j < multiplier.Length; j++ )
        {
            matrixValues += multiplier[j].x.ToString() + "\t" + multiplier[j].y.ToString() + "\n";
        }

        matrixValues += "\nwhich results to\n";

        for (int k = 0; k < NewVertices.Length; k++)
        {
            matrixValues += newVertices[k].x.ToString() + "\t" + newVertices[k].y.ToString() + "\n";
        }
    }
}
