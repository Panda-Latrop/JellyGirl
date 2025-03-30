using UnityEngine.Audio;

namespace UnityEngine.MusicSystem
{
    public class MusicHolder
    {
        private AudioSource[] sources = new AudioSource[2];
        private AudioMixer audioMixer;
        private int activeSources;

        public bool IsPlaying => Active.isPlaying && Active.enabled;
        public AudioSource Active => sources[activeSources];
        public AudioSource Next => sources[(activeSources + 1 + sources.Length) % sources.Length];
        public AudioSource Previous => sources[(activeSources + 1 + sources.Length) % sources.Length];

        public MusicHolder(GameObject root)
        {
            Initialization(root);
        }
        private void Initialization(GameObject root)
        {
            audioMixer = Resources.Load<AudioMixer>(Utility.resourcesAudioMixerPath);
            for (int i = 0; i < sources.Length; i++)
                sources[i] = CreateAudioSource(root);
            activeSources = 0;
        }
        private AudioSource CreateAudioSource(GameObject root)
        {
            AudioSource source = root.AddComponent<AudioSource>();
            source.spatialBlend = 0.0f;
            source.volume = 0.0f;
            source.playOnAwake = false;
            source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Music")[0];
            source.enabled = false;
            return source;
        }
        public AudioSource Increase()
        {
            activeSources = (activeSources + 1 + sources.Length) % sources.Length;
            return sources[activeSources];
        }
        public AudioSource Decrease()
        {
            activeSources = (activeSources - 1 + sources.Length) % sources.Length;
            return sources[activeSources];
        }
        public void Stop()
        {
            for (int i = 0; i < sources.Length; i++)
                sources[i].Stop();
        }
        public void Pause()
        {
            for (int i = 0; i < sources.Length; i++)
                sources[i].Pause();
        }
        public void Resume()
        {
            for (int i = 0; i < sources.Length; i++)
                sources[i].UnPause();
        }
        public void Play(AudioClip music, float volume = 1f, bool isLoop = true)
        {
            Play(Active, music, volume, isLoop);
        }
        public void Play(AudioSource source, AudioClip music, float volume = 1f, bool isLoop = true)
        {
            source.Stop();
            source.enabled = true;
            Set(source, music, volume, isLoop);
            source.Play();
        }
        public void Set(AudioClip music, float volume = 1f, bool isLoop = true)
        {
            Set(Active, music, volume, isLoop);
        }
        public void Set(AudioSource source, AudioClip music, float volume = 1f, bool isLoop = true)
        {
            source.volume = volume;
            source.clip = music;
            source.loop = isLoop;
            source.time = 0;
        }
    }
    public class MusicCrossFade
    {
        private MusicHolder holder;
        private bool inAction;
        private float fadeTime;
        private float currentTime;

        public bool InAction => inAction;
        public MusicCrossFade(MusicHolder holder)
        {
            this.holder = holder;
        }
        public void Start(AudioClip music, float fadeTime = 1.0f, bool isLoop = true, bool keepTime = false)
        {
            inAction = true;
            AudioSource previous = holder.Active;
            holder.Increase();
            holder.Play(music, 0f, isLoop);
            if (keepTime)
                holder.Active.time = previous.time;
            this.fadeTime = fadeTime;
            currentTime = 0f;
        }
        private void Evaluate(float percent)
        {
            AudioSource main = holder.Active;
            AudioSource sub = holder.Previous;
            main.volume = Mathf.Lerp(0f, 1.0f, percent);
            sub.volume = Mathf.Lerp(1.0f, 0f, percent);
        }
        private void OnFinish()
        {
            AudioSource sub = holder.Previous;
            sub.Stop();
            sub.enabled = false;
            inAction = false;
        }
        public bool Update()
        {
            if (inAction)
            {
                float percent = currentTime / fadeTime;
                Evaluate(percent);
                if (currentTime >= fadeTime)
                {
                    OnFinish();
                    return true;
                }
                currentTime += Time.deltaTime;
            }
            return false;
        }
    }
    public class MusicTransaction
    {
        [System.Flags]
        private enum ActionTypeEnum
        {
            fade = 1,
            rise = 2,
        }

