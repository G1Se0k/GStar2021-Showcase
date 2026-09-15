using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    Vector3 scale=new Vector3(-10,1,10);
    float timer=2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 1)
        {
            
            if (this.transform.localScale != scale)
                this.transform.localScale = Vector3.Lerp(this.transform.localScale, scale, 3f * Time.deltaTime);
            if (Vector3.Distance(this.transform.localScale, scale) < 0.1)
                this.transform.localScale = scale;
        }
    }
    public void DestroyThis()
    {
        Destroy(this.gameObject,10);
        scale = new Vector3(0, 1, 0);

        timer = 6.5f;
    }
}
