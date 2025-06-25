using UnityEngine;

namespace Game
{
    public static class TransformUtils
    {
        public static Transform[] GetAllChildTransforms(Transform parent)
        {
            var childCount = parent.childCount;
            var children = new Transform[childCount];

            for (int i = 0; i < childCount; i++)
            {
                children[i] = parent.GetChild(i);
            }

            return children;
        }
    }
}