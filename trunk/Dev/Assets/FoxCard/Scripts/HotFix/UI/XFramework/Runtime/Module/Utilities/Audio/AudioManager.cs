using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace XFramework
{
    /// <summary>
    /// 音频类型
    /// </summary>
    public enum AudioType
    {
        /// <summary>
        /// 背景音乐
        /// </summary>
        BGM,

        /// <summary>
        /// 音效
        /// </summary>
        SFX,
    }

    public sealed class AudioManager : CommonObject
    {
        /// <summary>
        /// bgm音量 
        /// </summary>
        private const string BGMValue = "BGMVALUE";

        /// <summary>
        /// bgm静音
        /// </summary>
        private const string BGMMUTE = "BGMMUTE";

        /// <summary>
        /// sfx音量
        /// </summary>
        private const string SFXValue = "SFXVALUE";

        /// <summary>
        /// sfx静音
        /// </summary>
        private const string SFXMUTE = "SFXMUTE";

        public static AudioManager _instance;

        public static AudioManager Instance => _instance;

        private Dictionary<AudioType, AudioSource> dictAudio = new Dictionary<AudioType, AudioSource>();

        /// <summary>
        /// 音频类型 -> 音频Clip的key
        /// </summary>
        private Dictionary<AudioType, string> dictClipKeys = new Dictionary<AudioType, string>();

        public static List<string> banks { get; } = new List<string>()
        {
            "Master.bank",
            "Master.strings.bank",
            "BGM.bank",
            "BGM.strings.bank",
            "SoundEffects.bank",
            "SoundEffects.strings.bank",
        };

        protected override void Init()
        {
            base.Init();

            _instance = this;


            // var global = Common.Instance.Get<Global>();
            // var gameRoot = global.GameRoot;
            //
            // var bgm = gameRoot.gameObject.AddComponent<AudioSource>();
            // var sfx = gameRoot.gameObject.AddComponent<AudioSource>();
            //
            // bgm.loop = true;
            // bgm.playOnAwake = false;
            // sfx.loop = false;
            // sfx.playOnAwake = false;
            //
            // if (PlayerPrefsHelper.TryGetFloat(BGMValue, out float bgmVolume)) 
            //     bgm.volume = bgmVolume;
            //
            // if (PlayerPrefsHelper.TryGetBool(BGMMUTE, out bool bgmMute))
            //     bgm.mute = bgmMute;
            //
            // if (PlayerPrefsHelper.TryGetFloat(SFXValue, out float sfxVolume))
            //     sfx.volume = sfxVolume;
            //
            // if (PlayerPrefsHelper.TryGetBool(SFXMUTE, out bool sfxMute))
            //     sfx.mute = sfxMute;
            //

            // dictAudio.Add(AudioType.BGM, bgm);
            // dictAudio.Add(AudioType.SFX, sfx);
            //LoadAudio().Forget();
        }

        async UniTaskVoid LoadAudio()
        {
            foreach (var bank in banks)
            {
                var textAsset = await ResourcesManager.LoadAssetAsync<TextAsset>(bank);
                //RuntimeManager.LoadBank(textAsset, false);
            }

            // RuntimeManager.

            // if (RuntimeManager.HasBankLoaded("BGM"))
            // {
            // RuntimeManager.StudioSystem.getBankList(out var banks);
            // foreach (var VARIABLE in banks)
            // {
            //     if (VARIABLE.isValid())
            //     {
            //         VARIABLE.getPath(out var path);
            //         Log.Error($"{path}");
            //     }
            // }
            // }

            //RuntimeManager.AnyBankLoading()
            //Master.
            //RuntimeManager.ge
            // if (!RuntimeManager.IsInitialized)
            // {
            //     Log.Error($"!IsInitialized");
            //     return;
            // }

            // List<string> clipsList = new List<string>();
            // //clipsList.Add("NormalCollideSound");
            // clipsList.Add("PickGemSound");
            // RuntimeManager.GetEventDescription("event:/PickGemSound");
            //
            //
            // foreach (var VARIABLE in clipsList)
            // {
            //     var newstr = $"event:/{VARIABLE}";
            // }
        }

        protected override void Destroy()
        {
            base.Destroy();
            foreach (var audioSource in dictAudio.Values)
            {
                GameObject.DestroyImmediate(audioSource, true);
            }

            dictAudio.Clear();
            dictClipKeys.Clear();

            _instance = null;
        }

        public bool TryGetAudioSource(AudioType audioType, out AudioSource audioSource)
        {
            return dictAudio.TryGetValue(audioType, out audioSource);
        }

        /// <summary>
        /// 获取音量
        /// </summary>
        /// <param name="audioType"></param>
        /// <returns></returns>
        public float GetVolume(AudioType audioType)
        {
            if (TryGetAudioSource(audioType, out var source))
                return source.volume;

            return 0;
        }

        /// <summary>
        /// 获取是否静音
        /// </summary>
        /// <param name="audioType"></param>
        /// <returns></returns>
        public bool GetMute(AudioType audioType)
        {
            if (TryGetAudioSource(audioType, out var source))
                return source.mute;

            return true;
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="audioType"></param>
        public void Stop(AudioType audioType)
        {
            if (TryGetAudioSource(audioType, out var source))
                source.Stop();
        }

        /// <summary>
        /// 设置音量
        /// </summary>
        /// <param name="audioType"></param>
        /// <param name="volume"></param>
        public void SetVolume(AudioType audioType, float volume)
        {
            if (TryGetAudioSource(audioType, out var source))
            {
                source.volume = volume;
                switch (audioType)
                {
                    case AudioType.BGM:
                        PlayerPrefsHelper.SetFloat(BGMValue, volume, true);
                        break;
                    case AudioType.SFX:
                        PlayerPrefsHelper.SetFloat(SFXMUTE, volume, true);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 设置静音
        /// </summary>
        /// <param name="audioType"></param>
        /// <param name="mute"></param>
        public void SetMute(AudioType audioType, bool mute)
        {
            if (TryGetAudioSource(audioType, out var source))
            {
                source.mute = mute;
                switch (audioType)
                {
                    case AudioType.BGM:
                        //PlayerPrefsHelper.SetBool(BGMMUTE, mute, true);
                        break;
                    case AudioType.SFX:
                        //PlayerPrefsHelper.SetBool(SFXMUTE, mute, true);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 设置循环播放
        /// </summary>
        /// <param name="audioType"></param>
        /// <param name="loop"></param>
        public void SetLoop(AudioType audioType, bool loop)
        {
            if (TryGetAudioSource(audioType, out var source))
            {
                source.loop = loop;
            }
        }

        /// <summary>
        /// bgm音量
        /// </summary>
        /// <returns></returns>
        public float GetBgmVolume()
        {
            return GetVolume(AudioType.BGM);
        }

        /// <summary>
        /// sfx音量
        /// </summary>
        /// <returns></returns>
        public float GetSFXVolume()
        {
            return GetVolume(AudioType.SFX);
        }

        /// <summary>
        /// bgm静音
        /// </summary>
        /// <returns></returns>
        public bool GetBgmMute()
        {
            return GetMute(AudioType.BGM);
        }

        /// <summary>
        /// sfx静音
        /// </summary>
        /// <returns></returns>
        public bool GetSFXMute()
        {
            return GetMute(AudioType.SFX);
        }

        /// <summary>
        /// 停止bgm
        /// </summary>
        public void StopBgm()
        {
            Stop(AudioType.BGM);
        }

        /// <summary>
        /// 停止sfx
        /// </summary>
        public void StopSFX()
        {
            Stop(AudioType.SFX);
        }

        /// <summary>
        /// 设置bgm音量
        /// </summary>
        /// <param name="volume"></param>
        public void SetBgmVolume(float volume)
        {
            this.SetVolume(AudioType.BGM, volume);
        }

        /// <summary>
        /// 设置bgm静音
        /// </summary>
        /// <param name="mute"></param>
        public void SetBgmMute(bool mute)
        {
            //RuntimeManager.StudioSystem.getBus("bus:/BGM", out var bus);


            //bus.setMute(mute);


            //this.SetMute(AudioType.BGM, mute);
        }

        /// <summary>
        /// 设置bgm循环播放
        /// </summary>
        /// <param name="loop"></param>
        public void SetBgmLoop(bool loop)
        {
            this.SetLoop(AudioType.BGM, loop);
        }

        /// <summary>
        /// 设置sfx音量
        /// </summary>
        /// <param name="volume"></param>
        public void SetSFXVolume(float volume)
        {
            this.SetVolume(AudioType.SFX, volume);
        }

        /// <summary>
        /// 设置sfx静音
        /// </summary>
        /// <param name="mute"></param>
        public void SetSFXMute(bool mute)
        {
            //RuntimeManager.StudioSystem.getBus("bus:/SoundEffects", out var bus);


            //bus.setMute(mute);
            //this.SetMute(AudioType.SFX, mute);
        }

        /// <summary>
        /// 设置sfx循环播放
        /// </summary>
        /// <param name="loop"></param>
        public void SetSFXLoop(bool loop)
        {
            this.SetLoop(AudioType.SFX, loop);
        }


        /// <summary>
        /// 播放音频
        /// </summary>
        /// <param name="audioType"></param>
        /// <param name="audioKey"></param>
        /// <param name="ignoreIfPlaying">如果正在播放音频，则忽略这一次播放</param>
        public async void PlayAudio(AudioType audioType, string audioKey, bool ignoreIfPlaying)
        {
            if (TryGetAudioSource(audioType, out var source))
            {
                if (source.isPlaying && ignoreIfPlaying)
                    return;

                source.Stop();

                if (dictClipKeys.TryGetValue(audioType, out var key))
                {
                    if (key == audioKey)
                    {
                        source.Play();
                        return;
                    }

                    if (source.clip != null)
                    {
                        ResourcesManager.ReleaseAsset(source.clip);
                        source.clip = null;
                    }
                }

                source.clip = await ResourcesManager.LoadAssetAsync<AudioClip>(this, audioKey);
                source.Play();
                dictClipKeys[audioType] = audioKey;
            }
        }

        /// <summary>
        /// 播放音频
        /// </summary>
        /// <param name="audioType"></param>
        /// <param name="audioKey"></param>
        public void PlayAudio(AudioType audioType, string audioKey)
        {
            PlayAudio(audioType, audioKey, false);
        }

        /// <summary>
        /// 播放BGM
        /// </summary>
        /// <param name="audioKey"></param>
        /// <param name="ignoreIfPlaying">如果正在播放音频，则忽略这一次播放</param>
        public void PlayBgmAudio(string audioKey, bool ignoreIfPlaying)
        {
            PlayAudio(AudioType.BGM, audioKey, ignoreIfPlaying);
        }

        /// <summary>
        /// 播放BGM
        /// </summary>
        /// <param name="audioKey"></param>
        /// <param name="ignoreIfPlaying">如果正在播放音频，则忽略这一次播放</param>
        public void PlayFModAudio(string audioKey)
        {
            //var audio = RuntimeManager.GetEventDescription($"event:/{audioKey}");

            //audio.createInstance(out var instance);

            //instance.start();
            //instance.release();
        }

        /// <summary>
        /// 播放BGM
        /// </summary>
        /// <param name="audioKey"></param>
        public void PlayBgmAudio(string audioKey)
        {
            PlayAudio(AudioType.BGM, audioKey);
        }

        /// <summary>
        /// 播放SFX
        /// </summary>
        /// <param name="audioKey"></param>
        /// <param name="ignoreIfPlaying">如果正在播放音频，则忽略这一次播放</param>
        public void PlaySFXAudio(string audioKey, bool ignoreIfPlaying)
        {
            PlayAudio(AudioType.SFX, audioKey, ignoreIfPlaying);
        }

        /// <summary>
        /// 播放SFX
        /// </summary>
        /// <param name="audioKey"></param>
        public void PlaySFXAudio(string audioKey)
        {
            PlayAudio(AudioType.SFX, audioKey);
        }
    }
}