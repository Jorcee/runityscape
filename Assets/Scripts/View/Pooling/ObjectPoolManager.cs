﻿using System.Collections.Generic;
using UnityEngine;

namespace Scripts.View.ObjectPool {

    /// <summary>
    /// This class registers objects and retrieves them when needed,
    /// greatly improving the performance of the game as we only call
    /// Instantiate in the beginning, resetting and retrieving gameObjects
    /// should we need them again.
    /// </summary>
    public class ObjectPoolManager : MonoBehaviour {
        private static ObjectPoolManager instance;

        /// <summary>
        /// Contains the name the prefab, as well as a Stack
        /// holding all unused, registered PooledBehaviors
        /// </summary>
        private IDictionary<string, Stack<PooledBehaviour>> pools;

        public static ObjectPoolManager Instance {
            get {
                if (instance == null) {
                    instance = FindObjectOfType<ObjectPoolManager>();
                }
                return instance;
            }
        }

        /// <summary>
        /// Pop an unused PooledBehavior from the approrpriate stack.
        /// If the Stack is empty, we Instantiate the object.
        /// </summary>
        /// <typeparam name="T">Derived type of the pooledbehavior we're retrieving</typeparam>
        /// <param name="script">Script on the prefab we want to retrieve</param>
        /// <returns>PooledBehavior derived script attached to a resetted gameObject.</returns>
        public T Get<T>(T script) where T : PooledBehaviour {
            Util.Assert(
                pools.ContainsKey(script.gameObject.name),
                string.Format("Unable to find {0} in Pool. Did you register the prefab?", script.gameObject.name)
                );
            PooledBehaviour pb = null;
            if (pools[script.gameObject.name].Count == 0) {
                //Util.Log(string.Format("Pool ran out of {0}. Instantiating...", script.gameObject.name));
                pb = (Instantiate(script.gameObject)).GetComponent<PooledBehaviour>();
                pb.gameObject.name = script.gameObject.name;
            } else {
                //Util.Log(string.Format("Getting {0} from a pool of {1}.", script.gameObject.name, pools[script.gameObject.name].Count));
                pb = pools[script.gameObject.name].Pop();
            }
            pb.gameObject.SetActive(true);
            return pb.GetComponent<T>();
        }

        /// <summary>
        /// Instantiate prefabs into the Dictionary for later retrieval.
        /// </summary>
        /// <param name="prefab">Prefab we want to register.</param>
        /// <param name="count">Number of prefab we want to register into the pool.</param>
        public void Register(PooledBehaviour prefab, int count) {
            //Util.Log(string.Format("Registering {0} of {1}.", count, prefab.gameObject.name));
            if (!pools.ContainsKey(prefab.gameObject.name)) {
                pools.Add(prefab.name, new Stack<PooledBehaviour>());
            }
            for (int i = 0; i < count; i++) {
                PooledBehaviour pb = (Instantiate(prefab.gameObject)).GetComponent<PooledBehaviour>();
                pb.gameObject.name = prefab.gameObject.name;
                pb.gameObject.SetActive(false);
                Util.Parent(pb.gameObject, this.gameObject);
                pools[prefab.name].Push(pb);
            }
        }

        /// <summary>
        /// Return a PooledBehavior to the pool.
        /// </summary>
        /// <param name="pb">PooledBehavior we want to return to the pool.</param>
        public void Return(PooledBehaviour pb) {
            Util.Assert(
                pools.ContainsKey(pb.gameObject.name),
                string.Format("Unable to find {0} in Pool. Did you use Instantiate?", pb.gameObject.name));
            pb.Reset();
            pb.gameObject.SetActive(false);
            pools[pb.gameObject.name].Push(pb);
            Util.Parent(pb.gameObject, this.gameObject);
        }

        public void Start() {
            pools = new Dictionary<string, Stack<PooledBehaviour>>();
        }
    }
}