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
        private FixedTransform transformRaw;

        public void OnAwake() => transformRaw = new FixedTransform(base.transform);

        public void OnDestroy() => DestructByOnDestroy();

        protected virtual void DestructByOnDestroy() => transformRaw = null;

        public new FixedTransform transform
        {
            get
            {
                if (transformRaw == null)
                    transformRaw = new FixedTransform(base.transform);
                if (transformRaw.TargetTransform == null)
                    transformRaw.TargetTransform = base.transform;
                return transformRaw;
            }
            set
            {
            }
        }

        public void SetLocalPosX(float _x) => transform.SetLocalPosX(_x);

        public void SetLocalPosY(float _y) => transform.SetLocalPosY(_y);

        public class FixedTransform
        {
            public const float DIGID = 100f;
            private const float SCALE = 1;//60f;
            public int positionX;
            private int positionY;
            public Transform TargetTransform;

            public FixedTransform(Transform _targetTransform) => TargetTransform = _targetTransform;

            public Vector3 localPosition
            {
                get => new Vector3(positionX*SCALE / DIGID, positionY*SCALE / DIGID);
                set
                {
                    SetLocalPosX(value.x);
                    SetLocalPosY(value.y);
                }
            }

            public Vector3 position
            {
                get => !(TargetTransform.parent == null) ? TargetTransform.parent.TransformPoint(localPosition) : localPosition;
                set => localPosition = TargetTransform.parent == null ? value : TargetTransform.parent.InverseTransformPoint(value);
            }

            public Transform parent
            {
                get => TargetTransform.parent;
                set => TargetTransform.parent = value;
            }

            public Vector3 lossyScale
            {
                get => TargetTransform.lossyScale;
                private set
                {
                }
            }

            public Vector3 localScale
            {
                get => TargetTransform.localScale;
                set => TargetTransform.localScale = value;
            }

            public void SetLocalPosX(float _x)
            {
                positionX = BattleUtil.FloatToInt(_x * DIGID / SCALE);
                TargetTransform.SetLocalPosX(positionX / DIGID);
            }

            public void SetLocalPosY(float _y)
            {
                positionY = (int)((double)_y * DIGID / SCALE);
                TargetTransform.SetLocalPosY(positionY / DIGID);
                TargetTransform.SetLocalPosZ(positionY / DIGID);
            }
        }
    }
}
