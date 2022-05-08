using System;
using Common;
using PlayerScripts;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    protected Player Player;
    protected StateMachine StateMachine;
    public abstract void Construct(Player player);

    protected virtual void Update(){}
    protected virtual void FixedUpdate(){}
    protected virtual void Start(){}
}