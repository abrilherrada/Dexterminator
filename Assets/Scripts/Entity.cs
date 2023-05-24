using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private string entityName;
    [SerializeField] private string id;

    public string GetName() => entityName;

    public string GetID() => id;
}
