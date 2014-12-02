using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RobinTheilade.RuntimeTreeCollision.Example
{
    /// <summary>
    /// Repositions trees when player collides with them.
    /// </summary>
    [AddComponentMenu("Physics/Runtime Tree Colliders - Example - Reposition Tree")]
    public class RepositionTree : MonoBehaviour
    {
        /// <summary>
        /// Repositions the tree that is collided with.
        /// </summary>
        /// <param name="collision">
        /// Information about the collision.
        /// </param>
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.name.Equals("Tree Collider", StringComparison.InvariantCulture))
            {
                var treeInstanceInfo = collision.collider.GetComponent<TreeInstanceInfo>();
                if (treeInstanceInfo != null)
                {
                    var treeInstance = treeInstanceInfo.treeInstance;
                    var terrain = treeInstanceInfo.terrain;
                    var treePrototype = treeInstanceInfo.TreePrototype;
                    var instanceIndex = treeInstanceInfo.TreeInstanceIndex;
                    var runtimeTreeColliders = terrain.GetComponent<RuntimeTreeColliders>();
                    var instances = terrain.terrainData.treeInstances;

                    // remove the tree from runtime tree colliders so
                    // we don't get colliders where the are no trees
                    runtimeTreeColliders.RemoveTree(treeInstance);

                    // move the tree to another position
                    treeInstance.position = new Vector3(Random.value, 0.0f, Random.value);
                    instances[instanceIndex] = treeInstance;

                    // reassign to force unity to recalculate
                    // this will lag due to the number of trees
                    // it should lag less if multiple smaller
                    // terrains with less trees in each were used
                    terrain.terrainData.treeInstances = instances;

                    // add the tree back again to runtime tree colliders
                    // to get a collider where the tree now is
                    runtimeTreeColliders.AddTree(treeInstance);
                }
            }
        }
    }
}
