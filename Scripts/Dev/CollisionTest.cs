using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SpaceEngine;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    public static bool Taken = false;

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (CollisionTest.Taken)
    //    {
    //        Destroy(collision.gameObject);
    //        Taken = false;
    //    }
    //    else Taken = true;
    //}

    public GameObject Star;
    public GameObject Planet;

    public float Force;

    public int time;

    public ComputeShader Shader;

    public RenderTexture Texture;

    private int kernel;

    ComputeBuffer positionBuffer;
    ComputeBuffer massBuffer;
    ComputeBuffer dataBuffer;

    Vector3[] positions;
    float[] masses;
    GravityData[] data;

    Vector3[] forces;

    Vector3 force = Vector3.zero;

    private void Start()
    {
        time = 0;

        Texture = new RenderTexture(512, 512, 1);
        Texture.enableRandomWrite = true;
        Texture.Create();

        //ComputeShader shader = Resources.Load<ComputeShader>(@"../../Shaders/NewComputeShader.compute");
        // ComputeShader shader = Resources.Load<ComputeShader>("A.compute");
        // Debug.Log(shader);

        Shader.SetFloat("Mult", SpaceMath.Mult);
        Shader.SetFloat("Unit", SpaceMath.Unit);
        Shader.SetInt("NumOfObjects", Gravity.GravityObjects.Count);
        kernel = Shader.FindKernel("CSMain");

        positionBuffer = new ComputeBuffer(Gravity.GravityObjects.Count, 12);
        massBuffer = new ComputeBuffer(Gravity.GravityObjects.Count, 4);
        // dataBuffer = new ComputeBuffer((int) Mathf.Pow(Gravity.GravityObjects.Count, 2), 12);
        dataBuffer = new ComputeBuffer((int) Gravity.GravityObjects.Count, 12);

        positions = new Vector3[Gravity.GravityObjects.Count];
        masses = new float[Gravity.GravityObjects.Count];
        // data = new GravityData[(int) Mathf.Pow(Gravity.GravityObjects.Count, 2)];
        data = new GravityData[(int) Gravity.GravityObjects.Count];

        forces = new Vector3[Gravity.GravityObjects.Count];
    }

    private void FixedUpdate()
    {
        // var stopWatch = new Stopwatch();
        // stopWatch.Start();

        for (int i = 0; i < Gravity.GravityObjects.Count; i++)
        {
            positions[i] = Gravity.GravityObjects[i].rb.position;
            masses[i] = Gravity.GravityObjects[i].rb.mass;
            // data[i] = new GravityData { Force = Vector3.zero };
            data[i] = new GravityData { x = 0, y = 0, z = 0 };
        }

        // stopWatch.Stop();
        // print(stopWatch.ElapsedMilliseconds);

        // stopWatch.Start();

        positionBuffer.SetData(positions);
        massBuffer.SetData(masses);
        dataBuffer.SetData(data);

        Shader.SetBuffer(kernel, "Positions", positionBuffer);
        Shader.SetBuffer(kernel, "Masses", massBuffer);
        Shader.SetBuffer(kernel, "Data", dataBuffer);

        // stopWatch.Stop();
        // print(stopWatch.ElapsedMilliseconds);

        // stopWatch.Start();

        Shader.Dispatch(kernel, Gravity.GravityObjects.Count, Gravity.GravityObjects.Count, 1);

        // stopWatch.Stop();
        // print(stopWatch.ElapsedMilliseconds);

        // stopWatch.Start();

        dataBuffer.GetData(data);

        // stopWatch.Stop();
        // print(stopWatch.ElapsedMilliseconds);

        // stopWatch.Start();
        int num = Gravity.GravityObjects.Count;
        // for (int i = 0; i < num; i++)
        // {
        //     Vector3 force = Vector3.zero;
        //     for (int j = 0; j < num; j++)
        //     {
        //         if (i != j)
        //         {
        //             force += data[(i * num) + j].Direction;
        //         }
        //     }
        //     Gravity.GravityObjects[i].rb.AddForce(force);
        // }

        // Thread t = new Thread(new ThreadStart(CalculateForces));
        // t.Start();

        // for (int i = 0; i < num / 2; i++)
        // {
        //     for (int j = 0; j < num; j++)
        //     {
        //         if (i != j)
        //         {
        //             forces[i] += data[(i * num) + j].Force;
        //             // forces[i] += data[i].Force;
        //         }
        //     }
        //     // Gravity.GravityObjects[i].rb.AddForce(force);
        // }

        // t.Join();
        // Vector3 force = Vector3.zero;
        for (int i = 0; i < num; i++)
        {
            // Gravity.GravityObjects[i].rb.AddForce(forces[i]);
            // Gravity.GravityObjects[i].rb.AddForce(data[i].Force);
            // Vector3 force = new Vector3(data[i].x / 1000, data[i].y / 1000, data[i].z / 1000);
            // Gravity.GravityObjects[i].rb.AddForce(force);
            force.x = data[i].x / 1000f;
            force.y = data[i].y / 1000f;
            force.z = data[i].z / 1000f;
            // print(force);
            // Gravity.GravityObjects[i].rb.AddForce(new Vector3(data[i].x / 100, data[i].y / 100, data[i].z / 100));
            Gravity.GravityObjects[i].rb.AddForce(force);
            // print(force);
        }

        // stopWatch.Stop();
        // print(stopWatch.ElapsedMilliseconds);

        // stopWatch = new Stopwatch();
        // stopWatch.Start();

        // for (int i = 0; i < num - 1; i++)
        // {
        //     for (int j = i + 1; j < num; j++)
        //     {
        //         // Vector3 direction = new Vector3(data[(i * num) + j].x, data[(i * num) + j].y, data[(i * num) + j].z);
        //         // direction *= data[(i * num) + j].Force;
        //         // Vector3 direction = data[(i * num) + j].Direction * data[(i * num) + j].Force;
        //         // Gravity.GravityObjects[i].rb.AddForce(direction);
        //         // Gravity.GravityObjects[j].rb.AddForce(-direction);
        //         int pos = (i * num) + j;
        //         Gravity.GravityObjects[i].rb.AddForce(data[pos].Direction);
        //         Gravity.GravityObjects[j].rb.AddForce(-data[pos].Direction);
        //         // Gravity.GravityObjects[i].rb.velocity = direction;
        //         // Gravity.GravityObjects[j].rb.velocity = -direction;
        //         // Gravity.GravityObjects[i].rb.position += direction;
        //         // Gravity.GravityObjects[j].rb.position += -direction;
        //         // Gravity.GravityObjects[i].gameObject.transform.position += direction;
        //         // Gravity.GravityObjects[j].gameObject.transform.position += -direction;
        //     }
        // }

        // stopWatch.Stop();
        // print(stopWatch.ElapsedMilliseconds);
    }

    private void CalculateForces()
    {
        int num = Gravity.GravityObjects.Count;
        for (int i = num / 2; i < num; i++)
        {
            for (int j = 0; j < num; j++)
            {
                if (i != j)
                {
                    // forces[i] += data[(i * num) + j].Force;
                    // forces[i] += data[i].Force;
                }
            }
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

    // struct GravityData
    // {
    //     public Vector3 Force;
    // }
    struct GravityData
    {
        public int x;
        public int y;
        public int z;
    }
}