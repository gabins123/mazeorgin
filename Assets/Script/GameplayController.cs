﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

        public enum MazeGenerationAlgorithm
        {
            PureRecursive,
            RecursiveTree,
            RandomTree,
            OldestTree,
            RecursiveDivision,
        }

    public GameObject m_OutDoor;
    public GameObject m_LevelController;
    public MazeGenerationAlgorithm esc;
    public bool FullRandom = false;
    public int RandomSeed = 12345;
    public GameObject Floor = null;
    public GameObject Wall = null;
    //public GameObject Pillar = null;
    public int m_BasicRows = 5;
    public int m_BasicColumns = 5;
    public float CellWidth = 4;
    public float CellHeight = 4;
    public bool AddGaps = false;
    public GameObject GoalPrefab = null;
    public GameObject Parent;
    private BasicMazeGenerator mMazeGenerator = null;
    public int m_child=0;
    public bool isWon;
    public bool isSpawned;
    bool isBakanaSpawned;
    private int m_SoGoc=0;
    public GameObject m_Player;
    public GameObject m_StartPosition;
    public GameObject LevelController;
    //private int m_NumOfBanaka;
    //public List<GameObject> listBakana;
    public bool isSaved;

    // Use this for initialization
    void Start () {
        
        SpawnMaze(m_BasicColumns+LevelController.GetComponent<LevelController>().m_Level, m_BasicRows+ LevelController.GetComponent<LevelController>().m_Level);
        isSpawned = true;

    }
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Z))
        //{
        //    deleteAllChild();
        //}


        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    SpawnMaze(m_BasicColumns, m_BasicRows);
        //}
    }
    public void deleteAllChild()
    {
        if (Parent == null) return;
        foreach (Transform Child in Parent.transform)
        {
            Destroy(Child.gameObject);
        }
    }
    // Update is called once per frame
    public void SpawnMaze(int Columns, int Rows)
    {
        m_SoGoc = 0;
        isSaved = false;
        //m_NumOfBanaka = LevelController.GetComponent<LevelController>().m_Level;
        isBakanaSpawned = false;
        if (!FullRandom)
        {
            Random.seed = RandomSeed;
        }
        switch (esc)
        {
            case MazeGenerationAlgorithm.PureRecursive:
                mMazeGenerator = new RecursiveMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveTree:
                mMazeGenerator = new RecursiveTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RandomTree:
                mMazeGenerator = new RandomTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.OldestTree:
                mMazeGenerator = new OldestTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveDivision:
                mMazeGenerator = new DivisionMazeGenerator(Rows, Columns);
                break;
        }
        mMazeGenerator.GenerateMaze();
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (CellWidth + (AddGaps ? .2f : 0));
                float z = row * (CellHeight + (AddGaps ? .2f : 0));
                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp;
                tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
                tmp.transform.parent = Parent.transform;
                if (cell.WallRight)
                {
                    tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;// right
                    tmp.transform.parent = Parent.transform;
                }
                if (cell.WallFront)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z + CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;// front
                    tmp.transform.parent = Parent.transform;
                }
                if (cell.WallLeft)
                {
                    tmp = Instantiate(Wall, new Vector3(x - CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;// left
                    tmp.transform.parent = Parent.transform;
                }
                if (cell.WallBack)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z - CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;// back
                    tmp.transform.parent = Parent.transform;
                }
                if (cell.IsGoal && GoalPrefab != null && !isBakanaSpawned)
                {
                    m_SoGoc++;
                    if(m_SoGoc > caseSummon())
                    {
                        summonBakana(GoalPrefab, x, z);
                    }
                }
            }
        }
        //if (Pillar != null)
        //{
        //    for (int row = 0; row < Rows + 1; row++)
        //    {
        //        for (int column = 0; column < Columns + 1; column++)
        //        {
        //            float x = column * (CellWidth + (AddGaps ? .2f : 0));
        //            float z = row * (CellHeight + (AddGaps ? .2f : 0));
        //            GameObject tmp = Instantiate(Pillar, new Vector3(x - CellWidth / 2, 0, z - CellHeight / 2), Quaternion.identity) as GameObject;
        //            tmp.transform.parent = Parent.transform;
        //        }
        //    }
        //}
        if(!isBakanaSpawned)
        {
            Debug.Log("Re-Spawn!");
            deleteAllChild();
            SpawnMaze(Columns,Rows);
        }
        spawnOutDoor(m_BasicColumns, m_BasicRows, GoalPrefab);
    }
    public void hamReset()
    {
        if(isSaved)
        {
            hamWin();
            SpawnMaze(m_BasicColumns, m_BasicRows);
            m_Player.transform.position = m_StartPosition.transform.position;
        }
        else
        {
            Debug.Log("U haven't saved the Bakana yet!");
        }
    }
    void hamWin()
    {
        Debug.Log("Win!");
        isWon = true;
        if (Parent == null) return;
        foreach (Transform Child in Parent.transform)
        {
            Destroy(Child.gameObject);
        }
    }
    void summonBakana(GameObject tmp, float x, float z)
    {
        Debug.LogError("Spawned bakana!!");
        isBakanaSpawned = true;
        tmp = Instantiate(GoalPrefab, new Vector3(x, 1, z), Quaternion.Euler(0, 0, 0)) as GameObject;
        tmp.transform.parent = Parent.transform;
    }
    int caseSummon()
    {
        if(LevelController.GetComponent<LevelController>().m_Level <= 2)
        {
            return 1;
        }
        else if(LevelController.GetComponent<LevelController>().m_Level <= 4)
        {
            return 2;
        }
        else 
        {
            return LevelController.GetComponent<LevelController>().m_Level - 2;
        }
    }
    void spawnOutDoor(int columns, int rows,GameObject goal)
    {
        if(isBakanaSpawned)
        {
            int randX;
            int randZ;
            GameObject outdoor;
            do
            {
                randX = (Random.RandomRange(columns / 2, columns) - 1) * 4;
                randZ = (Random.RandomRange(rows / 2, rows) - 1) * 4;
            }
            while (randX == GoalPrefab.transform.position.x && randZ == GoalPrefab.transform.position.z);
            outdoor = Instantiate(m_OutDoor);
            outdoor.transform.parent = Parent.transform;
            outdoor.transform.position = new Vector3(randX, 0, randZ);
        }
    }
}
