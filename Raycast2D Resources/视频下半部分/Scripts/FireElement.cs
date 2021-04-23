using UnityEngine;

public class FireElement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public Transform target;//target A or target B
    [SerializeField] private Transform targetA, targetB;

    public Transform firePoint;
    [SerializeField] private float maxDist;
    public LayerMask mask;

    [Header("Laser")]
    private LineRenderer lineRenderer;
    [SerializeField] private Gradient redColor, greenColor;
    public GameObject hitEffect;



    private void Start()
    {
        target = targetB;
        lineRenderer = GetComponentInChildren<LineRenderer>();

        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        Move();
        Detect();
    }

    private void Move()
    {
        //在AB两点做【巡逻】
        if(Vector2.Distance(transform.position, targetA.position) <= 0.1f)//已经到达了目标点A这个位置，因为向着B巡逻了
        {
            target = targetB;
        }

        if(Vector2.Distance(transform.position, targetB.position) <= 0.01f)//已经到达了目标点B这个位置，因为向着A巡逻了
        {
            target = targetA;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void Detect()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, -transform.right, maxDist, mask);

        if(hitInfo.collider != null)//Enemy has hitted sth
        {
            Debug.Log(hitInfo.collider.gameObject.name);

            //MARKER 如果检测到的是墙壁Block，应该什么都不会发生
            if(hitInfo.collider.tag == "Block")
            {
                Debug.DrawLine(firePoint.position, hitInfo.point, Color.green);

                //LineRenderer检测到对象的击中点
                lineRenderer.SetPosition(1, hitInfo.point);
                lineRenderer.colorGradient = greenColor;
            }

            //如果检测到的是角色，那它Go Die
            if(hitInfo.collider.tag == "Player")
            {
                Debug.DrawLine(firePoint.position, hitInfo.point, Color.red);

                lineRenderer.SetPosition(1, hitInfo.point);
                lineRenderer.colorGradient = redColor;
            }

            Instantiate(hitEffect, hitInfo.point, Quaternion.identity);//Effect
            lineRenderer.SetPosition(0, firePoint.position);//LineRenderer的初始点
        }
    }


}
