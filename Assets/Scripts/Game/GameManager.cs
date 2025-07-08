using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("GameObjects")]
    [SerializeField] private MeshRenderer Ocean;

    [Header("CPlayer")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject Gun;

    private float BoatSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateBoatSpeed();
    }

    private void UpdateBoatSpeed()
    {
        Ocean.materials[0].SetFloat("BoatSpeed", BoatSpeed);
    }

    // public methods
    public void SetBoatSpeed(float BoatSpeed)
    {
        this.BoatSpeed = BoatSpeed;
    }

    public float GetBoatSpeed()
    {
        return BoatSpeed;
    }

    public void SetPlayerHasGun(bool bHasGun)
    {
        if (bHasGun)
        {
            playerController.SetHasGun(true);
            Gun.SetActive(false);
        }
        else
        {
            playerController.SetHasGun(false);
            Gun.SetActive(true);
        }
    }

    public bool GetPlayerHasGun()
    {
        return playerController.GetHasGun();
    }
}
