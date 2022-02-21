// Decompiled with JetBrains decompiler
// Type: CriAtomSource
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using UnityEngine;

[AddComponentMenu("CRIWARE/CRI Atom Source")]
public class CriAtomSource : MonoBehaviour
{
  //protected CriAtomEx3dSource source;
  private Vector3 lastPosition;
  private bool hasValidPosition;
  [SerializeField]
  private bool _playOnStart;
  [SerializeField]
  private string _cueName = "";
  [SerializeField]
  private string _cueSheet = "";
  [SerializeField]
  private bool _use3dPositioning = true;
  [SerializeField]
  private bool _loop;
  [SerializeField]
  private float _volume = 1f;
  [SerializeField]
  private float _pitch;
  [SerializeField]
  private bool _androidUseLowLatencyVoicePool;
  [SerializeField]
  private bool need_to_player_update_all = true;
    /*
  public CriAtomExPlayer player { protected set; get; }

  public bool playOnStart
  {
    get => this._playOnStart;
    set => this._playOnStart = value;
  }

  public string cueName
  {
    get => this._cueName;
    set => this._cueName = value;
  }

  public string cueSheet
  {
    get => this._cueSheet;
    set => this._cueSheet = value;
  }

  public bool use3dPositioning
  {
    set
    {
      this._use3dPositioning = value;
      if (this.player == null)
        return;
      this.player.Set3dSource(this.use3dPositioning ? this.source : (CriAtomEx3dSource) null);
      this.SetNeedToPlayerUpdateAll();
    }
    get => this._use3dPositioning;
  }

  public bool loop
  {
    set => this._loop = value;
    get => this._loop;
  }

  public float volume
  {
    set
    {
      this._volume = value;
      if (this.player == null)
        return;
      this.player.SetVolume(this._volume);
      this.SetNeedToPlayerUpdateAll();
    }
    get => this._volume;
  }

  public float pitch
  {
    set
    {
      this._pitch = value;
      if (this.player == null)
        return;
      this.player.SetPitch(this._pitch);
      this.SetNeedToPlayerUpdateAll();
    }
    get => this._pitch;
  }

  public float pan3dAngle
  {
    set
    {
      if (this.player == null)
        return;
      this.player.SetPan3dAngle(value);
      this.SetNeedToPlayerUpdateAll();
    }
    get => this.player == null ? 0.0f : this.player.GetParameterFloat32(CriAtomEx.Parameter.Pan3dAngle);
  }

  public float pan3dDistance
  {
    set
    {
      if (this.player == null)
        return;
      this.player.SetPan3dInteriorDistance(value);
      this.SetNeedToPlayerUpdateAll();
    }
    get => this.player == null ? 0.0f : this.player.GetParameterFloat32(CriAtomEx.Parameter.Pan3dDistance);
  }

  public int startTime
  {
    set
    {
      if (this.player == null)
        return;
      this.player.SetStartTime((long) value);
      this.SetNeedToPlayerUpdateAll();
    }
    get => this.player == null ? 0 : this.player.GetParameterSint32(CriAtomEx.Parameter.StartTime);
  }

  public long time => this.player == null ? 0L : this.player.GetTime();

  public CriAtomSource.Status status => this.player == null ? CriAtomSource.Status.Error : (CriAtomSource.Status) this.player.GetStatus();

  public bool attenuationDistanceSetting
  {
    set
    {
      if (this.source == null)
        return;
      this.source.SetAttenuationDistanceSetting(value);
      this.source.Update();
    }
    get => this.source != null && this.source.GetAttenuationDistanceSetting();
  }

  public bool androidUseLowLatencyVoicePool
  {
    get => this._androidUseLowLatencyVoicePool;
    set => this._androidUseLowLatencyVoicePool = value;
  }

  protected void SetNeedToPlayerUpdateAll() => this.need_to_player_update_all = true;

  protected virtual void InternalInitialize()
  {
    CriAtomPlugin.InitializeLibrary();
    this.player = new CriAtomExPlayer();
    this.source = new CriAtomEx3dSource();
  }

  protected virtual void InternalFinalize()
  {
    this.player.Dispose();
    this.player = (CriAtomExPlayer) null;
    this.source.Dispose();
    this.source = (CriAtomEx3dSource) null;
    CriAtomPlugin.FinalizeLibrary();
  }

  private void Awake() => this.InternalInitialize();

  private void OnEnable()
  {
    this.hasValidPosition = false;
    this.SetInitialParameters();
    this.SetNeedToPlayerUpdateAll();
  }

  private void OnDestroy() => this.InternalFinalize();

  protected bool SetInitialSourcePosition()
  {
    Vector3 position = this.transform.position;
    this.lastPosition = position;
    if (this.source == null)
      return false;
    this.source.SetPosition(position.x, position.y, position.z);
    this.source.Update();
    return true;
  }

  protected virtual void SetInitialParameters()
  {
    this.use3dPositioning = this.use3dPositioning;
    this.player.Set3dListener(CriAtomListener.sharedNativeListener);
    this.SetInitialSourcePosition();
    this.player.SetVolume(this._volume);
    this.player.SetPitch(this._pitch);
  }

  private void Start() => this.PlayOnStart();

  private void LateUpdate()
  {
    Vector3 position = this.transform.position;
    Vector3 vector3 = (position - this.lastPosition) / Time.deltaTime;
    this.lastPosition = position;
    this.source.SetPosition(position.x, position.y, position.z);
    if (this.hasValidPosition)
      this.source.SetVelocity(vector3.x, vector3.y, vector3.z);
    this.source.Update();
    this.hasValidPosition = true;
    if (!this.need_to_player_update_all)
      return;
    this.player.UpdateAll();
    this.need_to_player_update_all = false;
  }

  public void OnDrawGizmos()
  {
    if (PABCCELMCAJ.BNDCGKDLADN && this.status == CriAtomSource.Status.Playing)
      Gizmos.DrawIcon(this.transform.position, "Criware/VoiceOn.png");
    else
      Gizmos.DrawIcon(this.transform.position, "Criware/VoiceOff.png");
  }

  public CriAtomExPlayback Play() => this.Play(this.cueName);

  public CriAtomExPlayback Play(string cueName)
  {
    if (this.player == null)
      return new CriAtomExPlayback(uint.MaxValue);
    CriAtomExAcb acb = (CriAtomExAcb) null;
    if (!string.IsNullOrEmpty(this.cueSheet))
      acb = CriAtom.GetAcb(this.cueSheet);
    this.player.SetCue(acb, cueName);
    if (!this.hasValidPosition)
    {
      this.SetInitialSourcePosition();
      this.hasValidPosition = true;
    }
    if (this.status == CriAtomSource.Status.Stop)
      this.player.Loop(this._loop);
    return this.player.Start();
  }

  public CriAtomExPlayback Play(int cueId)
  {
    if (this.player == null)
      return new CriAtomExPlayback(uint.MaxValue);
    CriAtomExAcb acb = (CriAtomExAcb) null;
    if (!string.IsNullOrEmpty(this.cueSheet))
      acb = CriAtom.GetAcb(this.cueSheet);
    this.player.SetCue(acb, cueId);
    if (!this.hasValidPosition)
    {
      this.SetInitialSourcePosition();
      this.hasValidPosition = true;
    }
    if (this.status == CriAtomSource.Status.Stop)
      this.player.Loop(this._loop);
    return this.player.Start();
  }

  private void PlayOnStart()
  {
    if (!this.playOnStart || string.IsNullOrEmpty(this.cueName))
      return;
    this.StartCoroutine(this.PlayAsync(this.cueName));
  }

  private IEnumerator PlayAsync(string cueName)
  {
    CriAtomExAcb acb = (CriAtomExAcb) null;
    while (acb == null && !string.IsNullOrEmpty(this.cueSheet))
    {
      acb = CriAtom.GetAcb(this.cueSheet);
      if (acb == null)
        yield return (object) null;
    }
    this.player.SetCue(acb, cueName);
    if (!this.hasValidPosition)
    {
      this.SetInitialSourcePosition();
      this.hasValidPosition = true;
    }
    if (this.status == CriAtomSource.Status.Stop)
      this.player.Loop(this._loop);
    this.player.Start();
  }

  public void Stop()
  {
    if (this.player == null)
      return;
    this.player.Stop();
  }

  public void Pause(bool sw)
  {
    if (this.player == null)
      return;
    if (!sw)
      this.player.Resume(CriAtomEx.ResumeMode.PausedPlayback);
    else
      this.player.Pause();
  }

  public bool IsPaused() => this.player != null && this.player.IsPaused();

  public void SetBusSendLevel(string busName, float level)
  {
    if (this.player == null)
      return;
    this.player.SetBusSendLevel(busName, level);
    this.SetNeedToPlayerUpdateAll();
  }

  [Obsolete("Use CriAtomSource.SetBusSendLevel(string, float)")]
  public void SetBusSendLevel(int busId, float level)
  {
    if (this.player == null)
      return;
    this.player.SetBusSendLevel(busId, level);
    this.SetNeedToPlayerUpdateAll();
  }

  public void SetBusSendLevelOffset(string busName, float levelOffset)
  {
    if (this.player == null)
      return;
    this.player.SetBusSendLevelOffset(busName, levelOffset);
    this.SetNeedToPlayerUpdateAll();
  }

  [Obsolete("Use CriAtomSource.SetBusSendLevelOffset(string, float)")]
  public void SetBusSendLevelOffset(int busId, float levelOffset)
  {
    if (this.player == null)
      return;
    this.player.SetBusSendLevelOffset(busId, levelOffset);
    this.SetNeedToPlayerUpdateAll();
  }

  public void SetAisacControl(string controlName, float value)
  {
    if (this.player == null)
      return;
    this.player.SetAisacControl(controlName, value);
    this.SetNeedToPlayerUpdateAll();
  }

  [Obsolete("Use CriAtomSource.SetAisacControl")]
  public void SetAisac(string controlName, float value) => this.SetAisacControl(controlName, value);

  public void SetAisacControl(uint controlId, float value)
  {
    if (this.player == null)
      return;
    this.player.SetAisacControl(controlId, value);
    this.SetNeedToPlayerUpdateAll();
  }

  [Obsolete("Use CriAtomSource.SetAisacControl")]
  public void SetAisac(uint controlId, float value) => this.SetAisacControl(controlId, value);

  public void AttachToAnalyzer(CriAtomExOutputAnalyzer analyzer)
  {
    if (this.player == null)
      return;
    analyzer.AttachExPlayer(this.player);
  }

  public void DetachFromAnalyzer(CriAtomExOutputAnalyzer analyzer) => analyzer.DetachExPlayer();
    */
  public enum Status
  {
    Stop,
    Prep,
    Playing,
    PlayEnd,
    Error,
  }
}
