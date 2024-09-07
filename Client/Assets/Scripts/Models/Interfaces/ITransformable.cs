using UnityEngine;

namespace Models.Interfaces
{
    public interface ITransformable
    {
        Vector3 Position { get; set; }

        Quaternion Rotation { get; set; }

        Vector3 Scale { get; set; }

        void Translate(Vector3 translation);
    }
}
