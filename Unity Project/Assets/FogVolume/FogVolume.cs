/*CHANGE LOG
 * Added cute icon
 * Hack to avoid bug that breaks the effect when VolumeSize.x = VolumeSize.y = VolumeSize.z
 * fixed build warnings
 * added noise color tweaks (contrast, intensity)
 * fixed data update. Effect won't dissapear anymore in editor mode
 * Inscattering is now inside Volume boundings
 * Fixed Noise intensity & contrast
 * Public access to visibility (used by FoggyLights)
 * Shader variants fixes
 * Copy-paste will create a new material now
 * Gradients first implementation
 * Geometry created procedurally. (Unity 5 has scale issues)
 * Fixed a bug where mesh was not scaled in-game
 * */

using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]

public class FogVolume : MonoBehaviour
{
	GameObject FogVolumeGameObject;
	public Camera ForwardRenderingCamera;
	[HideInInspector]
	public Material
		FogMaterial;
	[SerializeField]
	Color
		InscatteringColor = Color.white, FogColor = new Color (.5f, .6f, .7f, 1);
	public float Visibility = 5;
	[Range(-1, 1)]
	public float
		InscatteringShape = 0;
	public float InscatteringIntensity = 2, InscatteringStartDistance = 400, InscatteringTransitionWideness = 1, _3DNoiseScale = 300, _3DNoiseStepSize = 50;
	public Texture3D _NoiseVolume = null;
	[Range(0f, 10)]
	public float
		NoiseIntensity = 1;
   // public int IntersectionThreshold=60;
	[Range(0f, 5f)]
	public float
		NoiseContrast = 0;
	[SerializeField]
	Light
		Sun;
	[SerializeField]
	int
		DrawOrder = 0;
	public Texture2D Gradient;
	[SerializeField]
	bool
		EnableGradient = false, EnableInscattering = false, EnableNoise = false;
	public Vector4 Speed = new Vector4 (0, 0, 0, 0);
	public Vector4 Stretch = new Vector4 (0, 0, 0, 0);
        
      //  #if UNITY_EDITOR
        public bool HideWireframe = false;
      //  #endif

        MeshFilter filter;
        Mesh mesh;
        public Vector3 fogVolumeScale = new Vector3(20, 20, 20);
        Vector3 currentFogVolume = Vector3.one;

	public float GetVisibility ()
	{
		return Visibility;
	}

	void CreateMaterial ()
	{            
		FogMaterial = new Material (Shader.Find ("Hidden/FogVolume"));
		FogMaterial.name = "Fog Material";
		FogMaterial.hideFlags = HideFlags.HideAndDontSave;
	}

	void SetCameraDepth ()
	{
		if (ForwardRenderingCamera)
			ForwardRenderingCamera.depthTextureMode |= DepthTextureMode.Depth;
	}

	void OnEnable ()
	{                        
		CreateMaterial ();
		SetCameraDepth ();
		FogVolumeGameObject = this.gameObject;
		FogVolumeGameObject.GetComponent<Renderer> ().sharedMaterial = FogMaterial;
		ToggleKeyword ();
        filter = gameObject.GetComponent<MeshFilter>();
        if (filter == null)
        {
            CreateBoxMesh(transform.localScale);
            transform.localScale = Vector3.one;
        }
        UpdateBoxMesh();
            
	}

	static public void Wireframe (GameObject obj, bool Enable)
	{
		#if UNITY_EDITOR
            EditorUtility.SetSelectedWireframeHidden(obj.GetComponent<Renderer>(), Enable);
		#endif
	}

	void Update ()
	{
		#if UNITY_EDITOR
			UpdateBoxMesh();
            Visibility = Mathf.Max(0.01f, Visibility);
            _3DNoiseStepSize = Mathf.Max(1, _3DNoiseStepSize);          
            InscatteringIntensity = Mathf.Max(0, InscatteringIntensity);
            NoiseIntensity = Mathf.Max(0, NoiseIntensity);
            InscatteringTransitionWideness = Mathf.Max(1, InscatteringTransitionWideness);
            InscatteringStartDistance = Mathf.Max(0, InscatteringStartDistance);
            if (_NoiseVolume == null)
                EnableNoise = false;
		#endif
	}

