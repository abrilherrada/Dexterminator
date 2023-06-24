using UnityEngine;

public interface IInteractable
{
    bool IsInteractable { get; }
    void Interact();
}

public class NPC : Entity, IInteractable
{
    [SerializeField] private string[] dialogue;

    private void Awake()
    {
        GetName();
    }

    public bool IsInteractable { get; }
    public void Interact()
    {

    }
}
