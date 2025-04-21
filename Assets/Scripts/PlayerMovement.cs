using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMoving = false;
    private Vector2 input;
    private Vector3 targetPos;

    void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Only move one direction at a time
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                var targetGridPos = transform.position + new Vector3(input.x, input.y, 0);
                StartCoroutine(MoveTo(targetGridPos));
            }
        }
    }

    System.Collections.IEnumerator MoveTo(Vector3 target)
    {
        isMoving = true;
        while ((target - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
        isMoving = false;
    }
}
