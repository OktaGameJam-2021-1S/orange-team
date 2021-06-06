using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeEnemy : BaseUnit, IEnemy
{
    public enum State
    {
        Find,
        Idle,
        Seek,
        Attack
    }

    [Header("Ranged")]
    [SerializeField] private GameObject Projectile;
    [SerializeField] private Transform ShootPosition;

    [Header("Visual")]
    [SerializeField] private Animator Animation;
    [SerializeField] private float TimeToTakeDamage = .5f;

    private State CurrentState;
    private IEntity Target;
    private float TimeToAttackAnimation;
    private float TimeToTakeDamageCounter = .5f;
    private Rigidbody Body;

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
                TimeToAttackAnimation = 0.3f;// Data.AttackTime;
                TimeToTakeDamageCounter = 0.3f+TimeToTakeDamage;// Data.AttackTime + TimeToTakeDamage;
                break;
            case State.Find:
                if (Animation != null)
                {
                    Animation.SetTrigger("Idle");
                }
                break;
            case State.Idle:
                if (Animation != null)
                {
                    Animation.SetTrigger("Idle");
                }
                break;
            case State.Seek:
                if (Animation != null)
                {
                    Animation.SetTrigger("Walk");
                }
                break;
        }
        CurrentState = state;
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        switch (CurrentState)
        {
            case State.Attack:
                TimeToAttackAnimation -= Time.deltaTime;
                TimeToTakeDamageCounter -= Time.deltaTime;
                if (TimeToAttackAnimation <= 0)
                {
                    if (Animation != null)
                    {
                        Animation.SetTrigger("Attack");
                    }
                    TimeToAttackAnimation = Data.AttackTime;
                }
                if (TimeToTakeDamageCounter <= 0)
                {
                    // ranged condition
                    if (Projectile != null)
                    {
                        var goProjectile = SimpleObjectPooling.Instance.Instantiate(Projectile, ShootPosition.position);
                        var projectile = goProjectile.GetComponent<Projectile>();
                        goProjectile.transform.rotation = ShootPosition.rotation;
                        projectile.Setup(this);
                    }
                    else
                    {
                        ((IEntity)Target).TakeDamage(this, Data.AttackDamage);
                    }
                    TimeToTakeDamageCounter = Data.AttackTime;
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
                            Target = (IEntity)player;
                            SetState(State.Seek);
                            break;
                        }
                    }
                }
                break;
            case State.Idle:
                break;
            case State.Seek:
                //LookAtTarget();
                //transform.LookAt(Target.Transform);
                break;
        }
        LookAtTarget();

    }
    void LookAtTarget()
    {
        if (Target == null)
            return;

        transform.LookAt(Target.Transform);
        //Vector3 LookAtPoint = new Vector3(Target.Transform.position.x, 0, Target.Transform.position.y);
        //transform.LookAt(LookAtPoint);
    }
    private void FixedUpdate()
    {
        if (CurrentState == State.Seek)
        {
            Body.position += Vector3.Normalize(Target.Transform.position - transform.position) * Data.Speed * Time.fixedDeltaTime;
            if (IsInTargetRange())
            {
                if (Animation != null)
                {
                    Animation.SetTrigger("Idle");
                }
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