using UnityEngine;

using UnityEditor;
using System.Collections;


public class CreatePlane : ScriptableWizard
{
   
    public enum Orientation
    {
        Horizontal,
        Vertical
    }

    public enum AnchorPoint
    {
        TopLeft,
        TopHalf,
        TopRight,
        RightHalf,
        BottomRight,
        BottomHalf,
        BottomLeft,
        LeftHalf,
        Center
    }
   
    public int widthSegments = 1;
    public int lengthSegments = 1;
    public float width = 1.0f;
    public float length = 1.0f;
    public Orientation orientation = Orientation.Horizontal;
    public AnchorPoint anchor = AnchorPoint.Center;
    public bool addCollider = false;
    public bool createAtOrigin = true;
    public string optionalName;

    static Camera cam;
    static Camera lastUsedCam;

   
    [MenuItem("GameObject/Create Other/Custom Plane...")]
    static void CreateWizard()
    {
        cam = Camera.current;
        // Hack because camera.current doesn't return editor camera if scene view doesn't have focus
        if (!cam)
            cam = lastUsedCam;
        else
            lastUsedCam = cam;
        ScriptableWizard.DisplayWizard("Create Plane",typeof(CreatePlane));
    }
   
   
    void OnWizardUpdate()
    {
        widthSegments = Mathf.Clamp(widthSegments, 1, 254);
        lengthSegments = Mathf.Clamp(lengthSegments, 1, 254);
    }
   
   
    void OnWizardCreate()
    {
        GameObject plane = new GameObject();
       
        if (!string.IsNullOrEmpty(optionalName))
            plane.name = optionalName;
        else
            plane.name = "Plane";
       
        if (!createAtOrigin && cam)
            plane.transform.position = cam.transform.position + cam.transform.forward*5.0f;
        else
            plane.transform.position = Vector3.zero;
       
        Vector2 anchorOffset;
        string anchorId;
        switch (anchor)
        {
        case AnchorPoint.TopLeft:
            anchorOffset = new Vector2(-width/2.0f,length/2.0f);
            anchorId = "TL";
            break;
        case AnchorPoint.TopHalf:
            anchorOffset = new Vector2(0.0f,length/2.0f);
            anchorId = "TH";
            break;
        case AnchorPoint.TopRight:
            anchorOffset = new Vector2(width/2.0f,length/2.0f);
            anchorId = "TR";
            break;
        case AnchorPoint.RightHalf:
            anchorOffset = new Vector2(width/2.0f,0.0f);
            anchorId = "RH";
            break;
        case AnchorPoint.BottomRight:
            anchorOffset = new Vector2(width/2.0f,-length/2.0f);
            anchorId = "BR";
            break;
        case AnchorPoint.BottomHalf:
            anchorOffset = new Vector2(0.0f,-length/2.0f);
            anchorId = "BH";
            break;
        case AnchorPoint.BottomLeft:
            anchorOffset = new Vector2(-width/2.0f,-length/2.0f);
            anchorId = "BL";
            break;     
        case AnchorPoint.LeftHalf:
            anchorOffset = new Vector2(-width/2.0f,0.0f);
            anchorId = "LH";
            break;     
        case AnchorPoint.Center:
        default:
            anchorOffset = Vector2.zero;
            anchorId = "C";
            break;
        }
               
        MeshFilter meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
        plane.AddComponent(typeof(MeshRenderer));

        string planeAssetName = plane.name + widthSegments + "x" + lengthSegments + "W" + width + "L" + length + (orientation == Orientation.Horizontal? "H" : "V") + anchorId + ".asset";
        Mesh m = (Mesh)AssetDatabase.LoadAssetAtPath("Assets/Meshes/" + planeAssetName,typeof(Mesh));
 
        if (m == null)
        {
            m = new Mesh();
            m.name = plane.name;
       
            int hCount2 = widthSegments+1;
            int vCount2 = lengthSegments+1;
            int numTriangles = widthSegments * lengthSegments * 6;
            int numVertices = hCount2 * vCount2;
       
            Vector3[] vertices = new Vector3[numVertices];
            Vector2[] uvs = new Vector2[numVertices];
            int[] triangles = new int[numTriangles];
       
            int index = 0;
            float uvFactorX = 1.0f/widthSegments;
            float uvFactorY = 1.0f/lengthSegments;
            float scaleX = width/widthSegments;
            float scaleY = length/lengthSegments;
            for (float y = 0.0f; y < vCount2; y++)
            {
                for (float x = 0.0f; x < hCount2; x++)
                {
                    if (orientation == Orientation.Horizontal)
                    {
                        vertices[index] = new Vector3(x*scaleX - width/2f - anchorOffset.x, 0.0f, y*scaleY - length/2f - anchorOffset.y);
                    }
                    else
                    {
                        vertices[index] = new Vector3(x*scaleX - width/2f - anchorOffset.x, y*scaleY - length/2f - anchorOffset.y, 0.0f);
                    }
                    uvs[index++] = new Vector2(x*uvFactorX, y*uvFactorY);
                }
            }
           
            index = 0;
            for (int y = 0; y < lengthSegments; y++)
            {
                for (int x = 0; x < widthSegments; x++)
                {
                    triangles[index]   = (y     * hCount2) + x;
                    triangles[index+1] = ((y+1) * hCount2) + x;
                    triangles[index+2] = (y     * hCount2) + x + 1;
       
                    triangles[index+3] = ((y+1) * hCount2) + x;
                    triangles[index+4] = ((y+1) * hCount2) + x + 1;
                    triangles[index+5] = (y     * hCount2) + x + 1;
                    index += 6;
                }
            }
       
            m.vertices = vertices;
            m.uv = uvs;
            m.triangles = triangles;
			m.RecalculateNormals();
			TangentSolver.Solve(m);

           
            AssetDatabase.CreateAsset(m, "Assets/Editor/" + planeAssetName);
            AssetDatabase.SaveAssets();
        }
       
        meshFilter.sharedMesh = m;
        m.RecalculateBounds();
       
        if (addCollider)
            plane.AddComponent(typeof(BoxCollider));
       
        Selection.activeObject = plane;
    }
	
