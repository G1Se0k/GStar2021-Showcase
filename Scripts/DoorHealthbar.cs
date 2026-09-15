using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorHealthbar : MonoBehaviour
{
    [SerializeField]
    private Door door;

    private float hp;
    private RawImage bar;

    private void Start()
    {
        bar = this.transform.GetComponent<RawImage>();
        hp = 1;
    }

    void Update()
    {
        hp = Mathf.Lerp(hp, door.HP / door.MaxHp, 0.1f);
        bar.rectTransform.offsetMax = new Vector2(-100 + (100 * hp),10);
    }
}
