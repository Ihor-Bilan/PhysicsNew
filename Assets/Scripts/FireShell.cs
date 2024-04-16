using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FireShell : MonoBehaviour {

    public GameObject bullet;
    public GameObject turret;
    public GameObject enemy;
    public Transform turretBase;
    float speed = 15;
    float rotSpeed = 5;
    float moveSpeed = 1;

    void CreateBullet() 
    {
        GameObject shell = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        shell.GetComponent<Rigidbody>().velocity = speed * turretBase.forward;
    }

    float? RotateTurret()
    {
        float? angle = CalculateAngle(false);
        if (angle != null)
        {
            turretBase.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f);  
        }
        return angle;
    }
    float? CalculateAngle(bool low)
    {
        Vector3 targetDir = enemy.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude - 1;
        float gravity = 9.8f;
        float sSgr = speed * speed;
        float underTheSgrRoot = (sSgr * sSgr) - gravity * (gravity * x * x + 2 * y * sSgr);

        if (underTheSgrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSgrRoot);
            float highAngle = sSgr + root;
            float lowAngle = sSgr - root;

            if(low)
                return(Mathf.Atan2(lowAngle,gravity * x) * Mathf.Rad2Deg);
            else
                return(Mathf.Atan2(highAngle,gravity * x) * Mathf.Rad2Deg);
        }
        else 
            return null;
    }

    void Update() 
    {
        Vector3 direction = (enemy.transform.position - this.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
        float? angle = RotateTurret();
        if (angle != null)
        {
            CreateBullet();
        }
        else 
        {
            this.transform.Translate(0, 0, Time.deltaTime * moveSpeed);
        }
        
    }
}