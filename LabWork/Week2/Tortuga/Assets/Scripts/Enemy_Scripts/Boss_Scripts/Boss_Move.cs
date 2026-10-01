using UnityEngine;

public class Boss_Move : MonoBehaviour
{
    public float MoveSpeed = 1.0f;
    public GameObject pointA;
    public GameObject pointB;
    public GameObject pointC;
    private Rigidbody2D rb;
    private Transform currentPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pointA = GameObject.Find("Boss_Point_A");
        pointB = GameObject.Find("Boss_Point_B");
        pointC = GameObject.Find("Boss_Point_C");
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointA.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == pointA.transform)
        {
            rb.linearVelocity = new Vector2 (0,-MoveSpeed);
        }
        else if(currentPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2 (MoveSpeed,0);
        }
        else if(currentPoint == pointC.transform)
        {
            rb.linearVelocity = new Vector2 (-MoveSpeed,0);
        } 
        else
        {
            rb.linearVelocity = new Vector2(0,0);
        } 

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointC.transform;
        }
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointC.transform)
        {
            currentPoint = pointB.transform;
        }

    }
}
