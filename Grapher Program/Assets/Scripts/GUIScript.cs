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
	public bool setEllipseClicked = false;
	public bool setParabolaClicked = false;
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
	public string percentageBox;
	public string xTranslateBox;
	public string yTranslateBox;
	public string ellipseCenterBoxX;
	public string ellipseCenterBoxY;
	public string ellipseWidthBox;
	public string ellipseHeightBox;

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
	public int percentage;
	public int degrees;
	public int xTranslate;
	public int yTranslate;
	public string equation;
	public int shearAmount;

	public int ellipseCenterX;
	public int ellipseCenterY;
	public int ellipseHeight;
	public int ellipseWidth;

	public int parabolaCenterX;
	public int parabolaCenterY;
	public int parabolaMagnitude;

	public string formulaBoxContent;
	public bool reflectX = false;
	public bool reflectY =false;




	void clear()
	{
		setObjectClicked = false;
		setVerticesClicked = false;
		scaleClicked = false;
		reflectClicked = false;
		setEllipseClicked = false;
		setParabolaClicked = false;
		rotateClicked = false;
		shearClicked = false;
		translateClicked = false;
		setEquationClicked = false;
	}
	void eraseFigure()
	{
		vectorArray =null;
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
		
		parabolaCenterX = 0;
		parabolaCenterY = 0;
		parabolaMagnitude = 0;
		
		formulaBoxContent = "";
		reflectX = false;
		reflectY =false;
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
			clear ();
			eraseFigure ();
			lineManager.RemoveLine();
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

		formulaBoxContent = GUI.TextArea (new Rect (15, 240, 300, 300), formulaBoxContent);


		if (GUI.Button (new Rect (200, 550, 100, 30), "Quit")) {
			Application.Quit();

		}


		if (setObjectClicked) {
			windowRect = GUI.Window (0, windowRect, setObjectFunction, "Set Object");
		}
		if (setVerticesClicked) {
			windowRect = GUI.Window (0, windowRect, setVerticesFunction, "Set Vertices");
		}
		if (setEllipseClicked) {
			windowRect = GUI.Window (0, windowRect, setEllipse, "Set Ellipse");
		}
		if (setParabolaClicked) {
			windowRect = GUI.Window (0, windowRect, setParabola, "Set Parabola");
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
			clear ();
            operation.reflect(0);
            lineManager2.RenderLine(operation.Vertices, new Color32(0, 63, 247, 255));
		}
		
		if (GUI.Button (new Rect (75, 100, 100, 30), "Along Y Axis")) {
			reflectY = true;
			print ("Reflected Across Y");
			clear ();
            operation.reflect(1);
            lineManager2.RenderLine(operation.Vertices, new Color32(0, 63, 247, 255));
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
		if (GUI.Button(new Rect(65, 115, 130, 30), "Clockwise"))
		{ degrees =int.Parse(degreeBox);
		  degreeBox ="";
		  print(degrees);
		  clear ();
		}
		
		if (GUI.Button (new Rect (65, 150, 130, 30), "Counterclockwise")) {
			degrees =int.Parse(degreeBox);
			degrees = degrees *(-1);
			degreeBox ="";
			print(degrees);
			clear ();
		}
		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {
			clear ();
		}
		GUI.DragWindow();
	}
	public void scale(int windowID) {
		GUI.Label(new Rect(65, 35, 130, 30), "% Scale");
		percentageBox = GUI.TextArea(new Rect(65, 55, 130, 30), percentageBox);
		percentageBox = rgx.Replace(percentageBox, "");
		if (GUI.Button (new Rect (65, 150, 130, 30), "Confirm")) {
			percentage =int.Parse(percentageBox);
			percentageBox ="";
			clear ();
			print(percentage);
		}
		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {
			percentageBox ="";
			clear ();
		}
		GUI.DragWindow();
	}

	public void shear(int windowID) {
		GUI.Label(new Rect(65, 35, 130, 30), "Shear");
		shearBox = GUI.TextArea(new Rect(65, 55, 130, 30), shearBox);
		shearBox = rgx.Replace(shearBox, "");
		if (GUI.Button (new Rect (65, 150, 130, 30), "Confirm")) {
			shearAmount = int.Parse(shearBox);
			shearBox="";
			print (shearAmount);
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
			clear ();
		}
		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {
			xTranslateBox ="";
			yTranslateBox ="";

			clear ();
		}
		GUI.DragWindow();
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
		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {
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
			clear ();
		}
		if (GUI.Button (new Rect (225, 0, 25, 20), "X")) {
			equationBox ="";
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
			
			Vector3 vertex = new Vector3(parabolaCenterX, parabolaCenterY, -1);
			particleGrapher.RenderParabola(vertex, parabolaMagnitude);

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
