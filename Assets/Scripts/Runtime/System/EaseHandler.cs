using System;
public enum Ease
{
    Linear,
    InSine,
    OutSine,
    InOutSine,
    InQuad,
    OutQuad,
    InOutQuad,
    InCubic,
    OutCubic,
    InOutCubic,
    InQuart,
    OutQuart,
    InOutQuart,
    InQuint,
    OutQuint,
    InOutQuint,
    InDecimatio,
    OutDecimatio,
    InOutDecimatio,
    InExpo,
    OutExpo,
    InOutExpo,
    InCirc,
    OutCirc,
    InOutCirc,
    InElastic,
    OutElastic,
    InOutElastic,
    InBack,
    OutBack,
    InOutBack,
    InBounce,
    OutBounce,
    InOutBounce,
}
public static class EaseHandler
{
    public static float Evaluate(Ease easeType, float percent)
    {
        switch (easeType)
        {
            case Ease.InSine:
                return InSine(percent);
            case Ease.OutSine:
                return OutSine(percent);
            case Ease.InOutSine:
                return InOutSine(percent);
            case Ease.InQuad:
                return InQuad(percent);
            case Ease.OutQuad:
                return OutQuad(percent);
            case Ease.InOutQuad:
                return InOutQuad(percent);
            case Ease.InCubic:
                return InCubic(percent);
            case Ease.OutCubic:
                return OutCubic(percent);
            case Ease.InOutCubic:
                return InOutCubic(percent);
            case Ease.InQuart:
                return InQuart(percent);
            case Ease.OutQuart:
                return OutQuart(percent);
            case Ease.InOutQuart:
                return InOutQuart(percent);
            case Ease.InQuint:
                return InQuint(percent);
            case Ease.OutQuint:
                return OutQuint(percent);
            case Ease.InOutQuint:
                return InOutQuint(percent);
            case Ease.InExpo:
                return InExpo(percent);
            case Ease.OutExpo:
                return OutExpo(percent);
            case Ease.InOutExpo:
                return InOutExpo(percent);
            case Ease.InCirc:
                return InCirc(percent);
            case Ease.OutCirc:
                return OutCirc(percent);
            case Ease.InOutCirc:
                return InOutCirc(percent);
            case Ease.InElastic:
                return InElastic(percent);
            case Ease.OutElastic:
                return OutElastic(percent);
            case Ease.InOutElastic:
                return InOutElastic(percent);
            case Ease.InBack:
                return InBack(percent);
            case Ease.OutBack:
                return OutBack(percent);
            case Ease.InOutBack:
                return InOutBack(percent);
            case Ease.InBounce:
                return InBounce(percent);
            case Ease.OutBounce:
                return OutBounce(percent);
            case Ease.InOutBounce:
                return InOutBounce(percent);
            case Ease.InDecimatio:
                return InDecimatio(percent);
            case Ease.OutDecimatio:
                return OutDecimatio(percent);
            case Ease.InOutDecimatio:
                return InOutDecimatio(percent);
            default:
                return percent;
        }
    }
    public static float Line(float percent)
    {
        return percent;
    }
    public static float InOutBounce(float percent)
    {
        return percent < 0.5f ?
            (1f - OutBounce(1f - 2f * percent)) / 2f :
            (1f + OutBounce(2f * percent - 1f)) / 2f;
    }
    public static float OutBounce(float percent)
    {
        float n1 = 7.5625f;
        float d1 = 2.75f;
        if (percent < 1f / d1)
        {
            return n1 * percent * percent;
        }
        else if (percent < 2f / d1)
        {
            return n1 * (percent -= 1.5f / d1) * percent + 0.75f;
        }
        else if (percent < 2.5f / d1)
        {
            return n1 * (percent -= 2.25f / d1) * percent + 0.9375f;
        }
        else
        {
            return n1 * (percent -= 2.625f / d1) * percent + 0.984375f;
        }
    }
    public static float InBounce(float percent)
    {
        return 1f - OutBounce(1f - percent);
    }
    public static float InOutBack(float percent)
    {
        float c1 = 1.70158f;
        float c2 = c1 * 1.525f;
        return percent < 0.5f ?
            ((4f * percent * percent) * ((c2 + 1f) * 2f * percent - c2)) / 2f :
            ((2f * percent - 2f) * (2f * percent - 2f) * ((c2 + 1f) * (percent * 2f - 2f) + c2) + 2f) / 2f;
    }
    public static float OutBack(float percent)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1f;
        return 1 + c3 *
           ((percent - 1f) * (percent - 1f) * (percent - 1f)) +
            c1 * ((percent - 1f) * (percent - 1f));
    }
    public static float InBack(float percent)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1f;
        return c3 * percent * percent * percent - c1 * percent * percent;
    }
    public static float InOutElastic(float percent)
    {
        double c5 = ((2.0 * Math.PI) / 4.5);
        double r =
            percent == 0f
          ? 0f
          : percent == 1f
          ? 1f
          : percent < 0.5f
          ? -(Math.Pow(2.0, 20.0 * percent - 10.0) *
          Math.Sin((20.0 * percent - 11.125) * c5)) / 2
          : (Math.Pow(2.0, -20.0 * percent + 10.0) *
          Math.Sin((20.0 * percent - 11.125) * c5)) / 2.0 + 1.0;
        return (float)r;
    }
    public static float OutElastic(float percent)
    {
        double c4 = (2.0 * Math.PI) / 3.0;
        double r =
            percent == 0f
          ? 0.0
          : percent == 1f
          ? 1
          : Math.Pow(2.0, -10.0 * percent) *
          Math.Sin((percent * 10.0 - 0.75) * c4) + 1.0;
        return (float)r;
    }
    public static float InElastic(float percent)
    {
        double c4 = (2.0 * Math.PI) / 3.0;
        double r =
            percent == 0.0f
          ? 0.0
          : percent == 1.0f
          ? 1.0
          : -Math.Pow(2.0, 10.0 * percent - 10.0) *
          Math.Sin((percent * 10.0 - 10.75) * c4);
        return (float)r;
    }
    public static float InOutCirc(float percent)
    {
        double r = percent < 0.5f
          ? (1.0 - Math.Sqrt(1.0 - Math.Pow(2.0 * percent, 2.0))) / 2.0
          : (Math.Sqrt(1.0 - Math.Pow(-2.0 * percent + 2.0, 2.0)) + 1.0) / 2.0;
        return (float)r;
    }
    public static float OutCirc(float percent)
    {
        return (float)Math.Sqrt(1.0 - Math.Pow(percent - 1.0, 2.0));
    }
    public static float InCirc(float percent)
    {
        return 1f - (float)Math.Sqrt(1.0 - Math.Pow(percent, 2.0));
    }
    public static float InOutExpo(float percent)
    {
        double r =
            percent == 0f
          ? 0.0
          : percent == 1f
          ? 1.0
          : percent < 0.5f ?
          Math.Pow(2.0, 20.0 * percent - 10.0) / 2.0 :
          (2.0 - Math.Pow(2.0, -20.0 * percent + 10.0)) / 2.0;
        return (float)r;
    }
    public static float OutExpo(float percent)
    {
        return percent == 1 ? 1f : 1f - (float)Math.Pow(2.0, -10.0 * percent);
    }
    public static float InExpo(float percent)
    {
        return percent == 0f ? 0.0f : (float)Math.Pow(2.0, 10.0 * percent - 10.0);
    }
    public static float InOutQuint(float percent)
    {
        return percent < 0.5f ? 16f *
            percent *
            percent *
            percent *
            percent *
            percent :
            1f - (float)Math.Pow(-2.0 * percent + 2.0, 5.0) / 2.0f;
    }
    public static float OutQuint(float percent)
    {
        return 1f - (float)Math.Pow(1.0 - percent, 5.0);
    }
    public static float InQuint(float percent)
    {
        return percent * percent * percent * percent * percent;
    }
    public static float InOutQuart(float percent)
    {
        return percent < 0.5f ?
            8f * percent * percent * percent * percent :
            1f - (float)Math.Pow(-2.0 * percent + 2.0, 4.0) / 2.0f;
    }
    public static float OutQuart(float percent)
    {
        return 1f - (float)Math.Pow(1.0 - percent, 4.0);
    }
    public static float InQuart(float percent)
    {
        return percent * percent * percent * percent;
    }
    public static float InOutCubic(float percent)
    {
        return percent < 0.5f ?
            4f * percent * percent * percent :
            1f - (float)Math.Pow(-2.0 * percent + 2.0, 3.0) / 2f;
    }
    public static float OutCubic(float percent)
    {
        return 1f - (float)Math.Pow(1.0 - percent, 3.0);
    }
    public static float InCubic(float percent)
    {
        return percent * percent * percent;
    }
    public static float InOutQuad(float percent)
    {
        return percent < 0.5f ?
            2f * percent * percent :
            1f - (float)Math.Pow(-2.0 * percent + 2.0, 2.0) / 2f;
    }
    public static float OutQuad(float percent)
    {
        return 1f - (1f - percent) * (1f - percent);
    }

    public static float InQuad(float percent)
    {
        return percent * percent;
    }
    public static float InOutSine(float percent)
    {
        return -(float)(Math.Cos(Math.PI * percent) - 1.0) / 2f;
    }
    public static float OutSine(float percent)
    {
        return (float)Math.Sin((percent * Math.PI) / 2.0);
    }
    public static float InSine(float percent)
    {
        return 1f - (float)Math.Cos((percent * Math.PI) / 2f);
    }
    public static float InOutDecimatio(float percent)
    {
        return percent < 0.5f ?
            (float)Math.Pow(2.0 * percent, 10.0) / 2.0f :
            1f - (float)Math.Pow(-2.0 * percent + 2.0, 5.0) / 2.0f;
    }
    public static float OutDecimatio(float percent)
    {
        return 1f - (float)Math.Pow(1.0 - percent, 10.0);
    }
    public static float InDecimatio(float percent)
    {
        return (float)Math.Pow(percent, 10.0);
    }
}