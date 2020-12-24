using Photon.Pun;
using UnityEngine;

public class HPPointBehaviour : MonoBehaviourPun
{
    public int IncreasedHP { get; set; }

    private GameObject hpPivotCorrector;

    private void Start()
    {
        //hpPivotCorrector = GetComponentInParent<Transform>();
        IncreasedHP += 25;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.Player.ToString()) && base.photonView.IsMine)
        {
            if(other.gameObject.CompareTag(Tag.Player.ToString()))
                other.GetComponent<PlayerHP>().IncreaseHP(IncreasedHP);
            
            gameObject.SetActive(false);
            //hpPivotCorrector.SetActive(false);
        }
    }
}