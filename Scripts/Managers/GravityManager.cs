using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SpaceEngine;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public static List<Gravity> Objects = new List<Gravity>();

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

    // Start is called before the first frame update
    void Start()
    {
        Shader.SetFloat("Mult", SpaceMath.Mult);
        Shader.SetFloat("Unit", SpaceMath.Unit);
        Shader.SetInt("NumOfObjects", GravityManager.Objects.Count);
        kernel = Shader.FindKernel("CSMain");

        positionBuffer = new ComputeBuffer(GravityManager.Objects.Count, 12);
        massBuffer = new ComputeBuffer(GravityManager.Objects.Count, 4);
        dataBuffer = new ComputeBuffer((int) GravityManager.Objects.Count, 12);

        positions = new Vector3[GravityManager.Objects.Count];
        masses = new float[GravityManager.Objects.Count];
        data = new GravityData[(int) GravityManager.Objects.Count];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GPUGravityCalculations();
    }

    private void CPUGravityCalculations()
    {
        for (int i = 0; i < GravityManager.Objects.Count - 1; i++)
        {
            for (int j = i + 1; j < GravityManager.Objects.Count; j++)
            {
                Vector3 direction = GravityManager.Objects[j].rb.position - GravityManager.Objects[i].rb.position;
                float force = SpaceMath.GetGravityForce(GravityManager.Objects[i].rb, GravityManager.Objects[j].rb, direction) * SpaceMath.Mult;
                direction = direction.normalized * force;
                GravityManager.Objects[i].rb.AddForce(direction);
                GravityManager.Objects[j].rb.AddForce(-direction);
            }
        }
    }

    private void GPUGravityCalculations()
    {
        for (int i = 0; i < GravityManager.Objects.Count; i++)
        {
            positions[i] = GravityManager.Objects[i].rb.position;
            masses[i] = GravityManager.Objects[i].rb.mass;
            data[i] = new GravityData { x = 0, y = 0, z = 0 };
        }

        positionBuffer.SetData(positions);
        massBuffer.SetData(masses);
        dataBuffer.SetData(data);

        Shader.SetBuffer(kernel, "Positions", positionBuffer);
        Shader.SetBuffer(kernel, "Masses", massBuffer);
        Shader.SetBuffer(kernel, "Data", dataBuffer);

        Shader.Dispatch(kernel, GravityManager.Objects.Count, GravityManager.Objects.Count, 1);

        dataBuffer.GetData(data);

        for (int i = 0; i < GravityManager.Objects.Count; i++)
        {
            force.x = data[i].x / this.fromInt;
            force.y = data[i].y / this.fromInt;
            force.z = data[i].z / this.fromInt;
            GravityManager.Objects[i].rb.AddForce(force);
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