        private MusicHolder holder;
        private ActionTypeEnum actionType;
        private bool inAction;
        private float fadeTime;
        private float riseTime;
        private float currentTime;
        private bool inRise;
        public bool InAction => inAction;
        public MusicTransaction(MusicHolder holder)
        {
            this.holder = holder;
        }
        public void Change(AudioClip music, float fadeTime = 1.0f, float riseTime = 1.0f, bool isLoop = true, bool keepTime = false)
        {
            inAction = true;
            holder.Set(holder.Next, music, 0f, isLoop);
            this.fadeTime = fadeTime;
            this.riseTime = riseTime;
            if (keepTime)
                holder.Next.time = holder.Active.time;
            currentTime = 0f;
            actionType = ActionTypeEnum.fade | ActionTypeEnum.rise;
        }
        public void Fade(float fadeTime = 1.0f)
        {
            inAction = true;
            inRise = false;
            this.fadeTime = fadeTime;
            currentTime = 0f;
            actionType = ActionTypeEnum.fade;

        }
        public void Rise(AudioClip music, float riseTime = 1.0f, bool isLoop = true)
        {
            inAction = true;
            inRise = true;
            holder.Play(music, 0f, isLoop);
            this.riseTime = riseTime;
            currentTime = 0f;
            actionType = ActionTypeEnum.rise;

        }
        private void Evaluate(float percent)
        {
            holder.Active.volume = Mathf.Lerp(0f, 1.0f, percent);
        }
        private void OnFinish()
        {
            if (actionType == (ActionTypeEnum.fade | ActionTypeEnum.rise))
            {
                inRise = true;
                AudioSource previous = holder.Active;
                holder.Increase();
                holder.Active.enabled = true;
                holder.Active.Play();
                previous.Stop();
                previous.enabled = false;
            }
            else
            {
                if (actionType == ActionTypeEnum.fade)
                {
                    holder.Active.Stop();
                }
                inRise = false;
                inAction = false;
            }
            currentTime = 0f;
        }
        public bool Update()
        {
            if (inAction)
            {
                float duration = inRise ? riseTime : fadeTime;
                float percent = inRise ? currentTime / duration : 1f - (currentTime / duration);
                Evaluate(percent);
                if (currentTime >= duration)
                {
                    OnFinish();
                    return true;
                }
                currentTime += Time.deltaTime;
            }
            return false;
        }
    }
    public class MusicSystem : MonoBehaviour
    {
        private MusicHolder holder;
        private MusicCrossFade crossFade;
        private MusicTransaction transaction;
        private string specifer;
        public string Specigfer { get => specifer; set => specifer = value; }
        public bool IsPlaying => holder.IsPlaying;
        public bool InCrossfade => crossFade.InAction;
        public float Time => holder.Active.time;
        public float Duration
        {
            get
            {
                if (holder.Active.isPlaying)
                    return holder.Active.clip.length;
                else return -1f;
            }
        }
        public bool CheckPlaying(AudioClip music)
        {
            if (holder.IsPlaying)
            {
                return holder.Active.clip == music;
            }
            return false;
        }
        public void Initialization()
        {
            holder = new MusicHolder(gameObject);
            crossFade = new MusicCrossFade(holder);
            transaction = new MusicTransaction(holder);
        }
        public void Stop() => holder.Stop();
        public void Pause() => holder.Pause();
        public void Resume() => holder.Resume();
        public void Play(AudioClip music, float volume = 1f, bool isLoop = true) => holder.Play(music, volume, isLoop);
        public void Change(AudioClip music, float fadeTime = 1.0f, float riseTime = 1.0f, bool isLoop = true, bool keepTime = false)
        {
            if (fadeTime <= 0f || !holder.IsPlaying)
            {
                if (riseTime <= 0f)
                {
                    holder.Play(music, 1f, isLoop);
                }
                else
                {
                    transaction.Rise(music, riseTime, isLoop);
                }
            }
            else
            {
                transaction.Change(music, fadeTime, riseTime, isLoop, keepTime);
                enabled = true;
            }
        }
        public void Fade(float fadeTime = 1.0f)
        {
            if (fadeTime <= 0f)
                holder.Stop();
            else
            {
                transaction.Fade(fadeTime);
                enabled = true;
            }
        }
        public void Rise(AudioClip music, float riseTime = 1.0f, bool isLoop = true)
        {
            if (riseTime <= 0f)
                holder.Play(music, 1f, isLoop);
            else
            {
                transaction.Rise(music, riseTime, isLoop);
                enabled = true;
            }
        }
        public void CrossFade(AudioClip music, float fadeTime = 1.0f, bool isLoop = true, bool keepTime = false)
        {
            if (fadeTime <= 0f || !holder.IsPlaying)
                holder.Play(music, 1f, isLoop);
            else
            {
                crossFade.Start(music, fadeTime, isLoop, keepTime);
                enabled = true;
            }
        }
        private void LateUpdate()
        {
            if (crossFade.InAction || transaction.InAction)
            {
                if (crossFade.InAction)
                    crossFade.Update();
                if (transaction.InAction)
                    transaction.Update();
            }
            else
                enabled = false;
        }
    }
}