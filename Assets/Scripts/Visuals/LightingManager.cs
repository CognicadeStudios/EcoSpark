using System;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    public class HourChangedArgs
    {
        public int hour;
        public HourChangedArgs(int hour)
        {
            this.hour = hour;
        }
    }

    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
        
    //Variables
    [SerializeField, Range(0, 24)] public float TimeOfDay;
    public float TimeElapsed = 0.0f;

    [SerializeField, Range(0.0f, 1.0f)] private float TimeSpeed;

    public static LightingManager instance;
    public bool isNight;

    private void Awake()
    {
        TimeElapsed = TimeOfDay;
        instance = this;
    }

    private float previousTick;

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            //(Replace with a reference to the game time)
            TimeOfDay += Time.deltaTime * TimeSpeed;
            TimeElapsed += Time.deltaTime * TimeSpeed;
            TimeOfDay %= 24; //Modulus to ensure always between 0-24
            
            /*if(firedUpdate && (TimeOfDay - Mathf.FloorToInt(TimeOfDay) > 0.5f))
            {
                firedUpdate = false;
                //30 minutes in (reset the update flag)
            }
            if(!firedUpdate && TimeOfDay - Mathf.FloorToInt(TimeOfDay) < 0.1f)
            {
                BuildingController.HourlyValueUpdates(null, null);
                //Debug.Log("Event fired");
                firedUpdate = true;
            }
            @Arush IDK what this does so i replaced it
            */

            if(TimeElapsed % 1 <= previousTick % 1)
            {
                BuildingController.HourlyValueUpdates(null, null);
            }

            previousTick = TimeOfDay;
            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }

        isNight = TimeOfDay is not (> 6f and < 18f);
    }


    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);
        RenderSettings.ambientSkyColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }

    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
