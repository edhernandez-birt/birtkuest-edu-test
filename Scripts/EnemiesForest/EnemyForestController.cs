using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForestController : MonoBehaviour
{
    private Animator myAnim;
    private Transform target;
    public Transform homePos;
    [SerializeField] private float speed;
    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;




    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
      //  target = FindObjectOfType<PlayerControllerGirl>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        target = getClosestPlayer();

        if (target != null)
        {
            Vector3 direction = (new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, 0)).normalized;
          //  myAnimator.SetFloat("aimX", direction.x);
          //  myAnimator.SetFloat("aimY", direction.y);

            if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
            {
                FollowPlayer();
            }
            else if (Vector3.Distance(target.position, transform.position) >= maxRange)
            {
                GoHome();
            }

        }
        //if (target != null)
        //{
        //    if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
        //    {
        //        FollowPlayer();
        //    }
        //    else if (Vector3.Distance(target.position, transform.position) >= maxRange)
        //    {
        //        GoHome();
        //    }
        //}

    }

    public void FollowPlayer()
    {
        myAnim.SetBool("isMoving", true);
        myAnim.SetFloat("moveX", (target.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private Transform getClosestPlayer()
    {
        PlayerControllerGirl[] targets = FindObjectsOfType<PlayerControllerGirl>();
        if (targets.Length == 0) { return null; }
        if (targets.Length == 1) { return targets[0].transform; }
        int idx = 0;
        double distance = double.MaxValue;
        for (int i = 0; i < targets.Length; i++)
        {
            double di = Vector3.Distance(transform.position, targets[i].transform.position);
            if (di < distance)
            {
                distance = di;
                idx = i;
            }
        }
        return targets[idx].transform;
    }

    IEnumerator FadeTo(SpriteRenderer spriteRenderer, float aValue, float aTime)
    {
        if (TryGetComponent<Transform>(out Transform transform))
        {
            float alpha = spriteRenderer.color.a;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
                spriteRenderer.material.color = newColor;
                yield return null;
            }
        }
    }


    public void GoHome()
    {
        myAnim.SetFloat("moveX", (homePos.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (homePos.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, homePos.position) == 0)
        {
            myAnim.SetBool("isMoving", false);
        }
    }
}
