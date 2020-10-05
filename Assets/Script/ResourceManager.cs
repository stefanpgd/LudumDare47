using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable 649

public class ResourceManager : MonoBehaviour
{
    public delegate void ResourceManagerUpdate();
    public event ResourceManagerUpdate ResourceManagerUpdateEvent;

    // Resources:
    public int gold;
    public int kills;
    public int roomsCompleted;

    #region Singleton
    public static ResourceManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Update()
    {
        ResourceManagerUpdateEvent?.Invoke();
    }

    public void AddResource(ResourceType type, int value)
    {
        switch (type)
        {
            case ResourceType.Gold:
                gold += value;
                break;

            case ResourceType.Kills:
                kills += value;
                break;

            case ResourceType.RoomsCompleted:
                roomsCompleted += value;
                break;
        }

        ResourceManagerUpdateEvent?.Invoke();
    }

    public void RemoveResource(ResourceType type, int value)
    {
        switch (type)
        {
            case ResourceType.Gold:
                gold -= value;

                if (gold < 0f)
                {
                    gold = 0;
                }
                break;
        }

        ResourceManagerUpdateEvent?.Invoke();
    }

    public int GetResourceValue(ResourceType type = ResourceType.Gold)
    {
        switch (type)
        {
            default:
                return gold;

            case ResourceType.Gold:
                return gold;

            case ResourceType.Kills:
                return kills;

            case ResourceType.RoomsCompleted:
                return roomsCompleted;
        }
    }

    public void ResetAllResources()
    {
        gold = 0;
        kills = 0;
        roomsCompleted = 0;
        Debug.Log("reset");
    }
}
