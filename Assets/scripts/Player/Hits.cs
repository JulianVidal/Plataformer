public class Hits
{
    public readonly bool HitTopLeftN;
    public readonly bool HitTopRightN;

    public readonly bool HitBottomLeftS;
    public readonly bool HitBottomRightS;

    //private readonly bool _hitBottomLeftE;
    //private readonly bool _hitTopLeftE;

    //private readonly bool _hitBottomRightW;
    //private readonly bool _hitTopRightW;


    public Hits(bool hitTopLeftN, bool hitTopRightN, bool hitBottomLeftS, bool hitBottomRightS/*, bool hitBottomLeftE, bool hitTopLeftE, bool hitBottomRightW, bool hitTopRightW*/)
    {
        HitTopLeftN = hitTopLeftN;
        HitTopRightN = hitTopRightN;

        HitBottomLeftS = hitBottomLeftS;
        HitBottomRightS = hitBottomRightS;

        //_hitBottomLeftE = hitBottomLeftE;
        //_hitTopLeftE = hitTopLeftE;

        //_hitBottomRightW = hitBottomRightW;
        //_hitTopRightW = hitTopRightW;
    }
}