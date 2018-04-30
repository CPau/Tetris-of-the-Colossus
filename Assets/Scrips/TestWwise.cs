using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWwise : MonoBehaviour {
    public AK.Wwise.Event AudioInputEvent;
    public uint SampleRate = 48000;
    public uint NumberOfChannels = 1;
    public uint SampleIndex = 0;
    public uint Frequency = 880;
    private bool IsPlaying = true;

    // Callback that fills audio samples - This function is called each frame for every channel.
    bool AudioSamplesDelegate(uint playingID, uint channelIndex, float[] samples)
    {
        for (uint i = 0; i < samples.Length; ++i)
            samples[i] = UnityEngine.Mathf.Sin(Frequency * 2 * UnityEngine.Mathf.PI * (i + SampleIndex) / SampleRate);

        if (channelIndex == NumberOfChannels - 1)
            SampleIndex = (uint)(SampleIndex + samples.Length) % SampleRate;

        // Return false to indicate that there is no more data to provide. This will also stop the associated event.
        return IsPlaying;
    }

    // Callback that sets the audio format - This function is called once before samples are requested.
    void AudioFormatDelegate(uint playingID, AkAudioFormat audioFormat)
    {
        // Channel configuration and sample rate are the main parameters that need to be set.
        audioFormat.channelConfig.uNumChannels = NumberOfChannels;
        audioFormat.uSampleRate = SampleRate;
    }

    private void Start()
    {
        // The AudioInputEvent event, that is setup within Wwise to use the Audio Input plug-in, is posted on gameObject.
        // AudioFormatDelegate is called once, and AudioSamplesDelegate is called once per frame until it returns false.
        AkAudioInputManager.PostAudioInputEvent(AudioInputEvent, gameObject, AudioSamplesDelegate, AudioFormatDelegate);
    }

    // This method can be called by other scripts to stop the callback
    public void StopSound()
    {
        IsPlaying = false;
    }

    private void OnDestroy()
    {
        AudioInputEvent.Stop(gameObject);
    }
}