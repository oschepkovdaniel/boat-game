using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [Header("Widgets")]
    [SerializeField] private Image CrosshairImage;
    [SerializeField] private Text InteractionText;

    private GameObject player;
    private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCrosshair();
        UpdateInteractionText();
    }

    private void UpdateCrosshair()
    {
        CrosshairImage.gameObject.SetActive(playerController.GetHasInteract());
    }

    private void UpdateInteractionText()
    {
        InteractionText.text = playerController.GetInteractionHint();
    }
}
