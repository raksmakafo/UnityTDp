using UnityEngine;
using Photon.Pun;   
public class NewMonoBehaviourScript : MonoBehaviour
{

    public float speed;
    PhotonView view;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCooldown = 1f;
    private float lastFireTime;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine)
        {
            Vector2 moveInput = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            );
            Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;

        }
    }
}
