using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(transform.GetChild(i).position, 0.5f);

                Vector3 from;
                Vector3 to;

                from = GetPositionOfIndex(i);
                to = GetNextPositionOfIndex(i);

                Gizmos.DrawLine(from, to);
            }
        }

        public Vector3 GetPositionOfIndex(int i)
        {
            return transform.GetChild(i).position;
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            else
            {
                return i + 1;
            }
        }


        public Vector3 GetNextPositionOfIndex(int i)
        {
            Vector3 to;
            if (i + 1 != transform.childCount)
            {
                to = transform.GetChild(i + 1).position;
            }
            else
            {
                to = transform.GetChild(0).position;
            }

            return to;
        }
    }   
}
