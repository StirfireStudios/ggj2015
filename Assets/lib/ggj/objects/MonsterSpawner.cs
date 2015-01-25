using N;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace GGJ {

    /** Try to periodically spawn monsters from a monster spawner if it's visible */
    public class MonsterSpawner : MonoBehaviour {

        /** All monster spawners */
        public static List<MonsterSpawner> All = new List<MonsterSpawner>();

        /** Last spawn */
        public float time_since_spawn;

        /** How many monsters do we allow at most */
        public int max_monsters = 2;

        /** How often do we spawn attempt */
        public float timer = 5f;

        /** Monster factory */
        private GameObject _monster_factory;

        void Start () {
            MonsterSpawner.All.Add(this);
            time_since_spawn = 0;
            _monster_factory = Resources.Load("monsters/Monster", typeof(GameObject)) as GameObject;
        }

        void Update () {
            if (max_monsters > Monster.All.Count) {
                time_since_spawn += Time.deltaTime;
                if (time_since_spawn > timer) {
                    _spawnMonster();
                }
            }
            else {
                time_since_spawn = 0f;
            }
        }

        private void _spawnMonster() {
            var instance = UnityEngine.Object.Instantiate(_monster_factory) as GameObject;
            var pos = this.gameObject.transform.position;
            instance.transform.position = pos;
        }
    }
}
