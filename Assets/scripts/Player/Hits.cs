public class Hits
{
    public readonly bool HitTopLeftN;
    public readonly bool HitTopRightN;

    public readonly bool HitBottomLeftS;
    public readonly bool HitBottomRightS;

    public readonly bool HitBottomLeftE;
    public readonly bool HitTopLeftE;

    public readonly bool HitBottomRightW;
    public readonly bool HitTopRightW;


    public Hits(bool hitTopLeftN, bool hitTopRightN, bool hitBottomLeftS, bool hitBottomRightS, bool hitBottomLeftE, bool hitTopLeftE, bool hitBottomRightW, bool hitTopRightW)
    {
        HitTopLeftN = hitTopLeftN;
        HitTopRightN = hitTopRightN;

        HitBottomLeftS = hitBottomLeftS;
        HitBottomRightS = hitBottomRightS;

        HitBottomLeftE = hitBottomLeftE;
        HitTopLeftE = hitTopLeftE;

        HitBottomRightW = hitBottomRightW;
        HitTopRightW = hitTopRightW;
    }
}