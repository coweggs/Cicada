using NAudio.CoreAudioApi;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Cicada.Services
{
    internal class AudioManager
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        private readonly FlyoutManager FlyoutMan;
        private readonly MMDeviceEnumerator Enumerator;

        public AudioManager(FlyoutManager flyoutManager)
        {
            Enumerator = new MMDeviceEnumerator();
            FlyoutMan = flyoutManager;
        }

        private AudioSessionControl? GetSessionFromPID(uint pId)
        {
            Process process = Process.GetProcessById((int)pId);

            MMDevice? device = Enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            SessionCollection sessions = device.AudioSessionManager.Sessions;
            Debug.WriteLine(process);

            // CASE1: Foreground pID is an audio session.
            for (int i = 0; i < sessions.Count; i++)
            {
                //Debug.WriteLine(Process.GetProcessById((int)sessions[i].GetProcessID).ProcessName + " " + sessions[i].GetProcessID);
                if (sessions[i].GetProcessID == pId)
                {
                    return sessions[i];
                }
            }

            // CASE2: Foreground pID has same process name as audio session.
            string? processName = process.ProcessName;
            for (int i = 0; i < sessions.Count; i++)
            {
                try
                {
                    Process sessionProcess =
                        Process.GetProcessById((int)sessions[i].GetProcessID);
                    if (processName == sessionProcess.ProcessName)
                    {
                        Debug.WriteLine("Found matching process names.");
                        return sessions[i];
                    }
                }
                catch
                {
                    // process exited between GetProcessById and now
                    continue;
                }
            }

            return null;
        }

        private uint GetActivePID()
        {
            IntPtr handle = GetForegroundWindow();
            GetWindowThreadProcessId(handle, out uint pId);
            return pId;
        }

        private AudioSessionControl? GetActiveSession()
        {
            uint pId = GetActivePID();
            AudioSessionControl? session = GetSessionFromPID(pId);
            if (session != null)
            {
                Debug.WriteLine("Session: " + Process.GetProcessById((int)pId).ProcessName);
            }
            else
            {
                Debug.WriteLine("No session found for: " + Process.GetProcessById((int)pId).ProcessName + " " + pId);
                FlyoutMan.ShowNoSessionFlyout();
            }
            return session;
        }

        public void IncreaseVolume(float increment)
        {
            AudioSessionControl? session = GetActiveSession();
            if (session != null)
            {
                session.SimpleAudioVolume.Volume += increment;
                FlyoutMan.ShowFlyout(session.SimpleAudioVolume.Volume, false, GetActivePID());
            }
        }

        public void DecreaseVolume(float increment)
        {
            AudioSessionControl? session = GetActiveSession();
            if (session != null)
            {
                session.SimpleAudioVolume.Volume -= increment;
                FlyoutMan.ShowFlyout(session.SimpleAudioVolume.Volume, false, GetActivePID());
            }
        }

        public void ToggleSessionMute()
        {
            AudioSessionControl? session = GetActiveSession();
            if (session != null)
            {
                session.SimpleAudioVolume.Mute = !session.SimpleAudioVolume.Mute;
                FlyoutMan.ShowFlyout(session.SimpleAudioVolume.Volume,
                    session.SimpleAudioVolume.Mute, GetActivePID());
            }
        }

        public void IsolateSession()
        {
            AudioSessionControl? session = GetActiveSession();
            if (session != null)
            {
                bool allMuted = true;
                MMDevice? device = Enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
                SessionCollection sessions = device.AudioSessionManager.Sessions;

                // mute all other sessions
                for (int i = 0; i < sessions.Count; i++)
                {
                    if (sessions[i] != session)
                    {
                        allMuted = allMuted && sessions[i].SimpleAudioVolume.Mute;
                        if (!allMuted)
                        {
                            break;
                        }
                    }
                }
                session.SimpleAudioVolume.Mute = false;
                for (int i = 0; i < sessions.Count; i++)
                {
                    if (sessions[i] != session)
                    {
                        sessions[i].SimpleAudioVolume.Mute = !allMuted;
                    }
                }

                FlyoutMan.ShowIsolateFlyout(allMuted, GetActivePID());
            }
        }
    }
}
