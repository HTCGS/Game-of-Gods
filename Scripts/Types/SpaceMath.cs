﻿using SpaceEngine;
using UnityEngine;

namespace SpaceEngine
{
    public static class SpaceMath
    {
        public static readonly float G = 6.67408f * Mathf.Pow(10, -11);

        public static readonly float LightSpeed = 299792458f;

        public static readonly float AU = 149597870700f;

        public static readonly float EarthMass = 5.972f * Mathf.Pow(10, 24);
        public static readonly float EarthRadius = 6371000f;

        public static readonly float SolMass = 1.98892f * Mathf.Pow(10, 30);
        public static readonly float SolRadius = 695700000f;

        public static readonly float ToEngineMass = Mathf.Pow(10, 9) / (250 * SpaceMath.SolMass);

        // public static float Mult = 1000000000f;
        public static float Mult = 100000000f;
        // public static float Mult = 1000000f;
        // public static float Mult = 1f;

        //public static float Unit = 10000000f;
        // public static float Unit = 10f;
        public static float Unit = 1f;
        //public static float Unit = AU;
        //public static float Unit = AU / 2;
        //public static float Unit = 1000f;

        public static class Gravity
        {
            public static void AddGravityForce(GameObject to, GameObject from)
            {
                AddGravityForce(to, from, 1f);
            }

            public static void AddGravityForce(Rigidbody to, Rigidbody from)
            {
                AddGravityForce(to, from, 1f);
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
                Vector3 distance = second.transform.position - first.transform.position;
                return GetGravityForce(first, second, distance);
            }

            public static float GetGravityForce(Rigidbody first, Rigidbody second)
            {
                Vector3 distance = second.position - first.position;
                return GetGravityForce(first, second, distance);
            }

            public static float GetGravityForce(GameObject first, GameObject second, Vector3 distance)
            {
                return SpaceMath.G * ((first.GetComponent<Rigidbody>().mass * second.GetComponent<Rigidbody>().mass) /
                    Mathf.Pow(distance.magnitude * SpaceMath.Unit, 2));
            }

            public static float GetGravityForce(Rigidbody first, Rigidbody second, Vector3 distance)
            {
                return SpaceMath.G * ((first.mass * second.mass) / Mathf.Pow(distance.magnitude * SpaceMath.Unit, 2));
            }

            public static float GetGravityForce(float first, float second, float distance)
            {
                return SpaceMath.G * ((first * second) / Mathf.Pow(distance, 2));
            }

            public static float GravitationalRadius(float mass)
            {
                return (2 * mass * SpaceMath.G) / (SpaceMath.LightSpeed * SpaceMath.LightSpeed);
            }
        }

        public static class CosmicVelocity
        {

            public static float FirstCosmicVelocity(GameObject cosmicObject, GameObject parent)
            {
                return FirstCosmicVelocity(cosmicObject, parent, 1f);
            }

            public static float FirstCosmicVelocity(Rigidbody cosmicObject, Rigidbody parent)
            {
                return FirstCosmicVelocity(cosmicObject, parent, 1f);
            }

            public static float FirstCosmicVelocity(GameObject cosmicObject, GameObject parent, float mult)
            {
                Vector3 toParentDistance = parent.transform.position - cosmicObject.transform.position;
                return Mathf.Sqrt(SpaceMath.G * parent.GetComponent<Rigidbody>().mass / (toParentDistance.magnitude * SpaceMath.Unit)) * mult;
            }

            public static float FirstCosmicVelocity(Rigidbody cosmicObject, Rigidbody parent, float mult)
            {
                Vector3 toParentDistance = parent.position - cosmicObject.position;
                return Mathf.Sqrt(SpaceMath.G * parent.mass / (toParentDistance.magnitude * SpaceMath.Unit)) * mult;
            }
        }

        public static class Orbit
        {
            public static float L1L2Distance(float mass1, float mass2, float distance)
            {
                return distance * Mathf.Pow(mass2 / (3 * mass1), 1f / 3f);
            }

            public static float L1L2Distance(Rigidbody biggest, Rigidbody smallest)
            {
                float distance = (biggest.position - smallest.position).magnitude;
                return L1L2Distance(biggest.mass, smallest.mass, distance);
            }

            public static Vector3 L1Point(Rigidbody biggest, Rigidbody smallest)
            {
                Vector3 distance = smallest.position - biggest.position;
                return smallest.position + (distance.normalized * L1L2Distance(biggest.mass, smallest.mass, distance.magnitude));
            }

            public static Vector3 L2Point(Rigidbody biggest, Rigidbody smallest)
            {
                Vector3 distance = smallest.position - biggest.position;
                return smallest.position + (-distance.normalized * L1L2Distance(biggest.mass, smallest.mass, distance.magnitude));
            }

            public static Vector3 L3Point(Rigidbody biggest, Rigidbody smallest)
            {
                Vector3 distance = smallest.position - biggest.position;
                float r = distance.magnitude + (distance.magnitude * ((5 * smallest.mass) / (12 * biggest.mass)));
                return biggest.position + (-distance.normalized * r);
            }

            public static Vector3 L4Point(Rigidbody biggest, Rigidbody smallest)
            {
                SpaceEngine.Orbit satOrbit = smallest.gameObject.With(g => g.GetComponent<SpaceEngine.Orbit>());
                if (satOrbit)
                {
                    Vector3 toParent = smallest.position - biggest.position;
                    Vector3 orbitDirection = satOrbit.GetOrbitalRotationVector(toParent, satOrbit.OrbitDirection);
                    Vector3 norm = Vector3.Cross(toParent, orbitDirection);
                    Vector3 rotatedVec = Quaternion.AngleAxis(-60f, norm) * toParent;
                    return rotatedVec + biggest.position;
                }
                return Vector3.zero;
            }

            public static Vector3 L5Point(Rigidbody biggest, Rigidbody smallest)
            {
                SpaceEngine.Orbit satOrbit = smallest.gameObject.With(g => g.GetComponent<SpaceEngine.Orbit>());
                if (satOrbit)
                {
                    Vector3 toParent = smallest.position - biggest.position;
                    Vector3 orbitDirection = satOrbit.GetOrbitalRotationVector(toParent, satOrbit.OrbitDirection);
                    Vector3 norm = Vector3.Cross(toParent, orbitDirection);
                    Vector3 rotatedVec = Quaternion.AngleAxis(60f, norm) * toParent;
                    return rotatedVec + biggest.position;
                }
                return Vector3.zero;
            }

            public static float SphereOfInfluence(float biggest, float smallest, float distance)
            {
                return distance * (Mathf.Pow(smallest / biggest, 0.4f));
            }

            public static float SphereOfInfluence(Rigidbody biggest, Rigidbody smallest)
            {
                float distance = (biggest.position - smallest.position).magnitude * SpaceMath.Unit;
                return distance * (Mathf.Pow(smallest.mass / biggest.mass, 0.4f));
            }
        }

        public static class Shape
        {

            public static float SphereRadius(GameObject body, float density)
            {
                return SphereRadius(body.GetComponent<Rigidbody>(), density);
            }

            public static float SphereRadius(Rigidbody body, float density)
            {
                return SphereRadius(body.mass, density);
            }

            public static float SphereRadius(float mass, float density)
            {
                return Mathf.Pow(mass / (1.333f * Mathf.PI * density), 1f / 3f);
            }

            public static float SphereMass(GameObject body, float density)
            {
                return SphereMass(body.transform.localScale.magnitude, density);
            }

            public static float SphereMass(float radius, float density)
            {
                return 1.333f * Mathf.PI * Mathf.Pow(radius, 3) * density;
            }
        }

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