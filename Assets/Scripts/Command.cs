using UnityEngine;

namespace CommandPattern
{
    public abstract class Command
    {
        public readonly float moveDistance = .2f;
        public abstract void Execute(Transform playerTrans);
        public abstract void Move(Transform playerTrans);
    }

    public class MoveForward : Command
    {
        public override void Execute(Transform playerTrans)
        {
            Move(playerTrans);
        }

        public override void Move(Transform playerTrans)
        {
            playerTrans.position = new Vector3(playerTrans.position.x, playerTrans.position.y + moveDistance, 0);
        }
    }

    public class MoveReverse : Command
    {
        public override void Execute(Transform playerTrans)
        {
            Move(playerTrans);
        }

        public override void Move(Transform playerTrans)
        {
            playerTrans.position = new Vector3(playerTrans.position.x, playerTrans.position.y - moveDistance, 0);
        }
    }

    public class MoveLeft : Command
    {
        public override void Execute(Transform playerTrans)
        {
            Move(playerTrans);
        }

        public override void Move(Transform playerTrans)
        {
            playerTrans.position = new Vector3(playerTrans.position.x - moveDistance, playerTrans.position.y, 0);
        }
    }

    public class MoveRight : Command
    {
        public override void Execute(Transform playerTrans)
        {
            Move(playerTrans);
        }

        public override void Move(Transform playerTrans)
        {
            playerTrans.position = new Vector3(playerTrans.position.x + moveDistance, playerTrans.position.y, 0);
        }
    }

    public class DoNothing : Command
    {
        public override void Execute(Transform playerTrans) { }
        public override void Move(Transform playerTrans) { }
    }
}