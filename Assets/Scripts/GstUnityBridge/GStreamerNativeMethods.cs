/*
*  GStreamer - Unity3D bridge (GUB).
*  Copyright (C) 2016  Fundacio i2CAT, Internet i Innovacio digital a Catalunya
*
*  This program is free software: you can redistribute it and/or modify
*  it under the terms of the GNU Lesser General Public License as published by
*  the Free Software Foundation, either version 3 of the License, or
*  (at your option) any later version.
*
*  This program is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU Lesser General Public License for more details.
*
*  You should have received a copy of the GNU Lesser General Public License
*  along with this program.  If not, see <http://www.gnu.org/licenses/>.
*
*  Authors:  Xavi Artigas <xavi.artigas@i2cat.net>
*/

using UnityEngine;
using System.Runtime.InteropServices;	// For DllImport.
using System;
using System.IO;

public class GStreamerNativeMethods
{
    internal const string DllName = "GstUnityBridge";

    [DllImport("gstreamer_android", CallingConvention = CallingConvention.Cdecl)]
    extern static private UIntPtr gst_android_get_application_class_loader();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I4)]
    extern static private bool gub_ref(
        [MarshalAs(UnmanagedType.LPStr)]string gst_debug_string,
        int isEditor);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_unref();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I4)]
    extern static private bool gub_is_active();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void GUBUnityDebugLogPFN(
        [MarshalAs(UnmanagedType.I4)]int level,
        [MarshalAs(UnmanagedType.LPStr)]string message);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_log_set_unity_handler(GUBUnityDebugLogPFN pfn);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_pipeline_destroy(System.IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_pipeline_play(System.IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_pipeline_pause(System.IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_pipeline_stop(System.IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private bool gub_pipeline_is_loaded(System.IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private bool gub_pipeline_is_playing(System.IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_pipeline_close(System.IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private System.IntPtr gub_pipeline_create(
        [MarshalAs(UnmanagedType.LPStr)]string name,
        System.IntPtr eos_pfn,
        System.IntPtr error_pfn,
        System.IntPtr qos_pfn,
        System.IntPtr userdata);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_pipeline_setup_decoding(System.IntPtr p,
        [MarshalAs(UnmanagedType.LPStr)]string uri,
        bool playAllIndexes,
        int video_index,
        int audio_index,
        [MarshalAs(UnmanagedType.LPStr)]string net_clock_address,
        int net_clock_port,
        ulong basetime,
        [MarshalAs(UnmanagedType.LPStr)]string audioGUID,
        float crop_left, float crop_top, float crop_right, float crop_bottom);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private int gub_pipeline_grab_frame(System.IntPtr p, ref int w, ref int h);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_pipeline_blit_image(System.IntPtr p, System.IntPtr _TextureNativePtr);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void GUBPipelineOnEosPFN(System.IntPtr p);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void GUBPipelineOnErrorPFN(System.IntPtr p,
        [MarshalAs(UnmanagedType.LPStr)]string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void GUBPipelineOnQosPFN(System.IntPtr p,
        long current_jitter, ulong current_running_time, ulong current_stream_time, ulong current_timestamp,
    double proportion, ulong processed, ulong dropped);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private double gub_pipeline_get_duration(System.IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private double gub_pipeline_get_position(System.IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_pipeline_set_position(System.IntPtr p, double position);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_pipeline_setup_encoding(System.IntPtr p,
        [MarshalAs(UnmanagedType.LPStr)]string filename,
        int width, int height);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_pipeline_consume_image(System.IntPtr p, System.IntPtr rawdata, int size);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_pipeline_stop_encoding(System.IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_pipeline_set_volume(System.IntPtr p, double volume);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_pipeline_set_adaptive_bitrate_limit(System.IntPtr p, float bitrate_limit);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private IntPtr gub_get_log_method(System.IntPtr p);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static internal IntPtr GetRenderEventFunc();

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private void gub_set_texture(IntPtr p, IntPtr texPtr);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    extern static private bool gub_pipeline_is_ready_to_render(ref int w, ref int h);

    protected System.IntPtr m_Instance;

    internal GStreamerNativeMethods(string name, GUBPipelineOnEosPFN eos_pfn, GUBPipelineOnErrorPFN error_pfn, GUBPipelineOnQosPFN qos_pfn, System.IntPtr userdata)
    {
        m_Instance = gub_pipeline_create(name,
            eos_pfn == null ? (System.IntPtr)null : Marshal.GetFunctionPointerForDelegate(eos_pfn),
            error_pfn == null ? (System.IntPtr)null : Marshal.GetFunctionPointerForDelegate(error_pfn),
            qos_pfn == null ? (System.IntPtr)null : Marshal.GetFunctionPointerForDelegate(qos_pfn),
            userdata);
    }

    internal static bool IsActive
    {
        get
        {
            return gub_is_active();
        }
    }

    public static void AddPluginsToPath()
    {
        // Setup the PATH environment variable so it can find the GstUnityBridge dll.
        var currentPath = Environment.GetEnvironmentVariable("PATH",
            EnvironmentVariableTarget.Process);
        var dllPath = "";

#if UNITY_EDITOR

#if UNITY_EDITOR_32
        dllPath = Application.dataPath + "/Plugins/x86";
#elif UNITY_EDITOR_64
        dllPath = Application.dataPath + "/Plugins/x86_64";
#endif

        if (currentPath != null && currentPath.Contains(dllPath) == false)
            Environment.SetEnvironmentVariable("PATH",
                dllPath + Path.PathSeparator +
                dllPath + "/GStreamer/bin" + Path.PathSeparator +
                currentPath,
                EnvironmentVariableTarget.Process);
#else
        dllPath = Application.dataPath + "/Plugins";
        if (currentPath != null && currentPath.Contains(dllPath) == false)
            Environment.SetEnvironmentVariable("PATH",
                dllPath + Path.PathSeparator +
                currentPath,
                EnvironmentVariableTarget.Process);
        Environment.SetEnvironmentVariable("GST_PLUGIN_PATH", dllPath, EnvironmentVariableTarget.Process);
#endif
    }

    internal static void Ref(string gst_debug_string, GUBUnityDebugLogPFN log_handler)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // Force loading of gstreamer_android.so before GstUnityBridge.so
        gst_android_get_application_class_loader();
        AndroidJNIHelper.debug = true;
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass gstAndroid = new AndroidJavaClass("org.freedesktop.gstreamer.GStreamer");
        gstAndroid.CallStatic("init", activity);
#endif
        gub_log_set_unity_handler(log_handler);
        gub_ref(gst_debug_string, Application.isEditor ? 1 : 0);
    }

    internal static void Unref()
    {
        gub_unref();
    }
    internal bool IsLoaded
    {
        get
        {
            return gub_pipeline_is_loaded(m_Instance);
        }
    }
    
    internal bool IsPlaying
    {
        get
        {
            return gub_pipeline_is_playing(m_Instance);
        }
    }

    internal void Destroy()
    {
        gub_pipeline_destroy(m_Instance);
    }

    internal void Play()
    {
        gub_pipeline_play(m_Instance);
    }

    internal void Pause()
    {
        gub_pipeline_pause(m_Instance);
    }

    internal void Stop()
    {
        gub_pipeline_stop(m_Instance);
    }

    internal void Close()
    {
        gub_pipeline_close(m_Instance);
    }

    internal double Duration
    {
        get { return gub_pipeline_get_duration(m_Instance); }
    }

    internal double Position
    {
        get { return gub_pipeline_get_position(m_Instance); }
        set { gub_pipeline_set_position(m_Instance, value); }
    }

    internal void SetupDecoding(string uri, bool playAllStreams, int video_index, int audio_index, string net_clock_address, int net_clock_port, ulong basetime, string audioGUID, float crop_left, float crop_top, float crop_right, float crop_bottom)
    {
        gub_pipeline_setup_decoding(m_Instance, uri, playAllStreams, video_index, audio_index, net_clock_address, net_clock_port, basetime, audioGUID, crop_left, crop_top, crop_right, crop_bottom);
    }

    internal bool GrabFrame(out Vector2 frameSize)
    {
        int w = 0, h = 0;
        if (gub_pipeline_grab_frame(m_Instance, ref w, ref h) == 1)
        {
            frameSize.x = w;
            frameSize.y = h;
            return true;
        }
        frameSize.x = frameSize.y = 0;
        return false;
    }

    internal void BlitTexture(System.IntPtr _NativeTexturePtr, int _TextureWidth, int _TextureHeight)
    {
        if (_NativeTexturePtr == System.IntPtr.Zero) return;

        gub_pipeline_blit_image(m_Instance, _NativeTexturePtr);
    }
    internal void SetTexture(IntPtr texture)
    {
        gub_set_texture(m_Instance, texture);
    }
    internal static bool IsReadyToRender(ref int width, ref int height)
    {
        return gub_pipeline_is_ready_to_render(ref width, ref height);
    }

    internal void SetupEncoding(string filename, int width, int height)
    {
        gub_pipeline_setup_encoding(m_Instance, filename, width, height);
    }

    internal void ConsumeImage(System.IntPtr ptr, int size)
    {
        gub_pipeline_consume_image(m_Instance, ptr, size);
    }

    internal void StopEncoding()
    {
        gub_pipeline_stop_encoding(m_Instance);
    }

    internal void SetVolume(double volume)
    {
        gub_pipeline_set_volume(m_Instance, volume);
    }

    internal void SetAdaptiveBitrateLimit(float bitrate_limit)
    {
        gub_pipeline_set_adaptive_bitrate_limit(m_Instance, bitrate_limit);
    }
}
