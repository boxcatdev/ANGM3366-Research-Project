using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// This component must be placed no the light gameobject that will be affected.
/// </summary>
[RequireComponent(typeof(Light))]
[DisallowMultipleComponent]
public class FlickeringLightEffect : MonoBehaviour
{
    [Header("Flicker Properties")]
    [Tooltip("Max time in seconds that the light will be on before it flickers.")]
    [SerializeField] float _maxOnTime = 3.0f;
    [Tooltip("Max time in seconds that the light will be off when it flickers.")]
    [SerializeField] float _maxOffTime = 0.25f;
    [Tooltip("Determines how dim the light will be when flickering (intensity is multiplied by this value.")]
    [Range(0.0f, 1.0f)]
    [SerializeField] float _dimPercentage = 0.0f;

    private Light _light;
    private float _startingIntensity;

    private float _onInterval;
    private float _offInterval;

    #region Editor Properties
    //material properties
    [HideInInspector]
    public bool changeMaterial;

    [HideInInspector]
    public MeshRenderer materialObject;

    [HideInInspector]
    public Material onMaterial;

    [HideInInspector]
    public Material offMaterial;
    #endregion

    #region Monobehavior
    private void Awake()
    {
        _light = GetComponent<Light>();
    }
    private void Start()
    {
        if (_light != null)
            _startingIntensity = _light.intensity;
        else
            Debug.LogError("Missing light component.");
    }
    private void Update()
    {
        if (_light.intensity == _startingIntensity) //when light is on
        {
            _onInterval -= Time.deltaTime;

            if (_onInterval <= 0) //if countdown timer has finished
            {
                //turn off light
                ToggleLightIntensity(false);

                AssignRandomOffInterval(0.1f);
            }
        }
        else //when light is off
        {
            _offInterval -= Time.deltaTime;

            if (_offInterval <= 0) //if countdown timer has finished
            {
                //turn on light
                ToggleLightIntensity(true);

                AssignRandomOnInterval(0.1f);
            }
        }
    }
    #endregion

    #region Private Functions
    private void ToggleLightIntensity(bool lightOn)
    {
        _light.intensity = (lightOn) ? _startingIntensity : _startingIntensity * _dimPercentage;

        if(changeMaterial) ToggleLightMaterial(lightOn);
    }
    private void ToggleLightMaterial(bool materialOn)
    {
        if(materialObject != null && onMaterial != null && offMaterial != null)
        {
            if (materialOn)
            {
                materialObject.material = onMaterial;
            }
            else
            {
                materialObject.material = offMaterial;
            }
        }
    }
    private void AssignRandomOnInterval(float minMultiplier)
    {
        //assign random value for on time
        _onInterval = Random.Range(_maxOnTime * ((minMultiplier < 1f)? minMultiplier : 0.1f), _maxOnTime);
    }
    private void AssignRandomOffInterval(float minMultiplier)
    {
        //assign random value for off time
        _offInterval = Random.Range(_maxOffTime * ((minMultiplier < 1f) ? minMultiplier : 0.1f), _maxOffTime);

    }
    #endregion

    #region Public Functions
    public float GetMaxOnTime()
    {
        return _maxOnTime;
    }
    public float GetMaxOffTime()
    {
        return _maxOffTime;
    }
    public float GetDimPercentage()
    {
        return _dimPercentage;
    }
    /// <summary>
    /// Modifies _maxOnTime if input value is greater than 0f.
    /// </summary>
    /// <param name="maxOnTime"></param>
    public void SetMaxOnTime(float maxOnTime)
    {
        if (maxOnTime > 0.0f)
            _maxOnTime = maxOnTime;
        else
            Debug.LogError("Cannot modify component using provided values.");
    }
    /// <summary>
    /// Modifies _maxOffTime if input value is greater than 0f.
    /// </summary>
    /// <param name="maxOffTime"></param>
    public void SetMaxOffTime(float maxOffTime)
    {
        if (maxOffTime > 0.0f)
            _maxOffTime = maxOffTime;
        else
            Debug.LogError("Cannot modify component using provided values.");
    }
    /// <summary>
    /// Modifies _dimPercentage if input value is less than or equal to 1f.
    /// </summary>
    /// <param name="dimPercentage"></param>
    public void SetDimPercentage(float dimPercentage)
    {
        if (dimPercentage <= 1.0f)
            _dimPercentage = dimPercentage;
        else
            Debug.LogError("Cannot modify component using provided values.");
    }
    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(FlickeringLightEffect))]
public class FlickeringLightEffect_Editor : Editor
{
    SerializedProperty materialObjectProp;
    SerializedProperty onMaterialProp;
    SerializedProperty offMaterialProp;

    private void OnEnable()
    {
        materialObjectProp = serializedObject.FindProperty("materialObject");
        onMaterialProp = serializedObject.FindProperty("onMaterial");
        offMaterialProp = serializedObject.FindProperty("offMaterial");
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FlickeringLightEffect script = (FlickeringLightEffect)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Material Properties", EditorStyles.boldLabel);

        //draw checkbox for materials
        script.changeMaterial = EditorGUILayout.Toggle("Change Material", script.changeMaterial);
        if (script.changeMaterial)
        {
            EditorGUILayout.PropertyField(materialObjectProp, new GUIContent("Material Object"));
            EditorGUILayout.PropertyField(onMaterialProp, new GUIContent("On Material"));
            EditorGUILayout.PropertyField(offMaterialProp, new GUIContent("Off Material"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
