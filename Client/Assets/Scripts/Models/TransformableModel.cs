using Models.Interfaces;
using UnityEngine;

namespace Models
{
    public abstract class TransformableModel : ModelBase, ITransformable
    {
        public Vector3 Position { get; set; }

        public Quaternion Rotation { get; set; }

        public Vector3 Scale { get; set; }

        public void Translate(Vector3 translation)
        {
            Position += translation;
        }
    }
}
