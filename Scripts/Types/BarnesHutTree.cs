using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceEngine
{
    public class BarnesHutTree
    {
        public Vector2 Position;
        public Vector2 CenterOfMass;
        public float Mass;
        public float RegionSize;
        public int NumberOfBodies;
        public Rigidbody2D Body;
        public BarnesHutTree NW;
        public BarnesHutTree NE;
        public BarnesHutTree SW;
        public BarnesHutTree SE;

        public BarnesHutTree() { }

        public BarnesHutTree(Vector2 position, float regionSize)
        {
            this.Position = position;
            this.RegionSize = regionSize;
        }

        public static BarnesHutTree CreateTree(List<Rigidbody2D> bodies, Vector2 position, float size)
        {
            var tree = new BarnesHutTree(position, size);
            tree.CreateLeafs();

            foreach (var body in bodies)
            {
                tree.AddBody(body);
            }
            return tree;
        }

        public BarnesHutTree CreateLeafs()
        {
            var leafSize = this.RegionSize / 2f;
            var NEpos = (this.Position + new Vector2(this.Position.x + leafSize, this.Position.y + leafSize)) / 2f;
            var NWpos = (this.Position + new Vector2(this.Position.x - leafSize, this.Position.y + leafSize)) / 2f;
            var SWpos = (this.Position + new Vector2(this.Position.x - leafSize, this.Position.y - leafSize)) / 2f;
            var SEpos = (this.Position + new Vector2(this.Position.x + leafSize, this.Position.y - leafSize)) / 2f;

            NE = new BarnesHutTree(NEpos, leafSize);
            NW = new BarnesHutTree(NWpos, leafSize);
            SW = new BarnesHutTree(SWpos, leafSize);
            SE = new BarnesHutTree(SEpos, leafSize);
            return this;
        }

        public BarnesHutTree GetQuadrant(Rigidbody2D body) => body.position switch
        {
            { x: var x, y: var y } when x < this.Position.x && y >= this.Position.y => this.NW,
            { x: var x, y: var y } when x < this.Position.x && y < this.Position.y => this.SW,
            { x: var x, y: var y } when x >= this.Position.x && y >= this.Position.y => this.NE,
            { x: var x, y: var y } when x >= this.Position.x && y < this.Position.y => this.SE,
            _ => throw new Exception("!!!SOS!!! Out of bounds!")
        };

        public void AddBody(Rigidbody2D body)
        {
            var quad = this.GetQuadrant(body);
            if (quad.NumberOfBodies == 0)
            {
                quad.Body = body;
                quad.CenterOfMass = body.position;
                quad.Mass = body.mass;
                quad.NumberOfBodies++;
            }
            else if (quad.NumberOfBodies == 1)
            {
                quad.CreateLeafs();
                quad.NumberOfBodies = 0;
                quad.CenterOfMass = Vector2.zero;
                quad.Mass = 0;
                quad.AddBody(quad.Body);
                quad.Body = null;
                quad.AddBody(body);
            }
            else quad.AddBody(body);
            this.CenterOfMass = CalculateCenterOfMass(this.CenterOfMass, this.Mass, body.position, body.mass);
            this.Mass += body.mass;
            this.NumberOfBodies++;












            // if (this.NumberOfBodies == 0)
            // {
            //     this.Body = body;
            //     this.CenterOfMass = body.position;
            //     this.Mass = body.mass;
            //     this.NumberOfBodies++;
            // }
            // else if (this.NumberOfBodies == 1)
            // {
            //     this.CenterOfMass = CalculateCenterOfMass(this.CenterOfMass, this.Mass, body.position, body.mass);
            //     this.Mass += body.mass;
            //     this.NumberOfBodies++;

            //     this.CreateLeafs();

            //     var quad = this.GetQuadrant(this.Body);
            //     quad.AddBody(this.Body);
            //     this.Body = null;

            //     quad = this.GetQuadrant(body);
            //     quad.AddBody(body);
            // }
            // else
            // {
            //     this.CenterOfMass = CalculateCenterOfMass(this.CenterOfMass, this.Mass, body.position, body.mass);
            //     this.Mass += body.mass;
            //     this.NumberOfBodies++;

            //     var quad = this.GetQuadrant(body);
            //     quad.AddBody(body);
            // }
        }

        public static Vector2 CalculateCenterOfMass(Vector3 p1, float m1, Vector2 p2, float m2)
        {
            var m = m1 + m2;
            return new Vector2((p1.x * m1 + p2.x * m2) / m, (p1.y * m1 + p2.y * m2) / m);
        }

        public static Vector2 CalculateGravityForce(Vector2 p1, float m1, Vector2 p2, float m2)
        {
            var direction = p1 - p2;
            float force = (6.67430f * m1 * m2) / (direction.magnitude * direction.magnitude);
            direction = direction.normalized * force;
            return direction;
        }

        public void CalculateForces(Rigidbody2D body, float theta = 0.5f)
        {
            if (this.NumberOfBodies == 0) return;

            var direction = (this.CenterOfMass - body.position);
            var distance = direction.magnitude;
            if (this.RegionSize / distance < theta || this.NumberOfBodies == 1)
            {
                if (this.Body == body) return;
                var force = SpaceMath.Gravity.GetGravityForce(this.Mass, body.mass, distance);
                // body.totalForce += force * direction.normalized * SpaceMath.Mult;
                body.AddForce(force * direction.normalized * SpaceMath.Mult);
            }
            else
            {
                this.NE.CalculateForces(body, theta);
                this.NW.CalculateForces(body, theta);
                this.SW.CalculateForces(body, theta);
                this.SE.CalculateForces(body, theta);
            }






            // if (this.NumberOfBodies == 0) return;
            // else if (this.NumberOfBodies == 1)
            // {
            //     if (this.Body == body) return;
            //     var direction = (this.CenterOfMass - body.position);
            //     var distance = direction.magnitude;
            //     var force = SpaceMath.Gravity.GetGravityForce(this.Mass, body.mass, distance);
            //     // body.AddForce(force * direction.normalized * SpaceMath.Mult);
            //     body.totalForce += force * direction.normalized * SpaceMath.Mult;
            // }
            // else
            // {
            //     var direction = (this.CenterOfMass - body.position);
            //     var distance = direction.magnitude;
            //     if (this.RegionSize / distance < theta)
            //     {
            //         var force = SpaceMath.Gravity.GetGravityForce(this.Mass, body.mass, distance);
            //         // body.AddForce(force * direction.normalized * SpaceMath.Mult);
            //         body.totalForce += force * direction.normalized * SpaceMath.Mult;
            //     }
            //     else
            //     {
            //         this.NE.CalculateForces(body, theta);
            //         this.NW.CalculateForces(body, theta);
            //         this.SW.CalculateForces(body, theta);
            //         this.SE.CalculateForces(body, theta);
            //     }
            // }
        }
    }
}