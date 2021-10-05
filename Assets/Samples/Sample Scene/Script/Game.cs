using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class Game : MonoBehaviour
    {

        [SerializeField] GameObject parent;

        List<Transform> transforms = new List<Transform>();
        List<Vector3> vector3s = new List<Vector3>();
        List<Quaternion> quaternions = new List<Quaternion>();


        [SerializeField]
        private int high;
        [SerializeField]
        private int width;
        [SerializeField]
        private float interval;
        [SerializeField]
        private Transform ReferencePoint;
        //à íuÇì¸ÇÍÇÈïœêî
        Vector3 pos;


        private enum GameState
        {
            None,
            Memory,
            Alignment,
            Main,
        }

        private GameState State = GameState.None;

        public int GamesState
        {
            get { return (int)State; }
        }

        // Start is called before the first frame update
        void Start()
        {
            foreach (Transform child in parent.transform)
            {
                transforms.Add(child);
                vector3s.Add(child.position);
                quaternions.Add(child.rotation);
            }
            StartRanmdom(transforms.ToArray());
            State = GameState.Main;
        }

        // Update is called once per frame
        void Update()
        {
            switch (State)
            {
                case GameState.Main:
                    MainPuzzling();
                    break;
            }
        }

        void MainPuzzling()
        {

        }

        private void StartRanmdom(Transform[] piece)
        {
            pos = ReferencePoint.position;
            int count = 0;
            for (int vi = 0; vi < width; vi++)
            {
                for (int hi = 0; hi < high; hi++)
                {
                    if (count > piece.Length - 1)
                    {
                        break;
                    }
                    piece[count].position = new Vector3(pos.x + vi * interval, pos.y + hi * interval, pos.z);
                    count++;
                }
            }
        }

    }
}