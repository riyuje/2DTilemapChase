using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FakeChaserHorrorEvent : HorrorEventBase
{
    [SerializeField] private Transform target;
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private float sightRange = 10f;
    

    [SerializeField] private bool isPause;
    [SerializeField] private float pauseTime;

    [SerializeField] private float disappearDistance = 1.2f;
    [SerializeField] private float stopTime = 0.5f;

    private bool isDisappearing = false; //trueÇ»ÇÁè¡Ç¶ÇÈ

    protected override void Update()
    {
        if (!target) return;

        if (isPause)
        {
            pauseTime -= Time.deltaTime;
            if (pauseTime <= 0)
                isPause = false;

            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        // á@ êÊÇ…è¡Ç¶ÇÈîªíË
        if (!isDisappearing && distance <= disappearDistance)
        {
            isDisappearing = true;
            StartCoroutine(Disappear());
            return;
        }

        // áA í«Ç¢Ç©ÇØèàóù
        if (distance <= sightRange)
        {
            float currentSpeed = speed;

            if (distance < 1.5f)
                currentSpeed = 0.5f;

            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                currentSpeed * Time.deltaTime
            );
        }
    }

    private IEnumerator Disappear()
    {
        // ìÆÇ´Çé~ÇﬂÇÈ
        speed = 0f;

        // è≠Çµé~Ç‹ÇÈ
        yield return new WaitForSeconds(stopTime);

        // è¡Ç¶ÇÈ
        Destroy(gameObject);
    }

    

    public void SetPause(float time)
    {
        pauseTime = time;
        isPause = true;
    }

    public override void SetUp(Transform target, Tilemap tilemap)
    {
        this.target = target;
        this.tilemap = tilemap;
    }
}