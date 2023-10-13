using Mykey;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKeyForm;

internal class Audio
{
    private WaveOutEvent startDevice = new();
    private WaveOutEvent stopDevice = new();
    private AudioFileReader startAudio;
    private AudioFileReader stopAudio;

    public Audio()
    {
        var startPath = GetFileWith("start");
        var stopPath = GetFileWith("stop");
        if (startPath != "")
        {
            startAudio = new AudioFileReader(startPath);
            startDevice.Init(startAudio);
        }
        if (stopPath != "")
        {
            stopAudio = new AudioFileReader(stopPath);
            stopDevice.Init(stopAudio);
        }
    }

    public void PlayStart()
    {
        if (!Config.Instance.PlayAudio) return;
        if (startAudio != null)
        {
            startAudio.Position = 0;
            startDevice.Play();
        }
        else
        {
            startDevice.Init(new SignalGenerator()
            {
                Gain = 0.2,
                Frequency = 1000,
                Type = SignalGeneratorType.Sin
            }
            .Take(TimeSpan.FromSeconds(0.1)));
            startDevice.Play();
        }
    }

    public void PlayStop()
    {
        if (!Config.Instance.PlayAudio) return;
        if (stopAudio != null)
        {
            stopAudio.Position = 0;
            stopDevice.Play();
        }
        else
        {
            stopDevice.Init(new SignalGenerator()
            {
                Gain = 0.2,
                Frequency = 700,
                Type = SignalGeneratorType.Sin
            }
            .Take(TimeSpan.FromSeconds(0.1)));
            stopDevice.Play();
        }
    }

    string GetFileWith(string filename)
    {
        var files = Directory.GetFiles(".");
        foreach (var file in files)
        {
            if (Path.GetFileNameWithoutExtension(file) == filename)
            {
                return file;
            }
        }
        return "";
    }
}
