using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkForm : MonoBehaviour
{
    //public Vector3 fullScale = new Vector3(0, 0, 0);
    public Vector3 innerBounds = new Vector3(0, 0, 0);
    [Space(20)]
    public Vector3 smoSocketFrame = new Vector3(3, 3, 3);
    public Vector3 smoSocketSolid = new Vector3(1, 1, 1);

    public Vector3 medSocketFrame = new Vector3(6, 6, 6);
    public Vector3 medSocketSolid = new Vector3(4, 4, 4);

    public Vector3 larSocketFrame = new Vector3(8, 8, 8);
    public Vector3 larSocketSolid = new Vector3(6, 6, 6);
    [Space(10)]
    public List<Vector3> smoSocket;
    public List<Vector3> medSocket;
    public List<Vector3> larSocket;

    void OnDrawGizmos()
    {
       
        if (innerBounds != new Vector3(0, 0, 0))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, innerBounds);
            Vector3 fullScale = new Vector3(0, 0, 0);
            fullScale.x = innerBounds.x + 1f;
            fullScale.y = innerBounds.y + 1f;
            fullScale.z = innerBounds.z + 1f;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, fullScale);
        }
        if (smoSocket.Count != 0)
        {
            foreach (Vector3 position in smoSocket)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(position, smoSocketFrame);
                Gizmos.DrawCube(position, smoSocketSolid);
            }
            
        }
        if (medSocket.Count != 0)
        {
            foreach (Vector3 position in medSocket)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(position, medSocketFrame);
                Gizmos.DrawCube(position, medSocketSolid);
            }

        }
        if (larSocket.Count != 0)
        {
            foreach (Vector3 position in larSocket)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(position, larSocketFrame);
                Gizmos.DrawCube(position, larSocketSolid);
            }

        }

    }
}
