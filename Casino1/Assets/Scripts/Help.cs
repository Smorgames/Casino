using UnityEngine;

public class Help : MonoBehaviour
{
    public GameObject helpScheme;

    public void HelpToggle()
    {
        helpScheme.SetActive(!helpScheme.activeSelf);
    }
}
