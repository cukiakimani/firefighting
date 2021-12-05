using System.Collections.Generic;
using UnityEngine;

namespace SweetLibs
{
    public static class DebugPhysics
    {
        public static bool Raycast(
            Vector3 origin,
            Vector3 direction,
            out RaycastHit hit,
            float maxLength,
            LayerMask layers,
            bool debug = false)
        {
            if (Physics.Raycast(origin, direction, out hit, maxLength, layers))
            {
                float dist = Vector3.Distance(origin, hit.point);

                if (debug)
                    DebugExtension.DebugArrow(origin, direction * dist, Color.red);

                return true;
            }

            if (debug)
                DebugExtension.DebugArrow(origin, direction * maxLength, Color.green);

            return false;
        }

        public static bool Raycast(
            Ray ray,
            out RaycastHit hit,
            float maxLength,
            LayerMask layers,
            bool debug = false)
        {
            return DebugPhysics.Raycast(ray.origin, ray.direction, out hit, maxLength, layers, debug);
        }

        public static bool BoxCast(
            Vector3 center,
            Vector3 halfExtents,
            Vector3 direction,
            Quaternion orientation,
            out RaycastHit hitInfo,
            float maxDistance,
            LayerMask layers,
            bool debug = false
            )
        {
            if (Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layers))
            {
                if (debug)
                {
                    Vector3 endPoint = center + direction * hitInfo.distance;
                    
                    DebugExtension.DebugArrow(center, direction * hitInfo.distance, Color.red);
                    DebugExtension.DebugLocalCube(Matrix4x4.TRS(center, orientation, Vector3.one), halfExtents * 2f, Color.red, Vector3.zero);
                    DebugExtension.DebugLocalCube(Matrix4x4.TRS(endPoint, orientation, Vector3.one), halfExtents * 2f, Color.red, Vector3.zero);
                }

                return true;
            }

            if (debug)
            {
                Vector3 endPoint = center + direction * maxDistance;
                
                DebugExtension.DebugArrow(center, direction * maxDistance, Color.green);
                DebugExtension.DebugLocalCube(Matrix4x4.TRS(center, orientation, Vector3.one), halfExtents * 2f, Color.green, Vector3.zero);
                DebugExtension.DebugLocalCube(Matrix4x4.TRS(endPoint, orientation, Vector3.one), halfExtents * 2f, Color.green, Vector3.zero);
            }

            return false;
        }

        public static bool SphereCast(
            Vector3 origin,
            float radius,
            Vector3 direction,
            out RaycastHit hitInfo,
            float maxDistance,
            LayerMask layerMask,
            bool debug = false)
        {
            if (Physics.SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask))
            {
                if (debug)
                {
                    DebugExtension.DebugWireSphere(origin, Color.red, radius);
                    DebugExtension.DebugArrow(origin, direction.normalized * hitInfo.distance, Color.red);
                    DebugExtension.DebugWireSphere(origin + direction.normalized * hitInfo.distance, Color.red, radius);
                }

                return true;
            }
            
            if (debug)
            {
                DebugExtension.DebugWireSphere(origin, Color.green, radius);
                DebugExtension.DebugArrow(origin, direction.normalized * maxDistance, Color.green);
                DebugExtension.DebugWireSphere(origin + direction.normalized * maxDistance, Color.green, radius);
            }

            return false;
        }

        public static bool OverlapSphere(
            Vector3 position,
            float radius,
            out Collider[] colliders,
            LayerMask layerMask = default,
            bool debug = false)
        {
            colliders = Physics.OverlapSphere(position, radius, layerMask);

            if (colliders.Length > 0)
            {
                if (debug)
                    DebugExtension.DebugWireSphere(position, Color.red, radius);

                return true;
            }

            if (debug)
                DebugExtension.DebugWireSphere(position, Color.green, radius);

            return false;
        }
    }
}