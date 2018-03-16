using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockManager : MonoBehaviour {
    public float movespeed;
    [HideInInspector]
    public bool Movable;
    public Block[,] blocks;

    private int blockcount;
    private bool moveflag;
    void Awake()
    {
        Movable = false;
        blocks =new Block[4,4];
        blockcount = 0;
        moveflag = false;
    }

    public void Spawn()
    {
        Block newblock = Managers.Pool.SpawnBlock();
        while (true)
        {
            int i = Random.Range(0, 4);
            int j = Random.Range(0, 4);
            if (blocks[i, j] == null)
            {
                newblock.trans.position = Managers.Slot.GetSlot(i, j);
                blocks[i, j] = newblock;
                break;
            }
        }
        blockcount++;
        if (CheckGameOver())
        {
            Managers.UI.PushPanel(PanelType.GameOver);
        }
        else
        {
            Movable = true;
        }
    }
    private bool CheckGameOver()
    {
        if (blockcount < 16) return false;
        for(int i = 0; i <= 2; i++)
        {
            for (int j = 0; j <= 2; j++)
            {
                if (blocks[i, j].Value == blocks[i + 1, j].Value ||
                    blocks[i, j].Value == blocks[i, j + 1].Value)
                    return false;
            }
        }
        for (int i = 0; i <= 2; i++)
        {
            if (blocks[i, 3].Value == blocks[i + 1, 3].Value ||
                    blocks[3, i].Value == blocks[3, i + 1].Value)
                return false;
        }
        return true;
    }
    public void CleanBlocks()
    {
        Managers.Pool.CleanBlocks();
        for (int i= 0; i < 4; i++){
            for(int j = 0; j < 4; j++)
            {
                blocks[i, j] = null;
            }
        }
        blockcount = 0;
    }

    private IEnumerator Move(Block block,int i,int j,int displacement)
    {
        Vector3 pos = Managers.Slot.GetSlot(i, j);
        Tween tween = block.trans.DOMove(pos, displacement/movespeed);
        yield return tween.WaitForCompletion();
    }
    private IEnumerator Merge(int i, int j,Block deleteblock, int displacement)
    {
        Vector3 pos = Managers.Slot.GetSlot(i, j);
        Tween tween = blocks[i,j].trans.DOMove(pos, displacement / movespeed);
        yield return tween.WaitForCompletion();
        blocks[i, j].Bigger();
        Managers.Pool.DestroyBlock(deleteblock.gameObject);
    }


    #region Operation

    IEnumerator Down()
    {
        var cor1 = StartCoroutine(DownColumn(0));
        var cor2 = StartCoroutine(DownColumn(1));
        var cor3 = StartCoroutine(DownColumn(2));
        var cor4 = StartCoroutine(DownColumn(3));
        yield return cor1;
        yield return cor2;
        yield return cor3;
        yield return cor4;
        if (moveflag)
        {
            Spawn();
            moveflag = false;
        }
        else
        {
            Movable = true;
        }
    }

    IEnumerator DownColumn(int col)
    {
        Coroutine coroutine=null;
        int k = 3;
        for (int i = 2; i >= 0; i--)
        {
            if (blocks[i, col] != null)
            {
                int j = i + 1;
                while (j <= k)
                {
                    if (blocks[j, col] != null)
                        break;
                    j++;
                }
                if (j <= k)
                {
                    if(blocks[j, col].Value!= blocks[i, col].Value)
                    {
                        if (j != i + 1)//Move
                        {
                            moveflag = true;
                            blocks[j-1, col] = blocks[i, col];
                            blocks[i, col] = null;

                            coroutine = StartCoroutine(Move(blocks[j - 1, col], j - 1, col,j-1-i));
                        }
                    }
                    else
                    {
                        k=j-1;
                        moveflag = true;
                        blockcount--;
                        
                        Block deleteblock = blocks[j, col];
                        blocks[j, col] = blocks[i, col];
                        blocks[i, col] = null;

                        coroutine = StartCoroutine(Merge(j,col,deleteblock,j-i));
                    }
                }
                else
                {
                    moveflag = true;
                    blocks[k, col] = blocks[i, col];
                    blocks[i, col] = null;

                    coroutine = StartCoroutine(Move(blocks[k, col], k, col,k-i));
                }
            }
        }
        if(coroutine!=null)
            yield return coroutine;
    }

    IEnumerator Up()
    {
        var cor1 = StartCoroutine(UpColumn(0));
        var cor2 = StartCoroutine(UpColumn(1));
        var cor3 = StartCoroutine(UpColumn(2));
        var cor4 = StartCoroutine(UpColumn(3));
        yield return cor1;
        yield return cor2;
        yield return cor3;
        yield return cor4;
        if (moveflag)
        {
            Spawn();
            moveflag = false;
        }
        else
        {
            Movable = true;
        }
    }

    IEnumerator UpColumn(int col)
    {
        Coroutine coroutine = null;
        int k = 0;
        for (int i = 1; i <= 3; i++)
        {
            if (blocks[i, col] != null)
            {
                int j = i - 1;
                while (j >= k)
                {
                    if (blocks[j, col] != null)
                        break;
                    j--;
                }
                if (j >= k)
                {
                    if (blocks[j, col].Value != blocks[i, col].Value)
                    {
                        if (j != i - 1)
                        {
                            moveflag = true;
                            blocks[j + 1, col] = blocks[i, col];
                            blocks[i, col] = null;

                            coroutine = StartCoroutine(Move(blocks[j + 1, col], j + 1, col,i-j-1));
                        }
                    }
                    else
                    {
                        k=j+1;
                        moveflag = true;
                        blockcount--;

                        Block deleteblock = blocks[j, col];
                        blocks[j, col] = blocks[i, col];
                        blocks[i, col] = null;

                        coroutine = StartCoroutine(Merge(j, col, deleteblock,i-j));
                    }
                }
                else
                {
                    moveflag = true;
                    blocks[k, col] = blocks[i, col];
                    blocks[i, col] = null;

                    coroutine = StartCoroutine(Move(blocks[k, col], k, col,i-k));
                }
            }
        }
        if (coroutine != null)
            yield return coroutine;
    }

    IEnumerator Right()
    {
        var cor1 = StartCoroutine(RightRow(0));
        var cor2 = StartCoroutine(RightRow(1));
        var cor3 = StartCoroutine(RightRow(2));
        var cor4 = StartCoroutine(RightRow(3));
        yield return cor1;
        yield return cor2;
        yield return cor3;
        yield return cor4;
        if (moveflag)
        {
            Spawn();
            moveflag = false;
        }
        else
        {
            Movable = true;
        }
    }

    IEnumerator RightRow(int row)
    {
        Coroutine coroutine = null;
        int k = 3;
        for (int i = 2; i >= 0; i--)
        {
            if (blocks[row, i] != null)
            {
                int j = i + 1;
                while (j <= k)
                {
                    if (blocks[row, j] != null)
                        break;
                    j++;
                }
                if (j <= k)
                {
                    if (blocks[row, j].Value != blocks[row, i].Value)
                    {
                        if (j != i + 1)
                        {
                            moveflag = true;
                            blocks[row, j - 1] = blocks[row, i];
                            blocks[row, i] = null;

                            coroutine = StartCoroutine(Move(blocks[row, j - 1], row, j - 1,j-1-i));
                        }
                    }
                    else
                    {
                        k=j-1;
                        moveflag = true;
                        blockcount--;

                        Block deleteblock = blocks[row, j];
                        blocks[row, j] = blocks[row, i];
                        blocks[row, i] = null;

                        coroutine = StartCoroutine(Merge(row, j, deleteblock,j-i));
                    }
                }
                else
                {
                    moveflag = true;
                    blocks[row, k] = blocks[row, i];
                    blocks[row, i] = null;

                    coroutine = StartCoroutine(Move(blocks[row, k], row, k,k-i));
                }
            }
        }
        if (coroutine != null)
            yield return coroutine;
    }

    IEnumerator Left()
    {
        var cor1 = StartCoroutine(LeftRow(0));
        var cor2 = StartCoroutine(LeftRow(1));
        var cor3 = StartCoroutine(LeftRow(2));
        var cor4 = StartCoroutine(LeftRow(3));
        yield return cor1;
        yield return cor2;
        yield return cor3;
        yield return cor4;
        if (moveflag)
        {
            Spawn();
            moveflag = false;
        }
        else
        {
            Movable = true;
        }
    }

    IEnumerator LeftRow(int row)
    {
        Coroutine coroutine = null;
        int k = 0;
        for (int i = 1; i <= 3; i++)
        {
            if (blocks[row, i] != null)
            {
                int j = i - 1;
                while (j >= k)
                {
                    if (blocks[row, j] != null)
                        break;
                    j--;
                }
                if (j >= k)
                {
                    if (blocks[row, j].Value != blocks[row, i].Value)
                    {
                        if (j != i - 1)
                        {
                            moveflag = true;
                            blocks[row, j + 1] = blocks[row, i];
                            blocks[row, i] = null;

                            coroutine = StartCoroutine(Move(blocks[row, j + 1], row, j + 1,i-j-1));
                        }
                    }
                    else
                    {
                        k=j+1;
                        moveflag = true;
                        blockcount--;

                        Block deleteblock = blocks[row, j];
                        blocks[row, j] = blocks[row, i];
                        blocks[row, i] = null;

                        coroutine = StartCoroutine(Merge(row, j, deleteblock,i-j));
                    }
                }
                else
                {
                    moveflag = true;
                    blocks[row, k] = blocks[row, i];
                    blocks[row, i] = null;

                    coroutine = StartCoroutine(Move(blocks[row, k], row, k,i-k));
                }
            }
        }
        if (coroutine != null)
            yield return coroutine;
    }

    #endregion

    // Update is called once per frame
    void Update () {
        if (!Movable) return;
		if(Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Movable = false;
            StartCoroutine(Left());
            return;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Movable = false;
            StartCoroutine(Right());
            return;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Movable = false;
            StartCoroutine(Up());
            return;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Movable = false;
            StartCoroutine(Down());
            return;
        }
    }
}
