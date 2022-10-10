using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    public Animator anim;
    public Camera cam;
    public ParticleSystem particle;
    private bool isUp = false;
    private float time = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("moveUp", 1, 3);
    }


    void moveUp()
    {
        if (!isUp)
        {
            if (Random.Range(0, 100) > 75)
            {
                Debug.Log("Here");
                anim.Play("Armature|Spawn 0", 0);
                isUp = true;
                StartCoroutine(goDown());
            }
        }
    }

    IEnumerator goDown()
    {



        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(time);
        if (isUp)
        {
            anim.Play("Armature|TimeUp 0", 0);
            isUp = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject obj = hit.collider.gameObject;
                Debug.Log(transform.gameObject.tag);
                if (obj == transform.gameObject && isUp)
                {
                    particle.Play();
                    anim.Play("Armature|Hit 0", 0);
                    isUp = false;
                }
            }
        }
    }
}