	void OnWillRenderObject ()
	{
		#if UNITY_EDITOR
            ToggleKeyword();
            Wireframe(FogVolumeGameObject, HideWireframe);
		#endif

		FogMaterial.SetColor ("_Color", FogColor);
		FogMaterial.SetColor ("_InscatteringColor", InscatteringColor);

		if (Sun) {
			FogMaterial.SetFloat ("_InscatteringIntensity", InscatteringIntensity);
			FogMaterial.SetVector ("L", -Sun.transform.forward);
			FogMaterial.SetFloat ("InscatteringShape", InscatteringShape);
			FogMaterial.SetFloat ("InscatteringTransitionWideness", InscatteringTransitionWideness);
		}

		if (EnableNoise && _NoiseVolume) {

			Shader.SetGlobalTexture ("_NoiseVolume", _NoiseVolume);                
			FogMaterial.SetFloat ("gain", NoiseIntensity);
			FogMaterial.SetFloat ("threshold", NoiseContrast * 0.5f);
			FogMaterial.SetFloat ("_3DNoiseScale", _3DNoiseScale * .001f);
			FogMaterial.SetFloat ("_3DNoiseStepSize", _3DNoiseStepSize * .001f);
            //FogMaterial.SetFloat("IntersectionThreshold", IntersectionThreshold);
			FogMaterial.SetVector ("Speed", Speed);
			FogMaterial.SetVector ("Stretch", new Vector4 (1, 1, 1, 1) + Stretch * .01f);
			//FogMaterial.SetInt("_NoiseSamples", _NoiseSamples);
		}

		if (Gradient != null)
			FogMaterial.SetTexture ("_Gradient", Gradient);           

		FogMaterial.SetFloat ("InscatteringStartDistance", InscatteringStartDistance);
		Vector3 VolumeSize = currentFogVolume;
		FogMaterial.SetVector ("_BoxMin", VolumeSize * -.5f);
		FogMaterial.SetVector ("_BoxMax", VolumeSize * .5f);

		FogMaterial.SetFloat ("_Visibility", Visibility);
		GetComponent<Renderer> ().sortingOrder = DrawOrder;

        Matrix4x4 viewMat = Camera.main.worldToCameraMatrix;
        Matrix4x4 projMat =Camera.main.projectionMatrix;
        Matrix4x4 viewProjMat = (projMat * viewMat);
        FogMaterial.SetMatrix("_ViewProjInv", viewProjMat.inverse);

	}

	void ToggleKeyword ()
	{
		if (FogMaterial) {
			if (EnableNoise && SystemInfo.supports3DTextures)
				FogMaterial.EnableKeyword ("_FOG_VOLUME_NOISE");
			else
				FogMaterial.DisableKeyword ("_FOG_VOLUME_NOISE");

			if (EnableInscattering && Sun)
				FogMaterial.EnableKeyword ("_FOG_VOLUME_INSCATTERING");
			else
				FogMaterial.DisableKeyword ("_FOG_VOLUME_INSCATTERING");

			if (EnableGradient && Gradient != null)
				FogMaterial.EnableKeyword ("_FOG_GRADIENT");
			else
				FogMaterial.DisableKeyword ("_FOG_GRADIENT");
                
		}
	}

	

	void UpdateBoxMesh ()
	{			
		if (currentFogVolume != fogVolumeScale || filter == null)
			CreateBoxMesh (fogVolumeScale);
				
		transform.localScale = Vector3.one;
	}

	void CreateBoxMesh (Vector3 scale)
	{
		currentFogVolume = scale;

		// You can change that line to provide another MeshFilter
		if (filter == null)
			filter = gameObject.AddComponent< MeshFilter > ();
        
		mesh = filter.sharedMesh;

		if (mesh == null) {
			mesh = new Mesh ();
			mesh.name = gameObject.name;
			filter.sharedMesh = mesh;
		}
		mesh.Clear ();

		float width = scale.y;
		float height = scale.z;
		float length = scale.x;
			
		#region Vertices
		Vector3 p0 = new Vector3 (-length * .5f, -width * .5f, height * .5f);
		Vector3 p1 = new Vector3 (length * .5f, -width * .5f, height * .5f);
		Vector3 p2 = new Vector3 (length * .5f, -width * .5f, -height * .5f);
		Vector3 p3 = new Vector3 (-length * .5f, -width * .5f, -height * .5f);	
			
		Vector3 p4 = new Vector3 (-length * .5f, width * .5f, height * .5f);
		Vector3 p5 = new Vector3 (length * .5f, width * .5f, height * .5f);
		Vector3 p6 = new Vector3 (length * .5f, width * .5f, -height * .5f);
		Vector3 p7 = new Vector3 (-length * .5f, width * .5f, -height * .5f);
			
		Vector3[] vertices = new Vector3[]
			{
		// Bottom
				p0, p1, p2, p3,
				
		// Left
				p7, p4, p0, p3,
				
		// Front
				p4, p5, p1, p0,
				
		// Back
				p6, p7, p3, p2,
				
		// Right
				p5, p6, p2, p1,
				
		// Top
				p7, p6, p5, p4
			};
		#endregion
			
		#region Triangles
		int[] triangles = new int[]
			{
		// Bottom
				3, 1, 0,
				3, 2, 1,			
				
		// Left
				3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
				3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
				
		// Front
				3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
				3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
				
		// Back
				3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
				3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
				
		// Right
				3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
				3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
				
		// Top
				3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
				3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,
				
			};
		#endregion
			
		mesh.vertices = vertices;
		mesh.triangles = triangles;
			
		mesh.RecalculateBounds ();
		mesh.Optimize ();
	}

	void Start ()
	{
		
	}

}

