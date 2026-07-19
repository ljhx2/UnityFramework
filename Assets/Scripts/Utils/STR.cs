using System.Text;


//Using Example:
//string example = STR.Begin()._("Int : ")._(a)._(", Bool : ")._(b).End();

public class STR
{
    private static readonly STR singleton = new STR();
    private readonly StringBuilder sb = new StringBuilder(100);

    private STR() { }

    public static STR Begin()
    {
        singleton.sb.Clear();
        return singleton;
    }

    public STR _(string value) { sb.Append(value); return this; }
    public STR _(bool value) { sb.Append(value); return this; }
    public STR _(byte value) { sb.Append(value); return this; }
    public STR _(short value) { sb.Append(value); return this; }
    public STR _(ushort value) { sb.Append(value); return this; }
    public STR _(int value) { sb.Append(value); return this; }
    public STR _(uint value) { sb.Append(value); return this; }
    public STR _(float value) { sb.Append(value); return this; }
    public STR _(double value) { sb.Append(value); return this; }

    public string End()
    {
        return sb.ToString();
    }
}