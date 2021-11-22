using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LostMind.Engine.Audio {
    enum PlayerState {
        Playing = 1,
        Paused,// 2
        Stopped,//3
        Standby// 4
    }
    class Player {
        WaveOutEvent audioDevice;
        AudioFileReader fileReader;
        PlayerState state = PlayerState.Standby;

        public static Player PlayNow(string filename) {
            var reader = new AudioFileReader(filename);
            var outDevice = new WaveOutEvent();
            outDevice.Init(reader);
            outDevice.Play();
            Player assembling = new Player();
            assembling.fileReader = reader;
            assembling.audioDevice = outDevice;
            assembling.state = PlayerState.Playing;
            assembling.subscribeEvents();
            return assembling;
        }

        void onPlaybackStop(object sender, StoppedEventArgs e) {
            _ = fileReader.DisposeAsync();
            _ = Task.Run(() => { audioDevice.Dispose(); });
            state = PlayerState.Stopped;
        }
        void subscribeEvents() {
            audioDevice.PlaybackStopped += onPlaybackStop;
        }

        public void StopAudio() {
            audioDevice.Stop();
            _ = fileReader.DisposeAsync();
            _ = Task.Run(() => { audioDevice.Dispose(); });
            state = PlayerState.Stopped;
        }
        public void Play(string filename) {
            //audioDevice.Init()
            if (state == PlayerState.Paused | state == PlayerState.Playing) {
                throw new Exception("You cannot play a new audio file in the same player while playing or paused.\nStop the playback in the first place.");
            }
            audioDevice = new WaveOutEvent();
            fileReader = new AudioFileReader(filename);
            audioDevice.Play();
            audioDevice.PlaybackStopped += onPlaybackStop;
            state = PlayerState.Playing;
        }
    }
}
