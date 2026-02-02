using System.Runtime.InteropServices;
using UnityEngine;

public struct State
{
    public float x, y, z;
    public float vx, vy, vz;
    public float qw, qx, qy, qz;
    public float wx, wy, wz;
    public float roll, pitch, yaw;
}

public struct SimplifiedState {
    public float px, py, pz;
    public float vx, vy, vz;
    public float qw, qx, qy, qz;
}


public static class NativeSim
{
#if UNITY_STANDALONE_LINUX
    const string DLL_NAME = "NativeSim";
#else
    const string DLL_NAME = "__Internal";
#endif

    [DllImport(DLL_NAME)]
    public static extern System.IntPtr sim_create();

    [DllImport(DLL_NAME)]
    public static extern void sim_destroy(System.IntPtr sim);

    [DllImport(DLL_NAME)]
    public static extern void sim_reset(System.IntPtr sim, float x, float y, float z);

    [DllImport(DLL_NAME)]
    public static extern void sim_step(System.IntPtr sim,
                                        float thrust, float rollRate,
                                        float pitchRate, float yawRate,
                                        float dt);

    [DllImport(DLL_NAME)]
    public static extern SimplifiedState sim_get_simplified_state(System.IntPtr sim);
}

