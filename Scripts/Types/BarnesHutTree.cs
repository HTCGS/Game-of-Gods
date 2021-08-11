using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEngine
{
    public class BarnesHutTree
    {
        public BarnesHutTree() { }

        public BarnesHutTree(Vector3 position, float regionSize)
        {
            this.Position = position;
            this.RegionSize = regionSize;
        }
        public Vector3 Position;
        public Vector3 CenterOfMass;
        public float Mass;
        public float RegionSize;
        public int NumberOfBodies;
        public Rigidbody Body;
        public BarnesHutTree NW;
        public BarnesHutTree NE;
        public BarnesHutTree SW;
        public BarnesHutTree SE;

        public static BarnesHutTree CreateTree(List<Rigidbody> bodies, Vector3 position, int size)
        {
            var tree = new BarnesHutTree(position, size);

            foreach (var body in bodies)
            {
                tree.AddBody(body);
            }
            return tree;
        }

        public BarnesHutTree CreateChildrens()
        {
            var childSize = this.RegionSize / 2f;
            var NWpos = (this.Position + new Vector3(this.Position.x - childSize, this.Position.y + childSize, 0)) / 2f;
            var NEpos = (this.Position + new Vector3(this.Position.x + childSize, this.Position.y + childSize, 0)) / 2f;
            var SWpos = (this.Position + new Vector3(this.Position.x - childSize, this.Position.y - childSize, 0)) / 2f;
            var SEpos = (this.Position + new Vector3(this.Position.x + childSize, this.Position.y - childSize, 0)) / 2f;

            NW = new BarnesHutTree(NWpos, childSize);
            NE = new BarnesHutTree(NEpos, childSize);
            SW = new BarnesHutTree(SWpos, childSize);
            SE = new BarnesHutTree(SEpos, childSize);
            return this;
        }

        public BarnesHutTree GetQuadrant(Rigidbody body) => body.position switch
        {
            { x: var x, y: var y } when x < this.Position.x && y >= this.Position.y => this.NW,
            { x: var x, y: var y } when x < this.Position.x && y < this.Position.y => this.SW,
            { x: var x, y: var y } when x >= this.Position.x && y >= this.Position.y => this.NE,
            { x: var x, y: var y } when x >= this.Position.x && y < this.Position.y => this.SE,
            _ => throw new Exception("!!!SOS!!! Out of bounds!")
        };

        public void AddBody(Rigidbody body)
        {
            if (this.NumberOfBodies == 0)
            {
                this.Body = body;
                this.CenterOfMass = body.position;
                this.Mass = body.mass;
                this.NumberOfBodies++;
            }
            else if (this.NumberOfBodies == 1)
            {
                this.CenterOfMass = CalculateCenterOfMass(this.CenterOfMass, this.Mass, body.position, body.mass);
                this.Mass += body.mass;
                this.NumberOfBodies++;

                this.CreateChildrens();

                var quad = this.GetQuadrant(this.Body);
                quad.AddBody(this.Body);

                quad = this.GetQuadrant(body);
                quad.AddBody(body);
            }
            else
            {
                this.CenterOfMass = CalculateCenterOfMass(this.CenterOfMass, this.Mass, body.position, body.mass);
                this.Mass += body.mass;
                this.NumberOfBodies++;

                var quad = this.GetQuadrant(body);
                quad.AddBody(body);
            }
        }

        public static Vector3 CalculateCenterOfMass(Vector3 p1, float m1, Vector3 p2, float m2)
        {
            var m = m1 + m2;
            return new Vector3((p1.x * m1 + p2.x * m2) / m, (p1.y * m1 + p2.y * m2) / m, 0);
        }

        public static Vector3 CalculateGravityForce(Vector3 p1, float m1, Vector3 p2, float m2)
        {
            var direction = p1 - p2;
            float force = (6.67430f * m1 * m2) / (direction.magnitude * direction.magnitude);
            direction = direction.normalized * force;
            return direction;
        }

        public void CalculateForces(Rigidbody body, float theta)
        {
            if (this.NumberOfBodies == 0) return;
            else if (this.NumberOfBodies == 1)
            {
                if (this.Body == body) return;
                var direction = (this.CenterOfMass - body.position);
                var distance = direction.magnitude;
                var force = SpaceMath.Gravity.GetGravityForce(this.Mass, body.mass, distance);
                body.AddForce(force * direction.normalized * SpaceMath.Mult);
            }
            else
            {
                var direction = (this.CenterOfMass - body.position);
                var distance = direction.magnitude;
                if (this.RegionSize / distance < theta)
                {
                    var force = SpaceMath.Gravity.GetGravityForce(this.Mass, body.mass, distance);
                    body.AddForce(force * direction.normalized * SpaceMath.Mult);
                }
                else
                {
                    this.NW.CalculateForces(body, theta);
                    this.NE.CalculateForces(body, theta);
                    this.SW.CalculateForces(body, theta);
                    this.SE.CalculateForces(body, theta);
                }
            }
        }
    }
}