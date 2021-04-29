// Decompiled with JetBrains decompiler
// Type: Elements.FixedTransformMonoBehavior
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using UnityEngine;

namespace Elements
{
    public class FixedTransformMonoBehavior : MonoBehaviour
    {
        private FixedTransformMonoBehavior.FixedTransform transformRaw;

        public void OnAwake() => this.transformRaw = new FixedTransformMonoBehavior.FixedTransform(base.transform);

        public void OnDestroy() => this.DestructByOnDestroy();

        protected virtual void DestructByOnDestroy() => this.transformRaw = (FixedTransformMonoBehavior.FixedTransform)null;

        public new FixedTransformMonoBehavior.FixedTransform transform
        {
            get
            {
                if (this.transformRaw == null)
                    this.transformRaw = new FixedTransformMonoBehavior.FixedTransform(base.transform);
                if ((Object)this.transformRaw.TargetTransform == (Object)null)
                    this.transformRaw.TargetTransform = base.transform;
                return this.transformRaw;
            }
            set
            {
            }
        }

        public void SetLocalPosX(float _x) => this.transform.SetLocalPosX(_x);

        public void SetLocalPosY(float _y) => this.transform.SetLocalPosY(_y);

        public class FixedTransform
        {
            private const float DIGID = 100f;
            private const float SCALE = 1;//60f;
            private int positionX;
            private int positionY;
            public Transform TargetTransform;

            public FixedTransform(Transform _targetTransform) => this.TargetTransform = _targetTransform;

            public Vector3 localPosition
            {
                get => new Vector3((float)this.positionX*SCALE / DIGID, (float)this.positionY*SCALE / DIGID);
                set
                {
                    this.SetLocalPosX(value.x);
                    this.SetLocalPosY(value.y);
                }
            }

            public Vector3 position
            {
                get => !((Object)this.TargetTransform.parent == (Object)null) ? this.TargetTransform.parent.TransformPoint(this.localPosition) : this.localPosition;
                set => this.localPosition = (Object)this.TargetTransform.parent == (Object)null ? value : this.TargetTransform.parent.InverseTransformPoint(value);
            }

            public Transform parent
            {
                get => this.TargetTransform.parent;
                set => this.TargetTransform.parent = value;
            }

            public Vector3 lossyScale
            {
                get => this.TargetTransform.lossyScale;
                private set
                {
                }
            }

            public Vector3 localScale
            {
                get => this.TargetTransform.localScale;
                set => this.TargetTransform.localScale = value;
            }

            public void SetLocalPosX(float _x)
            {
                this.positionX = BattleUtil.FloatToInt(_x * DIGID / SCALE);
                this.TargetTransform.SetLocalPosX((float)this.positionX / DIGID);
            }

            public void SetLocalPosY(float _y)
            {
                this.positionY = (int)((double)_y * DIGID / SCALE);
                this.TargetTransform.SetLocalPosY((float)this.positionY / DIGID);
                this.TargetTransform.SetLocalPosZ((float)this.positionY / DIGID);
            }
        }
    }
}
