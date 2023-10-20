using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// This component does not need to be on a game object with a light component.
/// </summary>
public class FlickeringLightEffectSynced : MonoBehaviour
{
    [Header("Flicker Properties")]
    [Tooltip("Max time in seconds that the lights will be on before they flicker.")]
    [SerializeField] float _maxOnTime = 3.0f;
    [Tooltip("Max time in seconds that the lights will be off when they flicker.")]
    [SerializeField] float _maxOffTime = 0.25f;
    [Tooltip("Determines how dim the lights will be when flickering (intensity is multiplied by this value.")]
    [Range(0.0f, 1.0f)]
    [SerializeField] float _dimPercentage = 0.0f;

    [Header("Synced Lights")]
    [Tooltip("The light objects that will be affected by the component.")]
    [SerializeField] private Light[] _lights;

    //private Light _lights;
    private float[] _startingIntensities;

    private float _onInterval;
    private float _offInterval;

    #region Editor Properties
    //material properties
    [HideInInspector]
    public bool changeMaterial;

    [HideInInspector]
    public MeshRenderer[] materialObjects;

    [HideInInspector]
    public Material onMaterial;

    [HideInInspector]
    public Material offMaterial;
    #endregion

    #region Monobehavior
    private void Awake()
    {
        if(_lights.Length > 0) _startingIntensities = new float[_lights.Length];
    }
    private void Start()
    {
        if (_lights.Length > 0)
        {
            for (int i = 0; i < _lights.Length; i++)
            {
                _startingIntensities[i] = _lights[i].intensity;
            }
        }
        else
            Debug.LogError("Missing light components.");
    }
    private void Update()
    {
        if (_lights.Length == 0) return;

        if (_lights[0].intensity > _startingIntensities[0] * _dimPercentage) //when lights are on
        {
            _onInterval -= Time.deltaTime;

            if (_onInterval <= 0) //if countdown timer has finished
            {
                //turn off lights
                ToggleLightIntensity(false);

                AssignRandomOffInterval(0.5f);
            }
        }
        else //when lights are off
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
        for (int i = 0; i < _lights.Length; i++)
        {
            _lights[i].intensity = (lightOn) ? _startingIntensities[i] : _startingIntensities[i] * _dimPercentage;
        }

        if (changeMaterial) ToggleLightMaterial(lightOn);
    }
    private void ToggleLightMaterial(bool materialOn)
    {
        if (materialObjects != null && onMaterial != null && offMaterial != null)
        {
            for (int i = 0; i < materialObjects.Length; i++)
            {
                if (materialOn)
                {
                    materialObjects[i].material = onMaterial;
                }
                else
                {
                    materialObjects[i].material = offMaterial;
                }
            }
        }
    }
    private void AssignRandomOnInterval(float minMultiplier)
    {
        //assign random value for on time
        _onInterval = Random.Range(_maxOnTime * ((minMultiplier < 1f) ? minMultiplier : 0.1f), _maxOnTime);
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
[CustomEditor(typeof(FlickeringLightEffectSynced))]
public class FlickeringLightEffectSynced_Editor : Editor
{
    SerializedProperty materialObjectProp;
    SerializedProperty onMaterialProp;
    SerializedProperty offMaterialProp;

    private void OnEnable()
    {
        materialObjectProp = serializedObject.FindProperty("materialObjects");
        onMaterialProp = serializedObject.FindProperty("onMaterial");
        offMaterialProp = serializedObject.FindProperty("offMaterial");
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FlickeringLightEffectSynced script = (FlickeringLightEffectSynced)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Material Properties", EditorStyles.boldLabel);

        //draw checkbox for materials
        script.changeMaterial = EditorGUILayout.Toggle("Change Materials", script.changeMaterial);
        if (script.changeMaterial)
        {
            EditorGUILayout.PropertyField(materialObjectProp, new GUIContent("Material Objects"));
            EditorGUILayout.PropertyField(onMaterialProp, new GUIContent("On Material"));
            EditorGUILayout.PropertyField(offMaterialProp, new GUIContent("Off Material"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
