using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SpaceEngine;

namespace SpaceEngine
{
    public static class SpaceMath
    {
        public static float G = 6.67408f * Mathf.Pow(10, -11);

        public static float Mult = 1000000f;

        public static float Unit = 10000000f;
        //public static float Unit = 1f;

        public static float EarthMass = 5.972f * Mathf.Pow(10, 24);
        public static float EarthRadius = 6371000f;
        public static float EarthMult = 1f;

        public static float SolMass = 1.98892f * Mathf.Pow(10, 30);
        public static float SolRadius = 695700000f; 
        public static float SolMult = 1f;

        public static float ToEngineMass = Mathf.Pow(10, 9) / (250 * SpaceMath.SolMass);

        #region Gravity

        public static void AddGravityForce(GameObject to, GameObject from)
        {
            Vector3 direction = from.transform.position - to.transform.position;
            float force = GetGravityForce(to, from, direction);
            to.GetComponent<Rigidbody>().AddForce(direction.normalized * force * SpaceMath.Mult);
        }

        public static void AddGravityForce(Rigidbody to, Rigidbody from)
        {
            Vector3 direction = from.position - to.position;
            float force = GetGravityForce(to, from, direction);
            to.AddForce(direction.normalized * force * SpaceMath.Mult);
        }

        public static void AddGravityForce(GameObject to, GameObject from, float mult)
        {
            Vector3 direction = from.transform.position - to.transform.position;
            float force = GetGravityForce(to, from, direction);
            to.GetComponent<Rigidbody>().AddForce(direction.normalized * force * mult);
        }

        public static void AddGravityForce(Rigidbody to, Rigidbody from, float mult)
        {
            Vector3 direction = from.position - to.position;
            float force = GetGravityForce(to, from, direction);
            to.AddForce(direction.normalized * force * mult);
        }

        public static float GetGravityForce(GameObject first, GameObject second)
        {
            Vector3 direction = second.transform.position - first.transform.position;
            return SpaceMath.G * ((first.GetComponent<Rigidbody>().mass * second.GetComponent<Rigidbody>().mass) /
                Mathf.Pow(direction.magnitude * SpaceMath.Unit, 2));
        }

        public static float GetGravityForce(GameObject first, GameObject second, Vector3 direction)
        {
            return SpaceMath.G * ((first.GetComponent<Rigidbody>().mass * second.GetComponent<Rigidbody>().mass) /
                Mathf.Pow(direction.magnitude * SpaceMath.Unit, 2));
        }

        public static float GetGravityForce(Rigidbody first, Rigidbody second, Vector3 direction)
        {
            return SpaceMath.G * ((first.mass * second.mass) / Mathf.Pow(direction.magnitude * SpaceMath.Unit, 2));
        }

        #endregion

        public static Vector3 GetTangentPoint(GameObject circle, GameObject point)
        {
            float radius = circle.transform.localScale.x / 2;
            float alpha = Mathf.Atan((point.transform.position.z - circle.transform.position.z) / (point.transform.position.x - circle.transform.position.x));
            float beta = Mathf.Acos(radius /
                (Mathf.Sqrt(Mathf.Pow(point.transform.position.x - circle.transform.position.x, 2) +
                Mathf.Pow(point.transform.position.z - circle.transform.position.z, 2))));
            float gamma = 2 * Mathf.PI - alpha - beta;

            Vector3 toCircleDirection = circle.transform.position - point.transform.position;
            float angleX = Vector3.Angle(toCircleDirection, circle.transform.right);

            Vector3 tangent;
            tangent.y = circle.transform.position.y;
            if (angleX >= 0 && angleX <= 90)
            {
                tangent.x = circle.transform.position.x + radius * Mathf.Cos(gamma);
                tangent.z = circle.transform.position.z - radius * Mathf.Sin(gamma);
            }
            else
            {
                tangent.x = circle.transform.position.x - radius * Mathf.Cos(gamma);
                tangent.z = circle.transform.position.z + radius * Mathf.Sin(gamma);
            }
            return tangent;
        }

    }
}