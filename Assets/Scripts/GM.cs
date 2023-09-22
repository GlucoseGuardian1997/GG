using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GM : MonoBehaviour
{
    [SerializeField]
    DataHandler dataHandler;
    [SerializeField]
    Transform parentObject = null;
    [SerializeField]
    GridBox gridBoxPrefab = null;
    [SerializeField]
    float offsetDistance = 2;
    [SerializeField]
    SquareView squareViewPrefab = null;


    private int gridX = 5;
    private int gridY = 5;
    private List<GridBox> gridList;
    private List<SquareView> squareViewList ;
    private List<SquareData> SquareData;


    private void Start()
    {
        GetData();
        GridSpawnner();
        ObjectSpawnner();
        ObjectDataSet();
        ObjectInit();
    }


    private void GetData()
    {
        SquareData = dataHandler.SquareDataSet.SquareData;
        gridX = dataHandler.SquareDataSet.BaseGirdSizeX;
        gridY = dataHandler.SquareDataSet.BaseGirdSizeY;
    }


    private void GridSpawnner()
    {
        for (int i = 0; i < gridX; i++)
        {
            for (int j = 0; j < gridY; j++)
            {
                Vector3 spwnPos = new(i * offsetDistance, j * offsetDistance, 0);
                var obj = Instantiate(gridBoxPrefab, spwnPos, Quaternion.identity, parentObject);
                obj.transform.name = $"({i},{j})";
                obj.PosX = i; 
                obj.PosY = j;
                gridList.Add(obj);
            }
        }
    }


    private void ObjectSpawnner()
    {
        foreach (var grid in gridList)
        {
            var obj = Instantiate(squareViewPrefab);
           squareViewList.Add(obj);
        }
    }


    private void ObjectDataSet()
    {
        for (int i = 0; i < squareViewList.Count; i++)
        {
            squareViewList[i].PosX = SquareData[i].PosX;
            squareViewList[i].PosY = SquareData[i].PosY;
            squareViewList[i].Scale = new Vector3(SquareData[i].scaleX, SquareData[i].scaleY, 0);
            squareViewList[i].Rotation = new Vector3(0,0, SquareData[i].rotZ);
            squareViewList[i].Color = SquareData[i].color;
            squareViewList[i].Objname = SquareData[i].name;
        }
    }


    private void ObjectInit()
    {
        foreach (var view in squareViewList)
        {
            foreach (var grid in gridList)
            {
                if (grid.PosX == view.PosX && grid.PosY == view.PosY)
                {
                    view.transform.SetParent(grid.transform, false);
                }

            }
            view.Init();
        }  
    }


}



