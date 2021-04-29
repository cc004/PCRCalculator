// Decompiled with JetBrains decompiler
// Type: Elements.PrincessFormProcessor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using Elements.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
  public class PrincessFormProcessor
  {
    private UnitCtrl owner;
    private UnitActionController unitActionController;
    //private MovieManager movieManager;
    private BattleManager battleManager;
    private BattleSpeedInterface battleTimeScale;
    private eMovieType currentMovieType = eMovieType.PRINCESS_FORM_SKILL_NORMAL;
    private long currentMovieId;
    private long currentMovieIndex;
    private Dictionary<int, Dictionary<int, PrincessFormProcessor.MovieVoiceData>> MovieVoiceDataDictionary = new Dictionary<int, Dictionary<int, PrincessFormProcessor.MovieVoiceData>>();
    private bool forceNormalSD;

    public void Initialize(
      UnitCtrl _unitCtrl,
      UnitActionController _unitActionController,
      BattleManager _battleManager,
      BattleSpeedInterface _battleTimeScale)
    {
      //this.movieManager = ManagerSingleton<MovieManager>.Instance;
      this.battleManager = _battleManager;
      this.owner = _unitCtrl;
      this.unitActionController = _unitActionController;
      this.battleTimeScale = _battleTimeScale;
      //this.forceNormalSD = _battleManager.GetForceNormalSD();
    }

    public void StartPrincessFormSkill(
      List<PrincessSkillMovieData> _princessSkillMovieDatas)
    {
      int unionBurstSkillId = this.owner.UnionBurstSkillId;
      eSpineCharacterAnimeId spineCharacterAnimeId = this.unitActionController.GetSkillMotionType(unionBurstSkillId) == SkillMotionType.EVOLUTION ? eSpineCharacterAnimeId.PRINCESS_SKILL_EVOLUTION : eSpineCharacterAnimeId.PRINCESS_SKILL;
      this.unitActionController.StartAction(unionBurstSkillId);
      int current = 0;
      System.Action func = (System.Action) null;
      func = (System.Action) (() =>
      {
        ++current;
        if (current > _princessSkillMovieDatas.Count)
        {
          this.finishSkill();
        }
        else
        {
          this.battleManager.IsPlayingPrincessMovie = true;
          if (current == 1)
            this.battleManager.CAOHLDNADPB = true;
          this.playPrincessFormMovie(current, _princessSkillMovieDatas, (System.Action) (() =>
          {
            this.battleManager.IsPlayingPrincessMovie = false;
            if (!this.owner.GetCurrentSpineCtrl().IsAnimation(spineCharacterAnimeId, current, _index3: this.owner.MotionPrefix))
            {
                  //this.finishSkill();
                  owner.AppendCoroutine(WaitFinishSkill(3, finishSkill), ePauseType.SYSTEM, owner);
            }
            else
            {
              this.owner.PlayAnime(spineCharacterAnimeId, current, _index3: this.owner.MotionPrefix, _isLoop: false);
              this.owner.AppendCoroutine(this.waitAnimationEnd((System.Action) (() => func.Call())), ePauseType.SYSTEM, this.owner);
            }
          }));
        }
      });
      func.Call();
    }
        //add
        private IEnumerator WaitFinishSkill(int waitFrame,System.Action action)
        {
            for (int i = 0; i < waitFrame; i++)
                yield return null;
            action.Call();
        }
    private void finishSkill()
    {
      this.battleManager.BFKPGHCBBKM = false;
      //this.movieManager.SetMirrorMode(false);
      this.battleManager.SetBlackoutTimeZero();
      this.owner.SkillEndProcess();
    }

    private void playPrincessFormMovie(
      int _movieIndex,
      List<PrincessSkillMovieData> _movieDataList,
      System.Action _callback)
    {
      PrincessSkillMovieData movieData = _movieDataList[_movieIndex - 1];
      bool flag = !this.forceNormalSD && this.owner.IsShadow;
      this.currentMovieType = !this.battleTimeScale.IsSpeedQuadruple ? (flag ? eMovieType.PRINCESS_FORM_SKILL_SHADOW : eMovieType.PRINCESS_FORM_SKILL_NORMAL) : (flag ? eMovieType.PRINCESS_FORM_SKILL_SHADOW_4X : eMovieType.PRINCESS_FORM_SKILL_NORMAL_4X);
      int unionBurstSkillId = this.owner.UnionBurstSkillId;
      float num = 0.75f;
      //ViewManager instance1 = ManagerSingleton<ViewManager>.Instance;
      //SoundManager instance2 = ManagerSingleton<SoundManager>.Instance;
      //if (_movieIndex != 1)
      //  this.movieManager.Load(this.currentMovieType, (long) unionBurstSkillId, _movieIndex: ((long) _movieIndex));
      //this.movieManager.SetScreenScale(this.currentMovieType, (long) unionBurstSkillId, new Vector2(instance1.ActiveScreenWidthInUIRoot, instance1.ActiveScreenWidthInUIRoot * num), true, (long) _movieIndex);
      //this.movieManager.SetCollider(this.currentMovieType, (long) unionBurstSkillId, false, (long) _movieIndex);
      //this.movieManager.SetSortOrder(this.currentMovieType, (long) unionBurstSkillId, 0, (long) _movieIndex);
      //this.movieManager.SetMoviePlayerLocalPosition(this.currentMovieType, (long) unionBurstSkillId, WrapperUnityEngineScreen.GetRestoreNGUIOffsetYVector3(), (long) _movieIndex);
      /*if (this.MovieVoiceDataDictionary.ContainsKey(_movieIndex))
      {
        foreach (KeyValuePair<int, PrincessFormProcessor.MovieVoiceData> keyValuePair in this.MovieVoiceDataDictionary[_movieIndex])
          this.owner.StartCoroutine(this.playUbVoiceWithDelay(keyValuePair.Value));
      }*/
      //instance2.PlaySeFromName(UnitCtrl.ConvertToSkillCueSheetName(this.owner.SoundUnitId), this.buildSkillMovieName(unionBurstSkillId, _movieIndex));
      //bool changeFPS = this.battleManager.FameRate == 30 && this.battleTimeScale.SpeedUpFlag;
      //if (changeFPS)
      //  PABCCELMCAJ.IKMGFNGHDPJ = (int) ((double) this.battleTimeScale.SpeedUpRate * 30.0);
      bool alreadyCalled = false;
      System.Action action = (System.Action) (() =>
      {
        if (alreadyCalled)
          return;
        alreadyCalled = true;
        //if (changeFPS)
        //  PABCCELMCAJ.IKMGFNGHDPJ = 30;
        _callback.Call();
      });
      if (movieData.UseFade)
      {
        //this.movieManager.Play(this.currentMovieType, (long) unionBurstSkillId, eMoviePlayType.NORMAL, action, _fadeStartTime: movieData.FadeStartTime, _fadeDurationTime: movieData.FadeDuration, _movieIndex: ((long) _movieIndex));
        this.battleManager.StartCoroutine(this.waitMovieEndFrame((System.Action) null, (long) unionBurstSkillId, this.currentMovieType, _movieIndex));
      }
      else
      {
       // this.movieManager.Play(this.currentMovieType, (long) unionBurstSkillId, eMoviePlayType.NORMAL, _movieIndex: ((long) _movieIndex));
        this.battleManager.StartCoroutine(this.waitMovieEndFrame(action, (long) unionBurstSkillId, this.currentMovieType, _movieIndex));
      }
      //if (!this.battleTimeScale.IsSpeedQuadruple)
      //  this.movieManager.SetPlaySpeed(this.currentMovieType, (long) unionBurstSkillId, this.battleTimeScale.SpeedUpFlag ? this.battleTimeScale.SpeedUpRate : 1f, (long) _movieIndex);
      this.currentMovieId = (long) unionBurstSkillId;
      this.currentMovieIndex = (long) _movieIndex;
    }

    public IEnumerator waitMovieEndFrame(
      System.Action _callback,
      long _movieId,
      eMovieType _movieType,
      int _movieIndex)
    {
     // MovieManager movieManager = ManagerSingleton<MovieManager>.Instance;
      float oneFrame = 0.03333334f;
      float duration = 0.0f;
      while (true)
      {
                //duration = movieManager.GetDurationTime(_movieType, _movieId, (long) _movieIndex);
                duration = 2.5f;
        if ((double) duration == 0.0)
          yield return (object) null;
        else
          break;
      }
      bool resetAnime = false;
            float seekPositionTime = 0;
            while (true)
      {
                seekPositionTime += 0.05f;//movieManager.GetSeekPositionTime(_movieType, _movieId, (long) _movieIndex);
        if (!resetAnime && (double) seekPositionTime >= (double) duration / 2.0)
        {
          resetAnime = true;
          eSpineCharacterAnimeId _animeId = this.unitActionController.GetSkillMotionType((int) _movieId) == SkillMotionType.EVOLUTION ? eSpineCharacterAnimeId.PRINCESS_SKILL_EVOLUTION : eSpineCharacterAnimeId.PRINCESS_SKILL;
          BattleSpineController currentSpineCtrl = this.owner.GetCurrentSpineCtrl();
          if (currentSpineCtrl.IsAnimation(_animeId, _movieIndex, _index3: this.owner.MotionPrefix))
            this.owner.PlayAnime(_animeId, _movieIndex, _index3: this.owner.MotionPrefix, _isLoop: false);
          else
            this.owner.PlayAnime(eSpineCharacterAnimeId.IDLE, this.owner.MotionPrefix);
          currentSpineCtrl.RealUpdate();
          currentSpineCtrl.RealLateUpdate();
          if (_movieIndex == 1)
            this.battleManager.CAOHLDNADPB = false;
          if (_callback == null)
            break;
        }
        if ((double) seekPositionTime + (double) oneFrame + (double) oneFrame * 0.5 < (double) duration)
          yield return (object) null;
        else
          goto label_13;
      }
      yield break;
label_13:
      _callback.Call();
    }

    public void Prepare()
    {
      this.battleManager.BFKPGHCBBKM = true;
      bool flag = !this.forceNormalSD && this.owner.IsShadow;
      this.currentMovieType = !this.battleTimeScale.IsSpeedQuadruple ? (flag ? eMovieType.PRINCESS_FORM_SKILL_SHADOW : eMovieType.PRINCESS_FORM_SKILL_NORMAL) : (flag ? eMovieType.PRINCESS_FORM_SKILL_SHADOW_4X : eMovieType.PRINCESS_FORM_SKILL_NORMAL_4X);
      //this.movieManager.Load(this.currentMovieType, (long) this.owner.UnionBurstSkillId, _movieIndex: 1L);
      /*foreach (KeyValuePair<int, Dictionary<int, PrincessFormProcessor.MovieVoiceData>> movieVoiceData in this.MovieVoiceDataDictionary)
      {
        foreach (KeyValuePair<int, PrincessFormProcessor.MovieVoiceData> keyValuePair in movieVoiceData.Value)
          keyValuePair.Value.VoiceDelayAndEnable.Enable = false;
      }*/
    }

    /*public void SetMovieVoiceData(VoiceDelayAndEnable _data, int _index, bool _rarity6)
    {
      int movieIndex = _data.MovieIndex;
      if (!this.MovieVoiceDataDictionary.ContainsKey(movieIndex))
      {
        PrincessFormProcessor.MovieVoiceData movieVoiceData = new PrincessFormProcessor.MovieVoiceData()
        {
          VoiceDelayAndEnable = this.copyVoiceDelayAndEnable(_data),
          Index = _index,
          Rarity6 = _rarity6
        };
        this.MovieVoiceDataDictionary.Add(movieIndex, new Dictionary<int, PrincessFormProcessor.MovieVoiceData>()
        {
          {
            _index,
            movieVoiceData
          }
        });
      }
      else
      {
        Dictionary<int, PrincessFormProcessor.MovieVoiceData> movieVoiceData1 = this.MovieVoiceDataDictionary[movieIndex];
        if (movieVoiceData1.ContainsKey(_index))
        {
          movieVoiceData1[_index].Index = _index;
          movieVoiceData1[_index].VoiceDelayAndEnable.Enable = _data.Enable;
          movieVoiceData1[_index].VoiceDelayAndEnable.Delay = _data.Delay;
          movieVoiceData1[_index].VoiceDelayAndEnable.MovieIndex = _data.MovieIndex;
        }
        else
        {
          PrincessFormProcessor.MovieVoiceData movieVoiceData2 = new PrincessFormProcessor.MovieVoiceData()
          {
            VoiceDelayAndEnable = this.copyVoiceDelayAndEnable(_data),
            Index = _index,
            Rarity6 = _rarity6
          };
          movieVoiceData1.Add(_index, movieVoiceData2);
        }
      }
    }*/

    /*private VoiceDelayAndEnable copyVoiceDelayAndEnable(
      VoiceDelayAndEnable _voiceDelayAndEnable) => new VoiceDelayAndEnable()
    {
      Delay = _voiceDelayAndEnable.Delay,
      Enable = _voiceDelayAndEnable.Enable,
      MovieIndex = _voiceDelayAndEnable.MovieIndex
    };*/

    /*private IEnumerator playUbVoiceWithDelay(PrincessFormProcessor.MovieVoiceData _data)
    {
      float time = 0.0f;
      Skill skill = _data.Rarity6 ? this.unitActionController.UnionBurstEvolutionList[0] : this.unitActionController.UnionBurstList[0];
      float delay = _data.VoiceDelayAndEnable.Delay;
      if (_data.VoiceDelayAndEnable.Enable)
      {
        while (true)
        {
          if (!SingletonMonoBehaviour<BattleHeaderController>.Instance.IsPaused)
            time += Time.deltaTime;
          if ((double) time < (double) delay)
            yield return (object) null;
          else
            break;
        }
        if (skill.EffectBranchId == _data.Index % 100)
          this.owner.PlayVoice(SoundManager.eVoiceType.UNION_BURST, _data.Index, false, false);
      }
    }*/

    private string buildSkillMovieName(int _a, int _b) => string.Format("p_skill_{0}_{1}", (object) _a, (object) _b);

    private IEnumerator waitAnimationEnd(System.Action _callback)
    {
      do
      {
        yield return (object) null;
      }
      while (this.owner.GetCurrentSpineCtrl().IsPlayAnimeBattle);
      _callback.Call();
    }

    public void Pause(bool _pause)
    {
      if (!this.battleManager.IsPlayingPrincessMovie)
        return;
      //this.movieManager.Pause(this.currentMovieType, this.currentMovieId, _pause, this.currentMovieIndex);
    }

    private class MovieVoiceData
    {
      //public VoiceDelayAndEnable VoiceDelayAndEnable;
      public int Index;
      public bool Rarity6;
    }
  }
}
