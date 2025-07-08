using UnityEngine;

public static class GameManagerUtils
{
    public static void SetBoatSpeed(float BoatSpeed)
    {
        GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameController");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
        gameManager.SetBoatSpeed(BoatSpeed);
    }

    public static float GetBoatSpeed()
    {
        GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameController");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();

        return gameManager.GetBoatSpeed();
    }

    public static void SetPlayerHasGun(bool bHasGun)
    {
        GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameController");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();
        gameManager.SetPlayerHasGun(bHasGun);
    }

    public static bool GetPlayerHasGun()
    {
        GameObject gameManagerObject = GameObject.FindGameObjectWithTag("GameController");
        GameManager gameManager = gameManagerObject.GetComponent<GameManager>();

        return gameManager.GetPlayerHasGun();
    }
}
