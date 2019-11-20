public class Hits
{
    public bool _hitTopLeftN;
    public bool _hitTopRightN;

    public bool _hitBottomLeftS;
    public bool _hitBottomRightS;

    public bool _hitBottomLeftE;
    public bool _hitTopLeftE;

    public bool _hitBottomRightW;
    public bool _hitTopRightW;


    public Hits(bool hitTopLeftN, bool hitTopRightN, bool hitBottomLeftS, bool hitBottomRightS, bool hitBottomLeftE, bool hitTopLeftE, bool hitBottomRightW, bool hitTopRightW)
    {
        _hitTopLeftN = hitTopLeftN;
        _hitTopRightN = hitTopRightN;

        _hitBottomLeftS = hitBottomLeftS;
        _hitBottomRightS = hitBottomRightS;

        _hitBottomLeftE = hitBottomLeftE;
        _hitTopLeftE = hitTopLeftE;

        _hitBottomRightW = hitBottomRightW;
        _hitTopRightW = hitTopRightW;
    }
}