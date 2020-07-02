using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    private int _hp;

    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value > 100)
            {
                SetMaxHP();
                _hp = 100;
            }
            else if (value < 0)
            {
                SetMinHP();
                _hp = 0;
            }
            else
            {
                _hp = value;
            }
        }
    }

    private GameObject _hpBar;

    private void Start()
    {
        _hpBar = GameObject.FindGameObjectWithTag(Tag.PlayerHp.ToString());
        SetMaxHP();
    }

    public void SubtractHP(int damage)
    {
        if (HP > 0)
        {
            HP -= damage;
            var percentageOfDamage = ((float)damage / 100);
            _hpBar.transform.localScale -= new Vector3(percentageOfDamage, 0, 0);
        }
        if (HP <= 0)
        {
            SetMinHP();
        }
    }

    public void IncreaseHP(int health)
    {
        if (HP < 100)
        {
            HP += health;
            var percentageOfHealth = ((float)health / 100);
            _hpBar.transform.localScale += new Vector3(percentageOfHealth, 0, 0);
        }
        if (HP >= 100)
        {
            SetMaxHP();
        }
    }

    public void SetMaxHP()
    {
        HP = 100;
        _hpBar.transform.localScale = new Vector3(1, _hpBar.transform.localScale.y, _hpBar.transform.localScale.z);
    }

    public void SetMinHP()
    {
        HP = 0;
        _hpBar.transform.localScale = new Vector3(0, _hpBar.transform.localScale.y, _hpBar.transform.localScale.z);
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        //Debug
        if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.X))
        {
            SubtractHP(10);
        }
    }
}