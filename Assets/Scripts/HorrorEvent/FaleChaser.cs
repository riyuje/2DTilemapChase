using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FakeChaser : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private float sightRange = 10f;
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private bool isPause;
    [SerializeField] private float pauseTime;

    [SerializeField] private float disappearDistance = 1.2f;
    [SerializeField] private float stopTime = 0.5f;

    private bool isDisappearing = false; //trueÇ»ÇÁè¡Ç¶ÇÈ

    void Update()
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
            float currentSpeed = moveSpeed;

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
        moveSpeed = 0f;

        // è≠Çµé~Ç‹ÇÈ
        yield return new WaitForSeconds(stopTime);

        // è¡Ç¶ÇÈ
        Destroy(gameObject);
    }

    public void Setup(Transform target, Tilemap tilemap)
    {
        this.target = target;
        this.tilemap = tilemap;
    }

    public void SetPause(float time)
    {
        pauseTime = time;
        isPause = true;
    }
}