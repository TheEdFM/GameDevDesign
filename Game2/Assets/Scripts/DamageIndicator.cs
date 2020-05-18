using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageIndicator : MonoBehaviour
{
    public float moveSpeed;
    public float horizontalMoveSpeed;
    public float liveTime = 5f;
    private TextMeshProUGUI textMeshProUGUI;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Random.Range(5f, 20f);
        //moveSpeed = 10;
        horizontalMoveSpeed = Random.Range(-5f, 5f);
        //horizontalMoveSpeed = 0;

        textMeshProUGUI = GetComponent<TextMeshProUGUI>();

        StartCoroutine("DestoryDamageIndicator");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up*Time.deltaTime*moveSpeed + Vector2.right*Time.deltaTime*horizontalMoveSpeed);
        textMeshProUGUI.color = new Color(textMeshProUGUI.color.r, textMeshProUGUI.color.g, textMeshProUGUI.color.b, textMeshProUGUI.color.a-Time.deltaTime*1*1/liveTime);
    }

    IEnumerator DestoryDamageIndicator()
    {
        yield return new WaitForSeconds(liveTime);
        Destroy(this.gameObject);
    }
}
