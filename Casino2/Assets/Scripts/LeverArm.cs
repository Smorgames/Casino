using UnityEngine;

public class LeverArm : MonoBehaviour
{
    private CircleCollider2D circleCollider;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnMouseDown()
    {
        if (GameMaster.instance.bet <= GameMaster.instance.money)
        {
            ChangeLeverArm(-1, false);
            GameMaster.instance.StartRoll();
        }
    }

    public void ChangeLeverArm(int scaleMultiplyer, bool isColliderActive)
    {
        transform.localScale = new Vector3(3, scaleMultiplyer * 3, 1);
        circleCollider.enabled = isColliderActive;
    }
}
