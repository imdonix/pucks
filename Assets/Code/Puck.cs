using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Puck : MonoBehaviour
{
    private enum TurnPhase { Start, Directing, Powering, Shooting, End }

    [SerializeField] private GameObject selectorCircle;
    [SerializeField] private DirectorArrow directorArrow;

    private bool isSelected = false;

    private new Rigidbody2D rigidbody;
    private SpriteRenderer rendered;

    private TurnPhase turnPhase = TurnPhase.Start;
    private float radius;
    private bool queen;

    #region Properties



    #endregion

    private void Awake()
    {
        Select();
        rigidbody = GetComponent<Rigidbody2D>();
        rendered = GetComponent<SpriteRenderer>();
    }

    public Vector2 GetPosition()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }

    public float GetSize()
    {
        return radius;
    }

    private void Update()
    {
        if (!isSelected) return;

        if(Input.GetKeyDown(KeyCode.Space))
            MoveToNextPhase();
    }

    #region Public methods

    public void Select()
    {
        isSelected = true;
        turnPhase = TurnPhase.Start;
        selectorCircle.SetActive(true);
    }

    public void Set(Player player, bool queen, int radius)
    {
        rendered.color = Manager.Instance.GetMap().GetColor(player.Type());
        this.radius = radius;
        this.queen = queen;
    }

    #endregion

    #region Helpers

    private void StartDirecting()
    {
        turnPhase = TurnPhase.Directing;
        selectorCircle.SetActive(false);
        directorArrow.gameObject.SetActive(true);
        directorArrow.Rotate(0.1f);
    }

    private void StartPowering()
    {
        directorArrow.StopRotating();
        turnPhase = TurnPhase.Powering;
        transform.rotation = directorArrow.transform.rotation;
        directorArrow.transform.rotation = transform.rotation;
        directorArrow.StartPowering();
    }

    private void Shoot()
    {
        directorArrow.StopPowering();
        directorArrow.gameObject.SetActive(false);
        turnPhase = TurnPhase.Shooting;
        rigidbody.AddForce(transform.up * directorArrow.CurrentPower, ForceMode2D.Impulse);
    }

    private void MoveToNextPhase()
    {
        switch (turnPhase)
        {
            case TurnPhase.Start:
                StartDirecting();
                break;

            case TurnPhase.Directing:
                StartPowering();
                break;

            case TurnPhase.Powering:
                Shoot();
                break;
        }
    }

    #endregion
}
