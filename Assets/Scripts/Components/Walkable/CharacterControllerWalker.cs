using UnityEngine;

public class CharacterControllerWalker : Walker
{
    public float speed = 1.0f;
    public float rotateSpeed = 1.0f;
    public float gravity = 20.0f;

    [SerializeField]
    private Movement movement = new Movement();

    private void Start()
    {
        if (this.movement.IsMoving())
        {
            base.Move(this.movement.moveFront, this.movement.moveRight, this.movement.rotateVolume);
        }
    }

    private void Update()
    {
        CharacterController controller = this.GetComponent<CharacterController>();

        Vector3 calcuratedDirection = Vector3.zero;
        
        if (controller.isGrounded)
        {
            this.transform.Rotate(new Vector3(0, this.rotateSpeed * this.movement.rotateVolume, 0));
            calcuratedDirection = this.transform.TransformDirection(new Vector3(this.movement.moveRight, 0.0f, this.movement.moveFront));
            calcuratedDirection *= this.speed;
        }
        calcuratedDirection.y -= this.gravity * Time.deltaTime;

        if (controller.enabled)
        {
            controller.Move(calcuratedDirection * Time.deltaTime);
        }
    }

    public override Movement GetMovement()
    {
        return new Movement();
    }

    public override void Stop()
    {
        base.Stop();

        this.movement.Stop();
    }

    public override void Move(float front, float right, float rot)
    {
        base.Move(front, right, rot);

        this.movement.moveFront = front;
        this.movement.moveRight = right;
        this.movement.rotateVolume = rot;
    }

    public override void Run(float front, float right, float rot)
    {
        base.Run(front, right, rot);

        this.movement.moveFront = front;
        this.movement.moveRight = right;
        this.movement.rotateVolume = rot;
    }

    public override void MoveFront(float front)
    {
        base.MoveFront(front);

        this.movement.moveFront = front;
    }
    public override void MoveRight(float right)
    {
        base.MoveRight(right);

        this.movement.moveRight = right;
    }
    public override void MoveRotate(float rotate)
    {
        base.MoveRotate(rotate);

        this.movement.rotateVolume = rotate;
    }

    public override void Collapse()
    {
        base.Collapse();

        this.Stop();
    }
}
