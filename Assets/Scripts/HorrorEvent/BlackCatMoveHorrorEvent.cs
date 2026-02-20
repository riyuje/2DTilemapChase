using UnityEngine;

public class BlackCatMoveHorrorEvent : HorrorEventBase
{
    

    protected override void Update()
    {
        //base.Update();
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

