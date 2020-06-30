using UnityEngine;

public class HPPointBehaviour : MonoBehaviour
{
    public int IncreasedHP { get; set; }

    [SerializeField]
    private GameObject hpPivotCorrector;

    private void Start()
    {
        IncreasedHP += 25;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.Player.ToString()))
        {
            other.GetComponent<PlayerHP>().IncreaseHP(IncreasedHP);
            hpPivotCorrector.SetActive(false);
        }
    }
}