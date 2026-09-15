using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamUnit : Unit
{
    private GameObject gameManager;
    private Transform enemyP;
    [SerializeField]
    private int type;
    

    [SerializeField]
    private GameObject weapon;

    private Transform shootP;

    private Transform shootPos;

    private Animator ani;

    private RaycastHit hit;

    private Renderer thisRenderer;
    private Transform target;
    Color hitColor;

    private float thisPosX;
    private float thisPosY;
    bool pushTileStatus = false;

    private float redColorTimer;

    private void Start()
    {
        hitColor = new Color(1, 0.5f, 0.5f);
        gameManager = GameObject.Find("GameManager");
        shootP = GameObject.Find("Bullets").transform;
        enemyP = GameObject.Find("enemyParent").transform;
        ani = this.GetComponent<Animator>();
        shootPos = this.transform.GetChild(1);
        Current_health = 100;
        Damage = 50f;

        thisRenderer = this.transform.GetChild(0).GetComponent<Renderer>();
        Damage = gameManager.GetComponent<TeamStats>().Damage[type - 1];
        Max_Health = gameManager.GetComponent<TeamStats>().HP[type - 1];
        Current_health = Max_Health;
        switch (type)
        {
            case 1:
                Attack_range = 5f;
                break;
            case 2:
                Attack_range = 5f;
                break;
            case 3:
                Attack_range = 1f;
                break;
            case 4:
                Attack_range = 0f;
                break;
            case 5:
                Attack_range = 100f;
                break;
        }
        thisPosX = ((this.transform.position.x + 2.5f) / 5);
        thisPosY = ((-2.5f + this.transform.position.z) / 5);
    }

    private void Update()
    {
        if (!pushTileStatus)
        {
            TileStatus.tileStatus[(int)thisPosX, (int)thisPosY, 1] = this.gameObject;
            pushTileStatus = true;
        }
        int layerMask = 1 << LayerMask.NameToLayer("Enemys");
        if (type == 1)
        {
            if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Breathing Idle"))
            {
                 if (Physics.Raycast(this.transform.position + new Vector3(0, 1.5f, 0), this.transform.TransformDirection(Vector3.forward),Attack_range * 5,layerMask))
                 {
                     ani.Play("Throw");
                     StartCoroutine(shoot());
                     return;
                 } 
            }
        }
        else if (type == 2)
        {
            if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Breathing Idle"))
            {
                if (Physics.Raycast(this.transform.position + new Vector3(0, 1.5f, 0), this.transform.TransformDirection(Vector3.forward), Attack_range * 5, layerMask))
                {
                    ani.Play("Magic Heal");
                    StartCoroutine(shoot());
                    return;
                }
            }
        }
        else if (type == 3)
        {
            if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Great Sword Idle"))
            {
                if (Physics.Raycast(this.transform.position + new Vector3(0, 1.5f, 0), this.transform.TransformDirection(Vector3.forward), out hit, Attack_range * 5, layerMask))
                {
                    StartCoroutine(swing());
                    ani.Play("Great Sword Casting");
                    return;
                }
            }
        }
        else if (type == 4)
        {
        }
        else if (type == 5)
        {
            if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Breathing Idle"))
            {
                if (Physics.Raycast(shootPos.position + new Vector3(0, 1.5f, 0), shootPos.TransformDirection(Vector3.forward) * 45f, out hit, 55f, layerMask))
                {
                    target = hit.transform;
                    StartCoroutine(magic());
                    ani.Play("Standing 2H Magic Area Attack 02");
                    return;
                }
            }
        }

    }

    IEnumerator shoot()
    {
        yield return new WaitForSeconds(0.85f);
        GameObject shoot = Instantiate(weapon, shootP);
        shoot.transform.position = shootPos.position;
        shoot.transform.rotation = shootPos.rotation;
        shoot.GetComponent<JjangDol>().Damage = Damage;
        shoot.GetComponent<JjangDol>().Type = type;
        yield return 0;
    }

    IEnumerator swing()
    {
        yield return new WaitForSeconds(2.4f);
        if (hit.collider != null)
        {
            Instantiate(ParticleArr.Particles[11], hit.collider.gameObject.transform.position, ParticleArr.Particles[0].transform.transform.rotation, ParticleArr.Particles[0].transform);
            hit.collider.gameObject.transform.GetComponent<Enemy>().getDamage(Damage);
            
        }
        yield return 0;
    }

    IEnumerator magic()
    {
        yield return new WaitForSeconds(2f);
        GameObject shoot = Instantiate(weapon, shootP);
        shoot.transform.GetComponent<Soul_Attack>().init(target);
        shoot.transform.GetComponent<Soul_Attack>().Damage = Damage;

        yield return 0;
    }

    public bool getDamage(float damage)
    {
        Current_health -= damage;
        redColorTimer = 0.2f;
        StartCoroutine(damageColor());
        if (Current_health <= 0)
            Death();
        return true;
    }

    void Death()
    {
        ani.speed = 2;
        ani.Play("Falling Back Death");
        Destroy(this.gameObject, 1f);
        TileStatus.tileStatus[(int)thisPosX, (int)thisPosY, 1] = null;
    }

    IEnumerator damageColor()
    {
        thisRenderer.material.color = hitColor;
        yield return new WaitForSeconds(0.2f);
        while(thisRenderer.material.color != new Color(1f, 1f, 1f))
        {
            thisRenderer.material.color = Color.Lerp(thisRenderer.material.color, new Color(1f, 1f, 1f), 0.1f);
        }

        yield return 0;
    }
}
