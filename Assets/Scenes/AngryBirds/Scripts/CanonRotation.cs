using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRotation : MonoBehaviour
{
    public Vector3 _maxRotation;
    public Vector3 _minRotation;
    private float offset = -51.6f;
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float ProjectileSpeed = 0;
    public float MaxSpeed;
    public float MinSpeed;
    public GameObject PotencyBar;
    private float initialScaleX;

    private void Awake()
    {
        initialScaleX = PotencyBar.transform.localScale.x;
    }
    void Update()
    {
        //PISTA: mireu TOTES les variables i feu-les servir

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//guardem posici� del ratol� a la c�mera
        Vector3 dist = Vector3.Normalize(mousePos-transform.position);//vector entre el click i la bala
        float angle = (Mathf.Atan2(dist.y, dist.x) * 180f / Mathf.PI + offset);
        angle = Mathf.Clamp(angle,_minRotation.z,_maxRotation.z);
        transform.rotation = Quaternion.Euler(0,0,angle);//aplicar rotaci� de l'angle al can�  

        if (Input.GetMouseButton(0))
        {
            ProjectileSpeed += 4*Time.deltaTime;//cada segon s'ha de fer 4 unitats m�s gran
            ProjectileSpeed = Mathf.Clamp(ProjectileSpeed,MinSpeed,MaxSpeed);
        }
        if(Input.GetMouseButtonUp(0))
        {
            var projectile = Instantiate(Bullet,ShootPoint.transform.position,ShootPoint.transform.rotation); //On s'instancia? Deberia ser ShootPoint pero no creo que sea buena idea crear una relacion padre e hijo entre los dos.
            projectile.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * Vector3.Normalize(ShootPoint.transform.position-transform.position);//quina velocitat ha de tenir la bala? 
            ProjectileSpeed = 0f; //reset despr�s del tret
        }
        CalculateBarScale();

    }
    public void CalculateBarScale()
    {
        PotencyBar.transform.localScale = new Vector3(Mathf.Lerp(0, initialScaleX, ProjectileSpeed / MaxSpeed),
            PotencyBar.transform.localScale.y,
            PotencyBar.transform.localScale.z);
    }
}
