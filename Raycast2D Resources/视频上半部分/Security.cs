using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Security : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;//装置旋转的速度
    [SerializeField] private float maxDist;//装置能检测的最大距离，即Raycast检测的最大范围
    public LayerMask mask;

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;//保证Raycast在开始检测时能忽略自己本身的Collider组件
    }

    private void Update()
    {
        //武器旋转
        SelfRotation();

        //装置的检测
        Detect();
    }

    private void SelfRotation()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);//装置绕着自身坐标系Z轴旋转
        //Debug.DrawLine(transform.position, transform.up * 10, Color.red);
    }

    private void Detect()
    {
        //MARKER RAY ONLY USED IN 3D Ray只能使用在3D世界中，直接将Ray的两个参数放在【Physics2D.Raycast】中就可以了
        //Ray ray = new Ray(transform.position, transform.up);//original Position and ray direction
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, maxDist, mask);//ONLY DETECT this layer 【2的八次方】//MARKER 只会检测指定层

        #region 这五个只要了解就好了，我习惯用第一个
        //RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, maxDist, 256); OPTIONAL 1
        //RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, maxDist, mask.value);//OPtIONAL 2
        //OPTIONAL 3
        //RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, maxDist, LayerMask.GetMask("Block"));//检测Block层
        //OPTIONAL 4
        //RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, maxDist, ~LayerMask.GetMask("Block"));//～除Block以外的层都会被检测
        //OPTIONAL 5
        //RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, maxDist, 1 << LayerMask.NameToLayer("Block"));//太骚了，不常用
        #endregion

        if (hitInfo.collider != null)//Raycasy has hit sth !!! 如果Raycast检测到了任何含有Collider的组件的游戏对象的话
        {
            Debug.Log(hitInfo.collider.gameObject.name);//显示名字
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);//SHOW RED LINE hitInfo.point gets ray the point we hitted it 辅助线
        }
        else
        {
            Debug.DrawLine(transform.position, transform.up * maxDist, Color.yellow);//SHOW GREEN LINE 辅助线
        }
    }


}
