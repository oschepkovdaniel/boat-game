using UnityEngine;

public class TestUsable : MonoBehaviour, IUsableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {
        Debug.Log("Hello");
    }

    public string GetInteractionHint()
    {
        return "[E] TEST INTERACT";
    }
}
