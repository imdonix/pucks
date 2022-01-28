using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    private const int SIZE = 100;
    private int[] map;

    private Color undefined = Color.green;

    private Color top;
    private Color bottom;

    private HashSet<Vector2Int> hash;

    public Map(Color top, Color bottom)
    {
        this.map = new int[SIZE * SIZE];

        this.top = top;
        this.bottom = bottom;

        Init();
        //Randomize();
    }

    private void Init()
    {
        for (int y = 0; y < SIZE; y++)
        {
            for (int x = 0; x < SIZE; x++)
            {
                map[y * SIZE + x] = y > SIZE / 2 ? 0 : 1;
            }
        }
    }

    private void Randomize()
    {
        for (int y = 0; y < SIZE; y++)
        {
            for (int x = 0; x < SIZE; x++)
            {
                map[y * SIZE + x] = Random.Range(0,2);
            }
        }
    }

    private Color GetColor(int s)
    {
        switch (s)
        {
            case 0: return top;
            case 1: return bottom;
        }

        return undefined;
    }

    private int Direction(int type)
    {
        return type == 0 ? 1 : -1;
    }

    private int Get(int x, int y)
    {
        return map[y * SIZE + x];
    }

    private int Get(Vector2Int pos)
    {
        return Get(pos.x, pos.y);
    }

    private bool OutOfIndex(Vector2Int pos)
    {
        bool zero = pos.x < 0 || pos.y < 0;
        bool top = pos.x >= SIZE || pos.y >= SIZE;
        return zero || top;
    }

    public void Pain(Vector2 pos, float size, int type)
    {
        Vector2 real = (pos + Vector2.one) / 2;
        Vector2Int grid = new Vector2Int(Mathf.CeilToInt(real.x * SIZE), Mathf.CeilToInt(real.y * SIZE));
        int rounded = Mathf.CeilToInt(size);

        for (int x = -rounded; x < rounded; x++)
        {
            for (int y = -rounded; y < rounded; y++)
            {
                Vector2Int check = new Vector2Int(x, y);
                if (Vector2Int.Distance(check, Vector2Int.zero) < size)
                {
                    Vector2Int curr = grid + check;
                    if (!OutOfIndex(curr))
                    {
                        map[curr.y * SIZE + curr.x] = type;
                    }
                }
            }
        }
    }

    public Sprite GetSprite()
    {
        Texture2D texture = new Texture2D(SIZE, SIZE);
        for (int y = 0; y < SIZE; y++)
        {
            for (int x = 0; x < SIZE; x++)
            {
                texture.SetPixel(x, y, GetColor(map[y * SIZE + x]));
            }
        }

        
        if (!ReferenceEquals(null, hash))
        {
            foreach (var item in hash)
            {
                texture.SetPixel(item.x, item.y, Color.red);
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();
        

        return Sprite.Create(texture, new Rect(0,0,SIZE,SIZE), Vector2.one / 2);
    }

    public bool CheckConnected(Vector2 pos, int type)
    {
        Vector2 real = (pos + Vector2.one) / 2;
        Vector2Int grid = new Vector2Int(Mathf.CeilToInt(real.x * SIZE), Mathf.CeilToInt(real.y * SIZE));

        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        hash = visited;
        Queue<Vector2Int> queue = new Queue<Vector2Int>();

        queue.Enqueue(grid);
        while (queue.Count > 0)
        {
            Vector2Int curr = queue.Dequeue();
 
            if (Direction(type) > 0 && curr.y >= SIZE)
            {
                return true;
            }
            else if (Direction(type) < 0 && curr.y < 0)
            {
                return true;
            }

            if (Get(curr) == type && !visited.Contains(curr))
            {
                queue.Enqueue(curr + Vector2Int.up);
                queue.Enqueue(curr + Vector2Int.down);
                queue.Enqueue(curr + Vector2Int.left);
                queue.Enqueue(curr + Vector2Int.right);
            }

            visited.Add(curr);
        }

        return false;

    }
}
