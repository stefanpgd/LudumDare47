using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
#pragma warning disable 649

public class UIResourceDisplay : MonoBehaviour
{
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private TextMeshProUGUI textObject;

    private ResourceManager resourceManager;

    private void Start()
    {
        resourceManager = ResourceManager.Instance;
        resourceManager.ResourceManagerUpdateEvent += UpdateText;
    }

    private void OnDisable()
    {
        resourceManager.ResourceManagerUpdateEvent -= UpdateText;
    }

    private void UpdateText()
    {
        textObject.text = resourceManager.GetResourceValue(resourceType).ToString();
    }
}