	class TangentSolver
	{
	    public static void Solve(Mesh mesh)
	    {
	        int triangleCount = mesh.triangles.Length / 3;
	        int vertexCount = mesh.vertices.Length;
	
	        Vector3[] tan1 = new Vector3[vertexCount];
	        Vector3[] tan2 = new Vector3[vertexCount];
	
	        Vector4[] tangents = new Vector4[vertexCount];
	
	        for(long a = 0; a < triangleCount; a+=3)
	        {
	            long i1 = mesh.triangles[a+0];
	            long i2 = mesh.triangles[a+1];
	            long i3 = mesh.triangles[a+2];
	
	            Vector3 v1 = mesh.vertices[i1];
	            Vector3 v2 = mesh.vertices[i2];
	            Vector3 v3 = mesh.vertices[i3];
	
	            Vector2 w1 = mesh.uv[i1];
	            Vector2 w2 = mesh.uv[i2];
	            Vector2 w3 = mesh.uv[i3];
	
	            float x1 = v2.x - v1.x;
	            float x2 = v3.x - v1.x;
	            float y1 = v2.y - v1.y;
	            float y2 = v3.y - v1.y;
	            float z1 = v2.z - v1.z;
	            float z2 = v3.z - v1.z;
	
	            float s1 = w2.x - w1.x;
	            float s2 = w3.x - w1.x;
	            float t1 = w2.y - w1.y;
	            float t2 = w3.y - w1.y;
	
	            float r = 1.0f / (s1 * t2 - s2 * t1);
	
	            Vector3 sdir = new Vector3((t2 * x1 - t1 * x2) * r, (t2 * y1 - t1 * y2) * r, (t2 * z1 - t1 * z2) * r);
	            Vector3 tdir = new Vector3((s1 * x2 - s2 * x1) * r, (s1 * y2 - s2 * y1) * r, (s1 * z2 - s2 * z1) * r);
	
	            tan1[i1] += sdir;
	            tan1[i2] += sdir;
	            tan1[i3] += sdir;
	
	            tan2[i1] += tdir;
	            tan2[i2] += tdir;
	            tan2[i3] += tdir;
	        }
	
	
	        for (long a = 0; a < vertexCount; ++a)
	        {
	            Vector3 n = mesh.normals[a];
	            Vector3 t = tan1[a];
	
	            Vector3 tmp = (t - n * Vector3.Dot(n, t)).normalized;
	            tangents[a] = new Vector4(tmp.x, tmp.y, tmp.z);
	
	            tangents[a].w = (Vector3.Dot(Vector3.Cross(n, t), tan2[a]) < 0.0f) ? -1.0f : 1.0f;
	        }
	
	        mesh.tangents = tangents;
	    }
	}
}