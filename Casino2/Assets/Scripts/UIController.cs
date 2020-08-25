using UnityEngine;

public class UIController : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeBet()
    {
        GameMaster.instance.ChangeBet();
    }
}
