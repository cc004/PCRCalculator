// Decompiled with JetBrains decompiler
// Type: Elements.EquipSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;

namespace Elements
{
    public class EquipSlot
    {
        public ObscuredInt Id { get; private set; }

        public ObscuredBool IsSlot { get; private set; }

        public ObscuredInt EnhancementLevel { get; private set; }

        public ObscuredInt EnhancementPt { get; private set; }

        public ObscuredInt Rank { get; private set; }

        public ObscuredInt Status { get; private set; }

        public void SetId(int _id) => this.Id = (ObscuredInt)_id;

        public void SetIsSlot(bool _isSlot) => this.IsSlot = (ObscuredBool)_isSlot;

        public void SetEnhancementLevel(int _enhancementLevel) => this.EnhancementLevel = (ObscuredInt)_enhancementLevel;

        public void SetEnhancementPt(int _enhancementPt) => this.EnhancementPt = (ObscuredInt)_enhancementPt;

        public void SetRank(int _rank) => this.Rank = (ObscuredInt)_rank;

        public void SetStatus(int _status) => this.Status = (ObscuredInt)_status;

        private void initializeEquipSlot()
        {
            this.Id = (ObscuredInt)0;
            this.IsSlot = (ObscuredBool)false;
            this.EnhancementLevel = (ObscuredInt)0;
            this.EnhancementPt = (ObscuredInt)0;
            this.Rank = (ObscuredInt)0;
            this.Status = (ObscuredInt)0;
        }


    }
}
