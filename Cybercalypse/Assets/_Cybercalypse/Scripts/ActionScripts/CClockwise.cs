using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CClockwise : MonoBehaviour
{
    private List<Vector3> vertex;
    private List<int> tri;

    private Mesh mesh;

    // Use this for initialization
    void Start()
    {
        vertex = new List<Vector3>();
        tri = new List<int>();

        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        mesh = GetComponent<MeshFilter>().mesh;

        //StartCoroutine(SqrClockwiseAnim());
    }

    IEnumerator ClockwiseAnim()
    {
        Vector3 curVec = Vector3.zero;
        Vector3 dirVec = Vector3.zero;
        Vector3 prevVec = Vector3.zero;
        Vector3 targetVec = new Vector3(0.0f, 0.5f);

        float sLength = 0.26f;
        float lLength = 0.52f;

        //큰 가로길이 0.52
        //작은 가로길이 0.27
        //큰 세로길이 0.51
        //작은 세로길이 0.27

        vertex.Add(curVec);
        curVec = new Vector3(0.0f, 0.5f);
        dirVec = new Vector3(sLength, 0.0f);

        for (prevVec = curVec, targetVec += dirVec; (targetVec - prevVec).magnitude > (curVec - prevVec).magnitude; curVec += (dirVec / 4))
        {
            vertex.Add(curVec);
        }

        dirVec = new Vector3(sLength, -sLength);
        

        for (prevVec = curVec, targetVec += dirVec; (targetVec - prevVec).magnitude > (curVec - prevVec).magnitude; curVec += (dirVec / 8))
        {
            vertex.Add(curVec);
        }

        dirVec = new Vector3(0.0f, -lLength);

        for (prevVec = curVec, targetVec += dirVec; (targetVec - prevVec).magnitude > (curVec - prevVec).magnitude; curVec += (dirVec / 8))
        {
            vertex.Add(curVec);
        }

        dirVec = new Vector3(-sLength, -sLength);

        for (prevVec = curVec, targetVec += dirVec; (targetVec - prevVec).magnitude > (curVec - prevVec).magnitude; curVec += (dirVec / 8))
        {
            vertex.Add(curVec);
        }

        dirVec = new Vector3(-lLength, 0.0f);

        for (prevVec = curVec, targetVec += dirVec; (targetVec - prevVec).magnitude > (curVec - prevVec).magnitude; curVec += (dirVec / 8))
        {
            vertex.Add(curVec);
        }

        dirVec = new Vector3(-sLength, sLength);

        for (prevVec = curVec, targetVec += dirVec; (targetVec - prevVec).magnitude > (curVec - prevVec).magnitude; curVec += (dirVec / 8))
        {
            vertex.Add(curVec);
        }

        dirVec = new Vector3(0.0f, lLength);

        for (prevVec = curVec, targetVec += dirVec; (targetVec - prevVec).magnitude > (curVec - prevVec).magnitude; curVec += (dirVec / 8))
        {
            vertex.Add(curVec);
        }

        dirVec = new Vector3(sLength, sLength);

        for (prevVec = curVec, targetVec += dirVec; (targetVec - prevVec).magnitude > (curVec - prevVec).magnitude; curVec += (dirVec / 8))
        {
            vertex.Add(curVec);
        }

        dirVec = new Vector3(sLength, 0.0f);

        for (prevVec = curVec, targetVec += dirVec; (targetVec - prevVec).magnitude > (curVec - prevVec).magnitude; curVec += (dirVec / 4))
        {
            vertex.Add(curVec);
        }

        mesh.vertices = vertex.ToArray();

        for (int i = 1; i < vertex.Count; i++)
        {
            //!< vertex.Count = 65,
            if (i + 1 >= vertex.Count)
            {
                tri.Add(0);
                tri.Add(i);
                tri.Add(1);
                
            }
            else
            {
                tri.Add(0);
                tri.Add(i);
                tri.Add(i + 1);
            }

            mesh.triangles = tri.ToArray();
            yield return new WaitForSeconds(0.1f);
        }

        //mesh.vertices = new Vector3[] { Vector3.zero, Vector3.up, new Vector3(1, 1, 0) };
        //mesh.uv = new Vector2[] { Vector2.zero, Vector2.up, Vector2.one };
        //Vector2[] uvs = new Vector2[mesh.vertices.Length];
        //for (int i = 0; i < uvs.Length; i++)
        //uvs[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].z);
        //mesh.uv = uvs;
        // mesh.triangles = new int[] { 0, 1, 2 };
    }

    
}
