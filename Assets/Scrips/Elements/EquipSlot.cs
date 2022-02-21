// Decompiled with JetBrains decompiler
// Type: Elements.EquipSlot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EE4A7FA8-7E00-4124-8344-C695120E3AA4
// Assembly location: C:\Users\user\Desktop\Assembly-CSharp.dll



namespace Elements
{
    public class EquipSlot
    {
        public int Id { get; private set; }

        public bool IsSlot { get; private set; }

        public int EnhancementLevel { get; private set; }

        public int EnhancementPt { get; private set; }

        public int Rank { get; private set; }

        public int Status { get; private set; }

        public void SetId(int _id) => Id = _id;

        public void SetIsSlot(bool _isSlot) => IsSlot = _isSlot;

        public void SetEnhancementLevel(int _enhancementLevel) => EnhancementLevel = _enhancementLevel;

        public void SetEnhancementPt(int _enhancementPt) => EnhancementPt = _enhancementPt;

        public void SetRank(int _rank) => Rank = _rank;

        public void SetStatus(int _status) => Status = _status;

        private void initializeEquipSlot()
        {
            Id = 0;
            IsSlot = false;
            EnhancementLevel = 0;
            EnhancementPt = 0;
            Rank = 0;
            Status = 0;
        }


    }
}
