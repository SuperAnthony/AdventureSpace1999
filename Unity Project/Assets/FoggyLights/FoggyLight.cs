/*CHANGE LOG
 * Affected now by Fog Volume visibility
 * Data update fix. Won't dissappear anymore in editor
 * Icon format fix
 * Turning "attatch light" button off crashed the editor. This button will turn it off instead
 * Built-in fog won't affect it
 * Fixed d3d9 shader error
 * */

using UnityEngine;
using System.Collections;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]


public class FoggyLight : MonoBehaviour
{


    [SerializeField]
    GameObject FogVolumeContainer = null;
    public bool InsideFogVolume = false;
    

    private Component FogVolumeComponent = null;

    public Color PointLightColor = Color.white;
    Vector3 Position;
    public float PointLightIntensity = .3f, PointLightExponent = 5, Offset = -2;
    public int DrawOrder = 1;
    public bool HideWireframe = true, AttatchLight = false;
    Light AttachedLight = null;
    Material FoggyLightMaterial;

    static public void Wireframe(GameObject Obj, bool Enable)
    {
#if UNITY_EDITOR
        EditorUtility.SetSelectedWireframeHidden(Obj.GetComponent<Renderer>(), Enable);
#endif
    }
    void CreateMaterial()
    {
        if (!FoggyLightMaterial)
        {
            FoggyLightMaterial = new Material(Shader.Find("Hidden/FoggyLight"));
            FoggyLightMaterial.name = this.name.ToString() + " Material";
            gameObject.GetComponent<Renderer>().sharedMaterial = FoggyLightMaterial;
            FoggyLightMaterial.hideFlags = HideFlags.HideAndDontSave;
        }
    }
    void Start()
    {
        
    }

    void OnEnable()
    {
        CreateMaterial();
		Camera.main.depthTextureMode |= DepthTextureMode.Depth;
    }

    void OnWillRenderObject()
    {
        GetComponent<Renderer>().sortingOrder = DrawOrder;
        Position = gameObject.transform.position;
        PointLightIntensity = Mathf.Max(0, PointLightIntensity);
        PointLightExponent = Mathf.Max(1, PointLightExponent);

#if UNITY_EDITOR
        Wireframe(gameObject, HideWireframe);
#endif

        Position = gameObject.transform.position;
        GetComponent<Renderer>().sharedMaterial.SetColor("PointLightColor", PointLightColor);

        if (FogVolumeContainer && InsideFogVolume)
        {
            if (!FogVolumeComponent)
                FogVolumeComponent = FogVolumeContainer.GetComponent("FogVolume");


            FoggyLightMaterial.EnableKeyword("_FOG_CONTAINER");
            //            renderer.sharedMaterial.SetFloat("_Visibility", FogVolumeComponent.Visibility);
            float valueVisibility = (float)FogVolumeComponent.GetType().GetMethod("GetVisibility").Invoke(FogVolumeComponent, null);
            GetComponent<Renderer>().sharedMaterial.SetFloat("_Visibility", valueVisibility);



        }
        else
            FoggyLightMaterial.DisableKeyword("_FOG_CONTAINER");

        GetComponent<Renderer>().sharedMaterial.SetVector("PointLightPosition", Position);
        GetComponent<Renderer>().sharedMaterial.SetFloat("PointLightIntensity", PointLightIntensity);
        GetComponent<Renderer>().sharedMaterial.SetFloat("PointLightExponent", PointLightExponent);
        GetComponent<Renderer>().sharedMaterial.SetFloat("Offset", Offset);



        if (AttatchLight)
        {
            if (!gameObject.GetComponent<Light>())
            {
                gameObject.AddComponent<Light>();
                gameObject.GetComponent<Light>().shadows = LightShadows.Hard;
            }

            AttachedLight = gameObject.GetComponent<Light>();

            if (AttachedLight)
            {
                AttachedLight.intensity = PointLightIntensity / 2;
                AttachedLight.color = PointLightColor;

                AttachedLight.enabled = true;
            }
        }
        else
        {            
                if (AttachedLight)

                    AttachedLight.enabled = false;
           
        }
    }
}
