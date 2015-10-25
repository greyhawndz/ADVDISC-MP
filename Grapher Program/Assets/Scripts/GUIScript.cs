using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class GUIScript : MonoBehaviour {
	public struct VectorData
	{
		public int x;
		public int y;
	}
	public struct VectorInput
	{
		public string x;
		public string y;
	}
	
	//State of drawing
	private enum Shapes
    {
        None,
		Line,
		Conic
	}
	private Shapes shape = Shapes.None;
    
    //GameObject
    public LineManager lineManager;
    public LineManager lineManager2;
    public ParticleGrapher particleGrapher;
    public MatrixOperation operation;

	//variables to choose which window, you can ignore these
	public Rect windowRect;
	public bool clicked = false;
	public bool setObjectClicked = false;
	public bool setVerticesClicked = false;
	public bool setPointClicked = false;
	public bool setEllipseClicked = false;
	public bool setParabolaClicked = false;
	public bool setHyperbolaClicked=false;
	public bool scaleClicked = false;
	public bool reflectClicked = false;
	public bool rotateClicked = false;
	public bool shearClicked = false;
	public bool translateClicked = false;
	public bool setEquationClicked = false;
	public Font font; 
	private GUIStyle guiStyle;

	//Variables used for temporary storage, you can ignore these
	public string equationBox;
	public string degreeBox;
	public string percentageScaleBoxX;
	public string percentageScaleBoxY;
	public string percentageBox;
	public string xTranslateBox;
	public string yTranslateBox;
	public string ellipseCenterBoxX;
	public string ellipseCenterBoxY;
	public string ellipseWidthBox;
	public string ellipseHeightBox;

	public string pointBoxX;
	public string pointBoxY;

	public string hyperbolaCenterBoxX;
	public string hyperbolaCenterBoxY;
	public string hyperbolaWidthBox;
	public string hyperbolaHeightBox;


	public string parabolaCenterBoxX;
	public string parabolaCenterBoxY;
	public string parabolaMagnitudeBox;


	public string stringCleaner;
	public string shearBox;
	public Regex rgx;
	public int vertexCount;
	public VectorData tempVertex;
	public VectorInput vertexBox;
	VectorInput[] vectorInputArray = new VectorInput[8];
	VectorData[] tempVectorArray = new VectorData[8];
	

	/*============Variables with the usable data====================*/
	VectorData[] vectorArray;
    Vector3[] vectorArray2;
	public int percentageScaleX;
	public int percentageScaleY;
	public int percentage;
	public int degrees;
	public int xTranslate;
	public int yTranslate;
	public string equation;
	public int shearAmount;
	public bool isShearVertical;

	public int ellipseCenterX;
	public int ellipseCenterY;
	public int ellipseHeight;
	public int ellipseWidth;

	public int pointX;
	public int pointY;

	public int hyperbolaCenterX;
	public int hyperbolaCenterY;
	public int hyperbolaHeight;
	public int hyperbolaWidth;
	public bool hyperbolaIsVertical;

	public int parabolaCenterX;
	public int parabolaCenterY;
	public int parabolaMagnitude;

	public string formulaBoxContent;
	public bool reflectX = false;
	public bool reflectY =false;

	void clear()
	{
		setObjectClicked = false;
		setPointClicked = false;
		setVerticesClicked = false;
		scaleClicked = false;
		reflectClicked = false;
		setEllipseClicked = false;
		setParabolaClicked = false;
		setHyperbolaClicked = false;
		rotateClicked = false;
		shearClicked = false;
		translateClicked = false;
		setEquationClicked = false;
	}

	void eraseFigure()
	{
		vectorArray =null;
		percentageScaleX= 0;
		percentageScaleY= 0;
		percentage= 0;
		degrees = 0;
		xTranslate = 0;
		yTranslate = 0;
		equation = "";
		shearAmount = 0;
		
		ellipseCenterX = 0;
		ellipseCenterY = 0;
	    ellipseHeight = 0;
		ellipseWidth = 0;

		pointX = 0;
		pointY = 0;

		parabolaCenterX = 0;
		parabolaCenterY = 0;
		parabolaMagnitude = 0;

		hyperbolaCenterX = 0;
		hyperbolaCenterY = 0;
		hyperbolaHeight = 0;
		hyperbolaWidth = 0;
		hyperbolaIsVertical = false;
		
		formulaBoxContent = "";
		reflectX = false;
		reflectY =false;

        operation.clear();
        shape = Shapes.None;
    }
	// Use this for initialization
	void Start () {

		rgx = new Regex("[^-^0-9]");
		vertexCount = 1;
		formulaBoxContent = "Formula Box";
		vectorInputArray[0]= new VectorInput();
		vectorInputArray[1]= new VectorInput();
		vectorInputArray[2]= new VectorInput();
		vectorInputArray[3]= new VectorInput();
		vectorInputArray[4]= new VectorInput();
		vectorInputArray[5]= new VectorInput();
		vectorInputArray[6]= new VectorInput();
		vectorInputArray[7]= new VectorInput();

		vectorInputArray [0].x = "";
		vectorInputArray [0].y = "";
		vectorInputArray [1].x = "";
		vectorInputArray [1].y = "";
		vectorInputArray [2].x = "";
		vectorInputArray [2].y = "";
		vectorInputArray [3].x = "";
		vectorInputArray [3].y = "";
		vectorInputArray [4].x = "";
		vectorInputArray [4].y = "";
		vectorInputArray [5].x = "";
		vectorInputArray [5].y = "";
		vectorInputArray [6].x = "";
		vectorInputArray [6].y = "";
		vectorInputArray [7].x = "";
		vectorInputArray [7].y = "";

		windowRect = new Rect(450, 150, 250, 250);

	}
	
	// Update is called once per frame
	public void Update () {

	}


	public void OnGUI() {

		if (GUI.Button (new Rect (10, 50, 100, 30), "Set Object")) {
			clear ();
			setObjectClicked=true;
		}

		if (GUI.Button (new Rect (10, 85, 100, 30), "Erase Figure")) {
			if(shape == Shapes.Line){
				lineManager.RemoveLine();
            	lineManager2.RemoveLine();
            }
            else if(shape == Shapes.Conic){
            	particleGrapher.Delete();
            }

            clear();
            eraseFigure();
        }
			
		if (GUI.Button (new Rect (180, 160, 80, 30), "Reflect")) {
			clear ();
			reflectClicked = true;
		}
		if (GUI.Button (new Rect (80, 160, 90, 30), "Rotate")) {
			clear ();
			rotateClicked = true;
		}
		if (GUI.Button (new Rect (220, 200, 90, 30), "Scale")) {
			clear ();
			scaleClicked = true;
		}
		if (GUI.Button (new Rect (120, 200, 90, 30), "Shear")) {
			clear ();
			shearClicked = true;
		}
		if (GUI.Button (new Rect (20, 200, 90, 30), "Translate")) {
			clear ();
			translateClicked = true;
		}

		GUI.TextArea (new Rect (15, 240, 300, 300), formulaBoxContent);
        
		if (GUI.Button (new Rect (200, 550, 100, 30), "Quit")) {
			Application.Quit();

		}


		if (setObjectClicked) {
			windowRect = GUI.Window (0, windowRect, setObjectFunction, "Set Object");
		}
		if (setVerticesClicked) {
			windowRect = GUI.Window (0, windowRect, setVerticesFunction, "Set Vertices");
		}

		if (setPointClicked) {
			windowRect = GUI.Window (0, windowRect, setPoint, "Set Point");
		}
		if (setEllipseClicked) {
			windowRect = GUI.Window (0, windowRect, setEllipse, "Set Ellipse");
		}
		if (setParabolaClicked) {
			windowRect = GUI.Window (0, windowRect, setParabola, "Set Parabola");
		}
		if (setHyperbolaClicked) {
			windowRect = GUI.Window (0, windowRect, setHyperbola, "Set Hyperbola");
		}
		if (translateClicked) {
			windowRect = GUI.Window (0, windowRect, translate, "Translate");
		}

		if (reflectClicked) {
			windowRect = GUI.Window (0, windowRect, reflect, "Reflect");
		}
		if (rotateClicked) {
			windowRect = GUI.Window (0, windowRect, rotate, "Set Object");
		}
		if (scaleClicked) {
			windowRect = GUI.Window (0, windowRect, scale, "Scale");
		}
		if (shearClicked) {
			windowRect = GUI.Window (0, windowRect, shear, "Shear");
		}
		if (setEquationClicked) {
			windowRect = GUI.Window (0, windowRect, setEquation, "Set Object");
		}

			}

	public void reflect(int windowID) {

		if (GUI.Button(new Rect(75, 50, 100, 30), "Along X Axis"))
		{reflectX = true;
			print ("Reflected Across X");

            if (shape == Shapes.Line)
            {
                formulaBoxContent = "Reflect -> along the X Axis\n\n";
                operation.reflect(0);
                drawLine(operation.NewVertices);
            }
            else if( shape == Shapes.Conic )
            {

            }

            clear ();
        }
		
		if (GUI.Button (new Rect (75, 100, 100, 30), "Along Y Axis")) {
			reflectY = true;
			print ("Reflected Across Y");

            if( shape == Shapes.Line )
            {
                formulaBoxContent = "Reflect -> along the Y Axis\n\n";
                operation.reflect(1);
                drawLine(operation.NewVertices);
            }
            else if (shape == Shapes.Conic)
            {

            }

            clear ();
        }

		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {

			clear ();
		}
		GUI.DragWindow();
	}
        
	public void rotate(int windowID) {
		GUI.Label(new Rect(65, 35, 130, 30), "Degrees");

		degreeBox = GUI.TextArea(new Rect(65, 55, 130, 30), degreeBox);
		degreeBox = rgx.Replace(degreeBox, "");
		if (GUI.Button(new Rect(65, 115, 130, 30), "CounterClockwise"))
		{
            degrees =int.Parse(degreeBox);
		    degreeBox ="";
		    print(degrees);

            if( shape == Shapes.Line )
            {
                formulaBoxContent = "Rotate -> " + degrees + " degrees CounterClockwise\n\n";
                operation.rotate(1, degrees);
                drawLine(operation.NewVertices);
            }
            else if (shape == Shapes.Conic)
            {

            }

            clear ();            
		}
		
		if (GUI.Button (new Rect (65, 150, 130, 30), "Clockwise")) {
			degrees =int.Parse(degreeBox);
			degreeBox ="";
			print(degrees);

            if( shape == Shapes.Line )
            {
                formulaBoxContent = "Rotate -> " + degrees + " degrees Clockwise\n\n";
                operation.rotate(0, degrees);
                drawLine(operation.NewVertices);
            }
            else if (shape == Shapes.Conic)
            {

            }

            clear ();            
		}
		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {
			clear ();
		}
		GUI.DragWindow();
	}

	public void scale(int windowID) {
		GUI.Label(new Rect(65, 35, 130, 30), "% Scale X");
		percentageScaleBoxX = GUI.TextArea(new Rect(65, 55, 130, 30), percentageScaleBoxX);
		percentageScaleBoxX = rgx.Replace(percentageScaleBoxX, "");
		GUI.Label(new Rect(65, 90, 130, 30), "% Scale Y");
		percentageScaleBoxY = GUI.TextArea(new Rect(65, 110, 130, 30), percentageScaleBoxY);
		percentageScaleBoxY = rgx.Replace(percentageScaleBoxY, "");
		if (GUI.Button (new Rect (65, 155, 130, 30), "Confirm")) {
			percentageScaleX =int.Parse(percentageScaleBoxX);
			percentageScaleBoxX ="";
						
			percentageScaleY =int.Parse(percentageScaleBoxY);
			percentageScaleBoxY ="";

            print(percentageScaleX);

            print(percentageScaleY);

            if (shape == Shapes.Line)
            {
                formulaBoxContent = "Scale -> X: " + percentageScaleX + "%; Y: " + percentageScaleY + "%\n\n";
                operation.scale((float)(percentageScaleX / 100.0), (float)(percentageScaleY / 100.0));
                drawLine(operation.NewVertices);
            }
            else if (shape == Shapes.Conic)
            {

            }

            clear ();			
		}
		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {
			percentageScaleBoxX ="";
			clear ();
		}
		GUI.DragWindow();
	}

	public void shear(int windowID) {
		GUI.Label(new Rect(65, 35, 130, 30), "Shear");
		shearBox = GUI.TextArea(new Rect(65, 55, 130, 30), shearBox);
		shearBox = rgx.Replace(shearBox, "");
		if (GUI.Button (new Rect (65, 150, 130, 30), "Horizontal")&&(shearBox!="")) {
			shearAmount = int.Parse(shearBox);
			shearBox="";
			isShearVertical = false;
			print (shearAmount);

            if( shape == Shapes.Line )
            {
                formulaBoxContent = "Shear -> Horizonal by " + shearAmount + "\n\n";
                operation.shear(0, shearAmount);
                drawLine(operation.NewVertices);
            }
            else if (shape == Shapes.Conic)
            {

            }

            clear ();            
		}
		if (GUI.Button (new Rect (65, 190, 130, 30), "Vertical")&&(shearBox!="")) {
			shearAmount = int.Parse(shearBox);
			shearBox="";
			isShearVertical = true;
			print (shearAmount);

            if( shape == Shapes.Line )
            {
                formulaBoxContent = "Shear -> Vertical by " + shearAmount + "\n\n";
                operation.shear(1, shearAmount);
                drawLine(operation.NewVertices);
            }
            else if (shape == Shapes.Conic)
            {

            }

            clear ();            
		}
		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {
			clear ();
		}
		GUI.DragWindow();
	}

	public void translate(int windowID) {
		GUI.Label(new Rect(50, 40, 130, 30), "X");
		xTranslateBox = GUI.TextArea(new Rect(65, 35, 130, 30), xTranslateBox);
		GUI.Label(new Rect(50, 80, 130, 30), "Y");
		yTranslateBox = GUI.TextArea(new Rect(65, 75, 130, 30), yTranslateBox);


		if (GUI.Button (new Rect (65, 150, 130, 30), "Translate")) {
			xTranslate = int.Parse (xTranslateBox);
			yTranslate = int.Parse (yTranslateBox);
			print (xTranslate);
			print (yTranslate);

            if( shape == Shapes.Line )
            {
                formulaBoxContent = "Translate -> X: " + xTranslate + "; Y: " + yTranslate + "\n\n";
                operation.translate(xTranslate, yTranslate);
                drawLine(operation.NewVertices);
            }
            else if (shape == Shapes.Conic)
            {

            }

            clear ();            
		}
		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {
			xTranslateBox ="";
			yTranslateBox ="";

			clear ();
		}
		GUI.DragWindow();
	}

    private void drawLine( Vector3[] vertices )
    {
        lineManager2.RenderLine(vertices, new Color32(0, 63, 247, 255));
        formulaBoxContent += operation.MatrixValues;
    }

    public void setObjectFunction(int windowID) {

		if (GUI.Button(new Rect(75, 30, 100, 30), "Equation"))
		{	

			setEquationClicked =true;
			setEquation(0);}
		
		if (GUI.Button (new Rect (75, 65, 100, 30), "Vertices")) {
			setVerticesClicked = true;
			setVerticesFunction(0);
		}
		if (GUI.Button (new Rect (75, 100, 100, 30), "Ellipse")) {
			setEllipseClicked = true;
			setEllipse(0);
		}
		if (GUI.Button (new Rect (75, 135, 100, 30), "Parabola")) {
			setParabolaClicked = true;
			setParabola(0);
		}
		if (GUI.Button (new Rect (75, 170, 100, 30), "Hyperbola")) {

			setHyperbolaClicked = true;
			setHyperbola(0);
		}
		if (GUI.Button (new Rect (75, 205, 100, 30), "Point")) {
			
			setPointClicked = true;
			setPoint(0);
		}
		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {
			clear ();
		}
		GUI.DragWindow();
	}

	public void setPoint(int windowID) {
		GUI.Label(new Rect(50, 40, 130, 30), "X");
		pointBoxX = GUI.TextArea(new Rect(85, 35, 60, 20), pointBoxX);
		pointBoxX = rgx.Replace(pointBoxX, "");
		GUI.Label(new Rect(50, 65, 130, 30), "Y");
		pointBoxY = GUI.TextArea(new Rect(85, 60, 60, 20), pointBoxY);
		pointBoxY = rgx.Replace(pointBoxY, "");
		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {

			clear ();

		}
		if (GUI.Button (new Rect (65, 200, 130, 30), "Confirm")) {

			pointX = int.Parse(pointBoxX);
			pointY = int.Parse (pointBoxY);

			pointBoxX = "";
			pointBoxY = "";
			print (pointX);
			print (pointY);
			clear ();
								
				}
		
		
		
		GUI.DragWindow();
	}
	public void setEquation(int windowID)
	{	GUI.Label(new Rect(40, 55, 130, 30), "Equation");
		equationBox = GUI.TextArea(new Rect(40, 75, 180, 30), equationBox);

		
		if (GUI.Button (new Rect (65, 150, 130, 30), "Confirm")) {
			equation=equationBox;
			print(equation);
			equationBox ="";
			clear ();
		}
		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {
			equationBox ="";
			clear ();
		}
		GUI.DragWindow();
	}

	public void setEllipse(int windowID)
	{	
		GUI.Label(new Rect(50, 40, 130, 30), "X");
		ellipseCenterBoxX = GUI.TextArea(new Rect(65, 35, 130, 30), ellipseCenterBoxX);
		ellipseCenterBoxX = rgx.Replace(ellipseCenterBoxX, "");
		GUI.Label(new Rect(50, 80, 130, 30), "Y");
		ellipseCenterBoxY = GUI.TextArea(new Rect(65, 75, 130, 30), ellipseCenterBoxY);
		ellipseCenterBoxY = rgx.Replace(ellipseCenterBoxY, "");
		GUI.Label(new Rect(50, 120, 130, 30), "H");
		ellipseHeightBox = GUI.TextArea(new Rect(65, 115, 130, 30), ellipseHeightBox);
		ellipseHeightBox = rgx.Replace(ellipseHeightBox, "");
		GUI.Label(new Rect(50, 160, 130, 30), "W");
		ellipseWidthBox = GUI.TextArea(new Rect(65, 155, 130, 30), ellipseWidthBox);
		ellipseWidthBox = rgx.Replace(ellipseWidthBox, "");
		
		if (GUI.Button (new Rect (65, 200, 130, 30), "Confirm")) {
			ellipseCenterX=int.Parse(ellipseCenterBoxX);
			ellipseCenterY=int.Parse(ellipseCenterBoxY);
			ellipseHeight=int.Parse(ellipseHeightBox);
			ellipseWidth=int.Parse(ellipseWidthBox);
			ellipseCenterBoxX = "";
			ellipseCenterBoxY= "";
			ellipseHeightBox= "";
			ellipseWidthBox= "";
			print("X: "+ellipseCenterX);
			print("Y: "+ellipseCenterY);
			print("H: "+ellipseHeight);
			print("W: "+ellipseWidth);
			shape = Shapes.Line;
			//particleGrapher.RenderEllipse(new Vector3(ellipseCenterX,ellipseCenterY,0), ellipseHeight, ellipseWidth);
			lineManager.RenderEllipse(100, ellipseWidth,ellipseHeight, new Vector3(ellipseCenterX,ellipseCenterY,0));
			clear ();
		}
		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {
			equationBox ="";
			clear ();
		}
		GUI.DragWindow();
	}

	public void setHyperbola(int windowID)
	{			
		GUI.Label(new Rect(50, 40, 130, 30), "X");
		hyperbolaCenterBoxX = GUI.TextArea(new Rect(65, 35, 130, 30), hyperbolaCenterBoxX);
		hyperbolaCenterBoxX = rgx.Replace(hyperbolaCenterBoxX, "");
		GUI.Label(new Rect(50, 80, 130, 30), "Y");
		hyperbolaCenterBoxY = GUI.TextArea(new Rect(65, 75, 130, 30), hyperbolaCenterBoxY);
		hyperbolaCenterBoxY = rgx.Replace(hyperbolaCenterBoxY, "");
		GUI.Label(new Rect(50, 120, 130, 30), "H");
		hyperbolaHeightBox = GUI.TextArea(new Rect(65, 115, 130, 30), hyperbolaHeightBox);
		hyperbolaHeightBox = rgx.Replace(hyperbolaHeightBox, "");
		GUI.Label(new Rect(50, 160, 130, 30), "W");
		hyperbolaWidthBox = GUI.TextArea(new Rect(65, 155, 130, 30), hyperbolaWidthBox);
		hyperbolaWidthBox = rgx.Replace(hyperbolaWidthBox, "");
		
		if (GUI.Button (new Rect (35, 200, 80, 30), "Vertical")) {
			hyperbolaCenterX=int.Parse(hyperbolaCenterBoxX);
			hyperbolaCenterY=int.Parse(hyperbolaCenterBoxY);
			hyperbolaHeight=int.Parse(hyperbolaHeightBox);
			hyperbolaWidth=int.Parse(hyperbolaWidthBox);
			hyperbolaIsVertical = true;
			hyperbolaCenterBoxX = "";
			hyperbolaCenterBoxY= "";
			hyperbolaHeightBox= "";
			hyperbolaWidthBox= "";
			print("X: "+hyperbolaCenterX);
			print("Y: "+hyperbolaCenterY);
			print("H: "+hyperbolaHeight);
			print("W: "+hyperbolaWidth);
			shape = Shapes.Conic;
			clear ();
		}
		if (GUI.Button (new Rect (135, 200, 80, 30), "Horizontal")) {
			hyperbolaCenterX=int.Parse(hyperbolaCenterBoxX);
			hyperbolaCenterY=int.Parse(hyperbolaCenterBoxY);
			hyperbolaHeight=int.Parse(hyperbolaHeightBox);
			hyperbolaWidth=int.Parse(hyperbolaWidthBox);
			hyperbolaIsVertical = false;
			hyperbolaCenterBoxX = "";
			hyperbolaCenterBoxY= "";
			hyperbolaHeightBox= "";
			hyperbolaWidthBox= "";
			print("X: "+hyperbolaCenterX);
			print("Y: "+hyperbolaCenterY);
			print("H: "+hyperbolaHeight);
			print("W: "+hyperbolaWidth);
			shape = Shapes.Conic;
			clear ();
		}
		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {
			hyperbolaCenterBoxX = "";
			hyperbolaCenterBoxY= "";
			hyperbolaHeightBox= "";
			hyperbolaWidthBox= "";
			clear ();
		}
		GUI.DragWindow();
	}

	public void setParabola(int windowID)
	{	
		GUI.Label(new Rect(50, 40, 130, 30), "X");
		parabolaCenterBoxX = GUI.TextArea(new Rect(65, 35, 130, 30), parabolaCenterBoxX);
		parabolaCenterBoxX = rgx.Replace(parabolaCenterBoxX, "");
		GUI.Label(new Rect(50, 80, 130, 30), "Y");
		parabolaCenterBoxY = GUI.TextArea(new Rect(65, 75, 130, 30), parabolaCenterBoxY);
		parabolaCenterBoxY = rgx.Replace(parabolaCenterBoxY, "");
		GUI.Label(new Rect(50, 120, 130, 30), "M");
		parabolaMagnitudeBox = GUI.TextArea(new Rect(65, 115, 130, 30), parabolaMagnitudeBox);
		parabolaMagnitudeBox = rgx.Replace(parabolaMagnitudeBox, "");

		
		if (GUI.Button (new Rect (65, 200, 130, 30), "Confirm")) {
			parabolaCenterX=int.Parse(parabolaCenterBoxX);
			parabolaCenterY=int.Parse(parabolaCenterBoxY);
			parabolaMagnitude=int.Parse(parabolaMagnitudeBox);

			parabolaCenterBoxX = "";
			parabolaCenterBoxY= "";
			parabolaMagnitudeBox= "";
	
			print("X: "+parabolaCenterX);
			print("Y: "+parabolaCenterY);
			print("M: "+parabolaMagnitude);
			shape = Shapes.Line;
			Vector3 vertex = new Vector3(parabolaCenterX, parabolaCenterY, -1);
			//particleGrapher.RenderParabola(vertex, parabolaMagnitude);
			lineManager.RenderParabola(51,parabolaMagnitude, vertex);
			clear ();
		}
		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {
			equationBox ="";
			clear ();
		}
		GUI.DragWindow();
	}

	public void setVerticesFunction(int windowID) {

		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {
			clear ();
		}
		if (GUI.Button (new Rect (65, 200, 130, 30), "Confirm")) {
			 vectorArray = new VectorData[vertexCount];
            vectorArray2 = new Vector3[vertexCount];
			clear ();
			for(int i=0;i<vertexCount;i++)
			{
                vectorArray[i]=tempVectorArray[i];
                vectorArray2[i] = new Vector3(tempVectorArray[i].x, tempVectorArray[i].y,-1);
				print ("X:"+ vectorArray[i].x +"  Y:" + vectorArray[i].y);
				print (vectorArray.Length);
			}
            shape = Shapes.Line;
            lineManager.RenderLine(vectorArray2, new Color32(19, 255, 0, 255));


		}

		if (GUI.Button(new Rect(185, 30 +(vertexCount*20), 20, 20), "+")&& (vertexCount<8))
		{vertexCount++;
		}
		if (GUI.Button(new Rect(205, 30 +(vertexCount*20), 20, 20), "-")&& (vertexCount>1))
		{vertexCount--;
		}

		for (int i=0; i<vertexCount; i++) {
			GUI.Label(new Rect(10, 10+((i+1)*20), 100, 30), "Vertex "+(i+1));
			GUI.Label(new Rect(70, 10+((i+1)*20), 100, 30), "X:                 Y:");

			vectorInputArray[i].x = GUI.TextArea(new Rect(85, 5+((i+1)*20), 60, 20), vectorInputArray[i].x);
			vectorInputArray[i].x = rgx.Replace(vectorInputArray[i].x, "");
			vectorInputArray[i].y = GUI.TextArea(new Rect(165, 5+((i+1)*20), 60, 20), vectorInputArray[i].y);
			vectorInputArray[i].y = rgx.Replace(vectorInputArray[i].y, "");

			if(vectorInputArray[i].x != "")
				tempVertex.x =int.Parse(vectorInputArray[i].x);
			if(vectorInputArray[i].y != "")
				tempVertex.y =int.Parse(vectorInputArray[i].y);

			tempVectorArray[i]=tempVertex;

		}



		GUI.DragWindow();
	}
}
