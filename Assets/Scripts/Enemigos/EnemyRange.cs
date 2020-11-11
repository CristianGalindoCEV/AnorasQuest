using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public SpacePoint[] puntos;
    int currentPoint = 0;

    [SerializeField] float speed;

    [SerializeField] float rangeDistanceMin;
    [SerializeField] float rangeDistanceMax;

    [SerializeField] float rangeChase;

    float rangeDistance;
    [SerializeField] Transform player;
    [SerializeField] GameObject ProjectilePrefab;
    [SerializeField] Transform enemyRange;

    [SerializeField] float shootCooldown;
    private float shootCooldownTime = 0;

 
    

    private void Awake()
    {
        rangeDistance = rangeDistanceMin;
       
    }
    
   

    void Update()
    {
        
        //Miramos si hemos llegado al punto actual
        if(Vector3.Distance(transform.position, puntos[currentPoint].transform.position)< 0.2f){
            currentPoint++;
            currentPoint %= puntos.Length;
        }

        //Detecta Player
        
        if (Mathf.Abs(Vector3.Distance(player.position, transform.position)) < rangeDistance)
        {
            
            transform.LookAt(player);

            shootCooldownTime += Time.deltaTime;

            rangeDistance = rangeDistanceMax;

            //decide si dispara o se mueve hacia al jugador
            if(Mathf.Abs(Vector3.Distance(player.position, transform.position)) < rangeChase){

                //si el tiempo es mayor del establecido dispara prefab
                if (shootCooldownTime >= shootCooldown)
                {
                    Instantiate(ProjectilePrefab, enemyRange.transform.position, enemyRange.transform.rotation);
                    shootCooldownTime =0;
                }
            }

            else 
            {
                 transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * speed);
            }
            

        }
        
        //Patrulla siguiente punto
        else
        {
            rangeDistance = rangeDistanceMin;
            transform.position = Vector3.MoveTowards(transform.position, puntos[currentPoint].transform.position, Time.deltaTime * speed);
            
        }
        


    }

  

}

