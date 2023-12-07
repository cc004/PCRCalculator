namespace ActionParameterSerializer
{
    public class Ailment
    {

        public class AilmentDetail
        {
            public object detail;
            public void setDetail(object obj)
            {
                detail = obj;
            }

            public string description()
            {
                if (detail is DotDetail dotDetail)
                {
                    return dotDetail.description();
                }
                else if (detail is ActionDetail actionDetail)
                {
                    return actionDetail.description();
                }
                else if (detail is CharmDetail charmDetail)
                {
                    return charmDetail.description();
                }
                else
                {
                    return Utils.JavaFormat(Utils.GetString("unknown"));
                }
            }
        }

        public enum DotDetail
        {
            detain = 0,
            poison = 1,
            burn = 2,
            curse = 3,
            violentPoison = 4,
            hex = 5,
            compensation = 6,
            world_lightning = 10,
            unknown = -1
        }

        public enum CharmDetail
        {
            charm = 0,
            confuse = 1
        }

        public enum ActionDetail
        {
            slow = 1,
            haste = 2,
            paralyse = 3,
            freeze = 4,
            bind = 5,
            sleep = 6,
            stun = 7,
            petrify = 8,
            detain = 9,
            faint = 10,
            timeStop = 11,
            unknown = 12
        }

        public enum AilmentType
        {
            knockBack = 3,
            action = 8,
            dot = 9,
            charm = 11,
            darken = 12,
            silence = 13,
            confuse = 19,
            instantDeath = 30,
            countBlind = 56,
            inhibitHeal = 59,
            attackSeal = 60,
            fear = 61,
            awe = 62,
            toad = 69,
            maxHP = 70,
            hPRegenerationDown = 76,
            damageTakenIncreased = 78,
            damageByBehaviour = 79,
            unknown = 80
        }
        public AilmentType ailmentType;
        public AilmentDetail ailmentDetail;

        public Ailment(int type, int detail)
        {

            ailmentType = (AilmentType)(type);
            ailmentDetail = new AilmentDetail();
            switch (ailmentType)
            {
                case AilmentType.action:
                    ailmentDetail.setDetail((ActionDetail)(detail));
                    break;
                case AilmentType.dot:
                case AilmentType.damageByBehaviour:
                    ailmentDetail.setDetail((DotDetail)(detail));
                    break;
                case AilmentType.charm:
                    ailmentDetail.setDetail((CharmDetail)(detail));
                    break;
                default:
                    ailmentDetail = null;
                    break;
            }
        }

        public string description()
        {
            if (ailmentDetail != null)
            {
                return ailmentDetail.description();
            }
            else
            {
                return ailmentType.description();
            }
        }
    }



}
