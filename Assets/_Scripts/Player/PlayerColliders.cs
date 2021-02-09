using UnityEngine;

public class PlayerColliders : MonoBehaviour
{
    [HideInInspector]
    public bool IsOnEdge;

    public float ArraySpacingFromMiddlePoint = 0.15f;
    private int _layerMaskGround = 1 << 8;

    [HideInInspector]
    public RaycastHit HitInfoLeft;

    [HideInInspector]
    public RaycastHit HitInfoRight;

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.forward / 4, Color.red);
        CheckPlayerOnPlatform();
    }

    private void CheckPlayerOnPlatform()
    {
        var hitGround1 = Physics.Raycast(transform.localPosition + new Vector3(-ArraySpacingFromMiddlePoint, 0, 0), Vector3.down,
            out HitInfoLeft, 1.1f, _layerMaskGround);
        var hitGround2 = Physics.Raycast(transform.localPosition + new Vector3(ArraySpacingFromMiddlePoint, 0, 0), Vector3.down,
            out HitInfoRight, 1.1f, _layerMaskGround);

        Debug.DrawRay(transform.localPosition + new Vector3(-ArraySpacingFromMiddlePoint, 0, 0), Vector3.down * 1.1f, Color.red);
        Debug.DrawRay(transform.localPosition + new Vector3(ArraySpacingFromMiddlePoint, 0, 0), Vector3.down * 1.1f, Color.red);
    }
}