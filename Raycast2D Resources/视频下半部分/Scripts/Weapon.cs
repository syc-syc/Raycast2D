using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Rotation")]
    private Vector2 direction;
    public Transform firePoint;

    [Header("Detect Enemy Info")]
    public Image boardImage;
    public Text boardText;
    [SerializeField] private float maxDist;
    public LayerMask layerMask;

    [Header("Bullet")]
    [SerializeField] private GameObject bullet;
    public float force = 15f;

    private void Update()
    {
        WeaponRotation();
        Detect();

        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void WeaponRotation()
    {
        //target Pos【即鼠标的当前位置】 - current weapon Pos【最好是枪口的位置，或者transform.position】
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;//Mathf.Rad2Deg: Constant value 57
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    private void Detect()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, direction, maxDist, layerMask);
        if(hitInfo.collider != null)
        {
            if(hitInfo.collider.gameObject.tag == "Block")//如果枪口指向了墙壁，不希望有墙壁的UI显示，我们只显示敌人的信息在UI画布上
            {
                boardImage.gameObject.SetActive(false);
                boardText.text = "";

                Debug.DrawLine(firePoint.position, hitInfo.point, Color.green);
            } 
            else if (hitInfo.collider.gameObject.tag == "Enemy")
            {
                boardImage.gameObject.SetActive(true);
                boardText.text = hitInfo.collider.gameObject.name;

                Debug.DrawLine(firePoint.position, hitInfo.point, Color.red);
            }
            else
            {
                boardImage.gameObject.SetActive(false);
                boardText.text = "";
            }
        }
    }

    private void Fire()
    {
        GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * force, ForceMode2D.Impulse);
        //TODO LATER Shot Effect
    }
}
