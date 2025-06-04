﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class TowerManager : Loader<TowerManager>
{
    public TowerBtn towerBtnPressed { get; set; }

    SpriteRenderer spriteRenderer;
    private List<TowerControl> TowerList = new List<TowerControl>();
    private List<Collider2D> BuildList = new List<Collider2D>();
    private Collider2D buildTile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildTile = GetComponent<Collider2D>();
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("TowerSide"))
            {
                buildTile = hit.collider;
                buildTile.tag = "TowerSideFull";
                RegisterBuildSite(buildTile);
                PlaceTower(hit);
            }
        }

        if (spriteRenderer.enabled)
        {
            FollowMouse();

        }


    }


    public void RegisterBuildSite(Collider2D buildTag)
    {
        BuildList.Add(buildTag);
    }


    public void RegisterTower(TowerControl tower)
    {
        TowerList.Add(tower);
    }


    public void RenameTagBuildSite()
    {
        foreach (Collider2D buildTag in BuildList)
        {
            buildTag.tag = "TowerSide";
        }
        BuildList.Clear();
    }

    public void DestroyAllTower()
    {
        foreach (TowerControl tower in TowerList)
        {
            Destroy(tower.gameObject);
        }

        TowerList.Clear();

    }


    public void PlaceTower(RaycastHit2D hit)
    {
        if (!Manager.Instance.IsGameStarted)
        {
            Debug.Log("⛔ Игра ещё не началась! Ставить башни нельзя.");
            return;
        }

        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            int price = towerBtnPressed.TowerPrice;

            if (Manager.Instance.TotalMoney >= price)
            {
                TowerControl newTower = Instantiate(towerBtnPressed.TowerObject);
                newTower.transform.position = hit.transform.position;
                Manager.Instance.subtractMoney(price); // списываем только если хватило
                RegisterTower(newTower);
            }
            else
            {
                Debug.LogWarning("💸 Недостаточно средств для постройки башни!");
            }

            DisableDrag(); // независимо от успеха, выключаем тень
        }
    }


    public void BuyTower(int price)
    {
        Manager.Instance.subtractMoney(price);
    }

    public void SelectedTower(TowerBtn towerSelected)
    {
        if (towerSelected.TowerPrice <= Manager.Instance.TotalMoney)
        {
            towerBtnPressed = towerSelected;
            EnableDrag(towerBtnPressed.DragSprite);

        }


    }

    public void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }



    public void EnableDrag(Sprite sprite)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;

    }

    public void DisableDrag()
    {
        spriteRenderer.enabled = false;


    }
}
