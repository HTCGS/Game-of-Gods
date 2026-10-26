using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SpaceEngine;
using UnityEngine;

public class GravityManager2D : MonoBehaviour
{
    public static List<Rigidbody2D> Objects = new List<Rigidbody2D>();

    public CalculationDevice Device;

    public ComputeShader Shader;

    private int kernel;

    private ComputeBuffer positionBuffer;
    private ComputeBuffer massBuffer;
    private ComputeBuffer dataBuffer;

    private Vector3[] positions;
    private float[] masses;
    private GravityData[] data;

    private Vector3 force = Vector3.zero;
    private float fromInt = Mathf.Pow(10, 6);

    private int сhunkSize = 25000;
    private int chunkNum;
    private int size;

    private BarnesHutTree BHTree;

    // Start is called before the first frame update
    void Start()
    {
        if (Device == CalculationDevice.GPU)
        {
            Shader.SetFloat("Mult", SpaceMath.Mult);
            Shader.SetFloat("Unit", SpaceMath.Unit);
            Shader.SetInt("ToInt", (int)fromInt);
            kernel = Shader.FindKernel("CSMain");
            GPUSettingsInitialization();
        }

        for (int i = 0; i < this.transform.childCount; i++)
        {
            var child = this.transform.GetChild(i);
            var rb = child.GetComponent<Rigidbody2D>();
            GravityManager2D.Objects.Add(rb);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Device == CalculationDevice.CPU) CPUGravityCalculations();
        else GPUGravityCalculations();
    }

    private void CPUGravityCalculations()
    {
        // var stopWatch = new Stopwatch();
        // stopWatch.Start();
        this.BHTree = BarnesHutTree.CreateTree(Objects, this.transform.position, 400);
        // print(BHTree.NumberOfBodies);
        // print(GravityManager.Objects.Count);
        foreach (var body in Objects)
        {
            // if (body.name == "Crcle-319")
            // {
            //     print(body.name);
            // }
            BHTree.CalculateForces(body, 0.5f);
            // if (body.totalForce.magnitude < 1.0f)
            // {
            //     print(body.transform.name);
            // }
            // if (body.linearVelocity.magnitude == 0f)
            // {
            //     print(body.transform.name);
            //     print(body.totalForce);
            // }
            // print(body.linearVelocity.magnitude);
            // print(body.totalForce.magnitude);
        }
        // stopWatch.Stop();
        // print(stopWatch.ElapsedMilliseconds);
    }

    // private void CPUGravityCalculations()
    // {
    //     for (int i = 0; i < GravityManager2D.Objects.Count - 1; i++)
    //     {
    //         for (int j = i + 1; j < GravityManager2D.Objects.Count; j++)
    //         {
    //             Vector2 direction = GravityManager2D.Objects[j].position - GravityManager2D.Objects[i].position;
    //             float force = SpaceMath.Gravity.GetGravityForce(GravityManager2D.Objects[i], GravityManager2D.Objects[j], direction) * SpaceMath.Mult;
    //             direction = direction.normalized * force;
    //             // GravityManager.Objects[i].totalForce += direction;
    //             // GravityManager.Objects[j].totalForce -= direction;
    //             GravityManager2D.Objects[i].AddForce(direction);
    //             GravityManager2D.Objects[j].AddForce(-direction);
    //         }
    //     }
    // }

    void OnDrawGizmosSelected()
    {
        if (this.BHTree != null)
        {
            DrawBHTreeGizmos(BHTree);
        }
    }

    private void DrawBHTreeGizmos(BarnesHutTree tree)
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(tree.Position, new Vector2(tree.RegionSize, tree.RegionSize));
        if (tree.NumberOfBodies > 1)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(tree.CenterOfMass, 1f);
        }
        if (tree.NE != null) DrawBHTreeGizmos(tree.NE);
        if (tree.NW != null) DrawBHTreeGizmos(tree.NW);
        if (tree.SW != null) DrawBHTreeGizmos(tree.SW);
        if (tree.SE != null) DrawBHTreeGizmos(tree.SE);
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.DrawCube(Vector2.zero, Vector2.one);
    // }

    public void GPUSettingsInitialization()
    {
        this.chunkNum = (int)System.Math.Ceiling((double)GravityManager2D.Objects.Count / (double)сhunkSize);

        if (this.chunkNum > 1) this.size = this.сhunkSize;
        else this.size = GravityManager2D.Objects.Count;

        positionBuffer = new ComputeBuffer(size, 12);
        massBuffer = new ComputeBuffer(size, 4);
        dataBuffer = new ComputeBuffer((int)size, 12);

        positions = new Vector3[size];
        masses = new float[size];
        data = new GravityData[(int)size];
    }

    private void GPUGravityCalculations()
    {
        if (chunkNum == 1) GPUCalculation(GravityManager2D.Objects);
        else
        {
            List<Rigidbody> chunk = new List<Rigidbody>();
            int step = size / 2;
            int lastChunkObjects = GravityManager2D.Objects.Count - ((chunkNum - 1) * size);
            int emptyChunks = (float)lastChunkObjects / (float)step <= 1 ? 1 : 0;
            int chunks = ((chunkNum * 2) - 1) - emptyChunks;

            for (int i = 0; i < chunks; i++)
            {
                int lastObjects = 0;
                for (int j = i; j < chunks; j++)
                {
                    if (j == chunks - 1)
                    {
                        if (lastChunkObjects != сhunkSize)
                        {
                            if (emptyChunks == 0) lastObjects = step - (step - (lastChunkObjects - step));
                            else lastObjects = step - (step - (step - lastChunkObjects));
                        }
                    }

                    // for (int k = 0; k < step; k++)
                    // {
                    //     chunk.Add(GravityManager.Objects[(i * step) + k]);
                    // }

                    // for (int k = 0; k < step - lastObjects; k++)
                    // {
                    //     chunk.Add(GravityManager.Objects[((j + 1) * step) + k]);
                    // }

                    // GPUCalculation(chunk);
                    chunk.Clear();
                }
            }
        }
    }

    private void GPUCalculation(List<Rigidbody2D> chunk)
    {
        for (int i = 0; i < chunk.Count; i++)
        {
            positions[i] = chunk[i].position;
            masses[i] = chunk[i].mass;
            data[i] = new GravityData { x = 0, y = 0, z = 0 };
        }

        positionBuffer.SetData(positions);
        massBuffer.SetData(masses);
        dataBuffer.SetData(data);

        Shader.SetBuffer(kernel, "Positions", positionBuffer);
        Shader.SetBuffer(kernel, "Masses", massBuffer);
        Shader.SetBuffer(kernel, "Data", dataBuffer);

        Shader.Dispatch(kernel, chunk.Count, chunk.Count, 1);

        dataBuffer.GetData(data);

        for (int i = 0; i < chunk.Count; i++)
        {
            force.x = data[i].x / this.fromInt;
            force.y = data[i].y / this.fromInt;
            force.z = data[i].z / this.fromInt;
            chunk[i].AddForce(force);
        }
    }

    private void OnDestroy()
    {
        if ((positionBuffer != null) && (massBuffer != null) && (dataBuffer != null))
        {
            positionBuffer.Dispose();
            massBuffer.Dispose();
            dataBuffer.Dispose();
        }
    }

    private struct GravityData
    {
        public int x;
        public int y;
        public int z;
    }
}