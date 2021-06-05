using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeEnemy : BaseAlive, IEnemy
{
    public enum State
    {
        Find,
        Idle,
        Seek,
        Attack
    }

    [SerializeField] private float Speed = 1f;

    [Header("Ranged")]
    [SerializeField] private GameObject Projectile;
    [SerializeField] private Transform ShootPosition;

    State CurrentState;
    IAlive Target;
    float TimeToAttack;
    Rigidbody Body;

    protected override void Start()
    {
        base.Start();
        Body = GetComponent<Rigidbody>();
        SetState(State.Find);
    }

    public void SetState(State state)
    {
        switch (state)
        {
            case State.Attack:
                TimeToAttack = Data.AttackTime;
                break;
            case State.Find:
                break;
            case State.Idle:
                break;
            case State.Seek:
                break;
        }
        CurrentState = state;
        Debug.Log(state);
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        switch (CurrentState)
        {
            case State.Attack:
                TimeToAttack -= Time.deltaTime;
                if (TimeToAttack <= 0)
                {
                    // ranged condition
                    if(Projectile != null)
                    {
                        var projectile = Instantiate(Projectile);
                        projectile.transform.position = ShootPosition.position;
                        projectile.transform.rotation = ShootPosition.rotation;
                    }
                    else
                    {
                        ((IAlive)Target).TakeDamage(this, Data.AttackDamage);
                    }
                    TimeToAttack = Data.AttackTime;
                }
                if (!IsInTargetRange())
                {
                    SetState(State.Seek);
                }
                break;
            case State.Find:
                var players = GameObject.FindGameObjectsWithTag(k.TagPlayer);
                if (players.Length > 0)
                {
                    foreach (var item in players)
                    {
                        var player = item.GetComponent<IPlayer>();
                        if (player != null)
                        {
                            Target = (IAlive)player;
                            SetState(State.Seek);
                            break;
                        }
                    }
                }
                break;
            case State.Idle:
                break;
            case State.Seek:
                transform.LookAt(Target.Transform);
                break;
        }

    }

    private void FixedUpdate()
    {
        if (CurrentState == State.Seek)
        {
            Body.position += Vector3.Normalize(Target.Transform.position - transform.position) * Speed * Time.deltaTime;
            if (IsInTargetRange())
            {
                SetState(State.Attack);
            }
        }
    }
    bool IsInTargetRange()
    {
        return Vector3.Distance(Body.position, Target.Transform.position) <= Data.AttackDistance;
    }

}


public static class k
{
    public const string TagEnemy = "Enemy";
    public const string TagPlayer = "Player";
}